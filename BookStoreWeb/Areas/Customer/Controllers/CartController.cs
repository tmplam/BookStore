using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Utility.StaticDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Security.Claims;

namespace BookStoreWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity?) User.Identity;
            var userId = claimsIdentity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            ShoppingCartVM = new ShoppingCartVM()
            {
                ShoppingCarts = await _unitOfWork.ShoppingCart.GetAllAsync(shoppingCart =>
                    shoppingCart.ApplicationUserId == userId, includeProperties: "Product"),
                OrderHeader = new OrderHeader()
            };

            foreach (var cart in ShoppingCartVM.ShoppingCarts)
            {
                ShoppingCartVM.OrderHeader.OrderTotal += cart.Quantity * cart.Product.Price;
            }

            return View(ShoppingCartVM);
        }

        [HttpGet]
        public async Task<IActionResult> Summary()
        {
            var claimsIdentity = (ClaimsIdentity?) User.Identity;
            var userId = claimsIdentity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            ShoppingCartVM = new ShoppingCartVM()
            {
                ShoppingCarts = await _unitOfWork.ShoppingCart.GetAllAsync(shoppingCart =>
                    shoppingCart.ApplicationUserId == userId, includeProperties: "Product"),
                OrderHeader = new OrderHeader()
            };

            var applicationUser = await _unitOfWork.ApplicationUser.GetAsync(user => user.Id == userId);

            ShoppingCartVM.OrderHeader.ApplicationUser = applicationUser;

            ShoppingCartVM.OrderHeader.Name = applicationUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.Address = applicationUser.Address;


            foreach (var cart in ShoppingCartVM.ShoppingCarts)
            {
                ShoppingCartVM.OrderHeader.OrderTotal += cart.Quantity * cart.Product.Price;
            }

            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ActionName(nameof(Summary))]
        public async Task<IActionResult> SummaryPOST()
        {
            var claimsIdentity = (ClaimsIdentity?)User.Identity;
            var userId = claimsIdentity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            ShoppingCartVM.ShoppingCarts = await _unitOfWork.ShoppingCart.GetAllAsync(shoppingCart =>
                    shoppingCart.ApplicationUserId == userId, includeProperties: "Product");

            ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
            ShoppingCartVM.OrderHeader.ApplicationUserId = userId;

            var applicationUser = await _unitOfWork.ApplicationUser.GetAsync(user => user.Id == userId);

            foreach (var cart in ShoppingCartVM.ShoppingCarts)
            {
                ShoppingCartVM.OrderHeader.OrderTotal += cart.Quantity * cart.Product.Price;
            }

            if (applicationUser?.CompanyId == null)
            {
                // Regular customer account and we need to capture payment
                ShoppingCartVM.OrderHeader.OrderStatus = OrderStatuses.Pending;
                ShoppingCartVM.OrderHeader.PaymentStatus = PaymentStatuses.Pending;
            }
            else
            {
                // It is a company account
                ShoppingCartVM.OrderHeader.OrderStatus = OrderStatuses.Approved;
                ShoppingCartVM.OrderHeader.PaymentStatus = PaymentStatuses.DelayedPayment;
            }

            try
            {
                // Handle create order and order details
                await _unitOfWork.CreateTransactionAsync();

                ShoppingCartVM.OrderHeader.OrderDetails = new Collection<OrderDetail>();

                foreach (var cart in ShoppingCartVM.ShoppingCarts)
                {
                    var orderDetail = new OrderDetail()
                    {
                        ProductId = cart.ProductId,
                        Price = cart.Product.Price,
                        Quantity = cart.Quantity,
                    };
                    ShoppingCartVM.OrderHeader.OrderDetails.Add(orderDetail);
                }

                await _unitOfWork.OrderHeader.AddAsync(ShoppingCartVM.OrderHeader);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                TempData["errorMessage"] = "Error while placing order";
                return RedirectToAction(nameof(Index));
            }

            if (applicationUser?.CompanyId == null)
            {
                // Stripe logic payment for regular account
                var domain = Request.Scheme + "://" + Request.Host.Value;
                var options = new Stripe.Checkout.SessionCreateOptions
                {
                    SuccessUrl = $"{domain}/Customer/Cart/OrderConfirmation?orderId={ShoppingCartVM.OrderHeader.Id}",
                    CancelUrl = $"{domain}/Customer/Cart/OrderConfirmation?orderId={ShoppingCartVM.OrderHeader.Id}",
                    LineItems = new List<Stripe.Checkout.SessionLineItemOptions>(),
                    Mode = "payment",
                };

                foreach (var item in ShoppingCartVM.ShoppingCarts)
                {
                    var sessionLineItem = new Stripe.Checkout.SessionLineItemOptions
                    {
                        PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long) (item.Product.Price * 100), // In cents
                            Currency = "usd",
                            ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Title,
                            }
                        },
                        Quantity = item.Quantity,
                    };
                    options.LineItems.Add(sessionLineItem);
                }

                var service = new Stripe.Checkout.SessionService();
                Stripe.Checkout.Session session = service.Create(options);

                // Just have SessionId, PaymentId is null for now

                await _unitOfWork.CreateTransactionAsync();
                await _unitOfWork.OrderHeader.UpdateStripePaymentIDAsync(ShoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            
                // Redirect to payment page
                Response.Headers.Add("Location", session.Url);

                return new StatusCodeResult(303);
            }
            else
            {
                // A company account will place successfully, so clear the cart
                await _unitOfWork.CreateTransactionAsync();
                IEnumerable<ShoppingCart> shoppingCarts = await _unitOfWork.ShoppingCart.GetAllAsync(shoppingCart =>
                            shoppingCart.ApplicationUserId == userId);
                _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
                HttpContext.Session.Remove(ShoppingCartSession.SessionKey); // Clear shopping cart session
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            return RedirectToAction(nameof(OrderConfirmation), new { orderId = ShoppingCartVM.OrderHeader.Id });
        }

        [HttpGet]
        public async Task<IActionResult> OrderConfirmation(Guid orderId)
        {
            var orderHeader = await _unitOfWork.OrderHeader.GetAsync(orderHeader => orderHeader.Id == orderId,
                includeProperties: "ApplicationUser");
            
            if (orderHeader != null)
            {
                if (orderHeader.PaymentStatus != PaymentStatuses.DelayedPayment
                    && orderHeader.PaymentStatus != PaymentStatuses.Approved)
                {
                    // Order by customer
                    var service = new Stripe.Checkout.SessionService();
                    Stripe.Checkout.Session session = service.Get(orderHeader.SessionId);

                    await _unitOfWork.CreateTransactionAsync();
                    if (session.PaymentStatus.ToLower() == "paid")
                    {
                        if (string.IsNullOrEmpty(orderHeader.PaymentIntentId))
                        {
                            // Update payment info
                            await _unitOfWork.OrderHeader.UpdateStripePaymentIDAsync(orderId, session.Id, session.PaymentIntentId);
                            await _unitOfWork.OrderHeader.UpdateStatusAsync(orderId, OrderStatuses.Approved, PaymentStatuses.Approved);
                            // Remove shopping cart's items
                            IEnumerable<ShoppingCart> shoppingCarts = await _unitOfWork.ShoppingCart.GetAllAsync(shoppingCart =>
                                shoppingCart.ApplicationUserId == orderHeader.ApplicationUserId);
                            _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
                            HttpContext.Session.Remove(ShoppingCartSession.SessionKey); // Clear shopping cart session
                            await _unitOfWork.SaveChangesAsync();
                            await _unitOfWork.CommitAsync();
                        }
                    }
                    else
                    {
                        // Delete order if unpaid, ondelete cascade so orderDetails will be removeds
                        _unitOfWork.OrderHeader.Remove(orderHeader);
                        await _unitOfWork.SaveChangesAsync();
                        await _unitOfWork.CommitAsync();
                    }
                }
                else if (orderHeader.PaymentStatus == PaymentStatuses.DelayedPayment)
                {
                    return View(orderId);
                }
            }
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Plus(Guid productId)
        {
            var claimsIdentity = (ClaimsIdentity?)User.Identity;
            var userId = claimsIdentity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var cartFromDb = await _unitOfWork.ShoppingCart.GetAsync(shoppingCart =>
                    shoppingCart.ApplicationUserId == userId && shoppingCart.ProductId == productId);
            if (cartFromDb != null)
            {
                cartFromDb.Quantity += 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
                await _unitOfWork.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        #region HANDLE CART ITEMS
        
        public async Task<IActionResult> Minus(Guid productId)
        {
            var claimsIdentity = (ClaimsIdentity?)User.Identity;
            var userId = claimsIdentity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var cartFromDb = await _unitOfWork.ShoppingCart.GetAsync(shoppingCart =>
                    shoppingCart.ApplicationUserId == userId && shoppingCart.ProductId == productId);

            if (cartFromDb != null)
            {
                if (cartFromDb.Quantity <= 1)
                {
                    // Remove item from cart
                    _unitOfWork.ShoppingCart.Remove(cartFromDb);

                    // Update shopping cart count session
                    var cartCount = (await _unitOfWork.ShoppingCart.GetAllAsync(cart =>
                        cart.ApplicationUserId == userId)).Count();
                    HttpContext.Session.SetInt32(ShoppingCartSession.SessionKey, cartCount - 1);
                }
                else
                {
                    cartFromDb.Quantity -= 1;
                    _unitOfWork.ShoppingCart.Update(cartFromDb);
                }
                await _unitOfWork.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(Guid productId)
        {
            var claimsIdentity = (ClaimsIdentity?)User.Identity;
            var userId = claimsIdentity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var cartFromDb = await _unitOfWork.ShoppingCart.GetAsync(shoppingCart =>
                    shoppingCart.ApplicationUserId == userId && shoppingCart.ProductId == productId);

            if (cartFromDb != null)
            {
                _unitOfWork.ShoppingCart.Remove(cartFromDb);
                
                // Update shopping cart count session
                var cartCount = (await _unitOfWork.ShoppingCart.GetAllAsync(cart =>
                    cart.ApplicationUserId == userId)).Count();
                HttpContext.Session.SetInt32(ShoppingCartSession.SessionKey, cartCount - 1);

                await _unitOfWork.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
