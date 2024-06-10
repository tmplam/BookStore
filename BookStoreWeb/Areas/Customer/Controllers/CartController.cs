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

            //ShoppingCartVM.OrderHeader.ApplicationUser = applicationUser;

            foreach (var cart in ShoppingCartVM.ShoppingCarts)
            {
                ShoppingCartVM.OrderHeader.OrderTotal += cart.Quantity * cart.Product.Price;
            }

            if (applicationUser?.CompanyId == null)
            {
                // Regular customer account and we need to capture payment
                ShoppingCartVM.OrderHeader.OrderStatus = OrderStatuses.StatusPending;
                ShoppingCartVM.OrderHeader.PaymentStatus = PaymentStatuses.PaymentStatusPending;
            }
            else
            {
                // It is a company account
                ShoppingCartVM.OrderHeader.OrderStatus = OrderStatuses.StatusApproved;
                ShoppingCartVM.OrderHeader.PaymentStatus = PaymentStatuses.PaymentStatusDelayedPayment;
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
                var domain = "https://localhost:44303";
                var options = new Stripe.Checkout.SessionCreateOptions
                {
                    SuccessUrl = $"{domain}/Customer/Cart/OrderConfirmation/{ShoppingCartVM.OrderHeader.Id}",
                    CancelUrl = $"{domain}/Customer/Cart",
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
                // If customer click "back" SessionId will be null
                await _unitOfWork.OrderHeader.UpdateStripePaymentID(ShoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
                await _unitOfWork.SaveChangesAsync();
            
                // Redirect to payment page
                Response.Headers.Add("Location", session.Url);

                return new StatusCodeResult(303);
            }

            return RedirectToAction(nameof(OrderConfirmation), new { id = ShoppingCartVM.OrderHeader.Id });
        }

        [HttpGet]
        public async Task<IActionResult> OrderConfirmation(Guid id)
        {
            var orderHeader = await _unitOfWork.OrderHeader.GetAsync(orderHeader => orderHeader.Id == id,
                includeProperties: "ApplicationUser");
            
            if (orderHeader.PaymentStatus != PaymentStatuses.PaymentStatusDelayedPayment
                && orderHeader.PaymentStatus != PaymentStatuses.PaymentStatusApproved)
            {
                // Order by customer
                var service = new Stripe.Checkout.SessionService();
                Stripe.Checkout.Session session = service.Get(orderHeader.SessionId);

                if (session.PaymentStatus.ToLower() == "paid")
                {
                    await _unitOfWork.OrderHeader.UpdateStripePaymentID(id, session.Id, session.PaymentIntentId);
                    await _unitOfWork.OrderHeader.UpdateStatus(id, OrderStatuses.StatusApproved, PaymentStatuses.PaymentStatusApproved);
                    await _unitOfWork.SaveChangesAsync();
                }
            }

            IEnumerable<ShoppingCart> shoppingCarts = await _unitOfWork.ShoppingCart.GetAllAsync(shoppingCart => 
                shoppingCart.ApplicationUserId == orderHeader.ApplicationUserId);

            _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
            await _unitOfWork.SaveChangesAsync();

            return View(id);
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
                await _unitOfWork.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
