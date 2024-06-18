using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Utility.StaticDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

#nullable disable

namespace BookStoreWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public OrderVM OrderVM { get; set; }

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string? status)
        {
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            OrderListVM orderListVM = new OrderListVM
            {
                PaymentPendingCount = await _unitOfWork.OrderHeader.CountAsync(order => 
                    order.PaymentStatus == PaymentStatuses.DelayedPayment && order.ApplicationUserId == userId),
                ApprovedCount = await _unitOfWork.OrderHeader.CountAsync(order => 
                    order.OrderStatus == OrderStatuses.Approved && order.ApplicationUserId == userId),
                InProcessCount = await _unitOfWork.OrderHeader.CountAsync(order => 
                    order.OrderStatus == OrderStatuses.InProcess && order.ApplicationUserId == userId),
            };

            if (string.IsNullOrEmpty(status))
            {
                orderListVM.OrderHeaders = await _unitOfWork.OrderHeader.GetAllAsync(order => order.ApplicationUserId == userId, includeProperties: "ApplicationUser");
            }
            else if (string.Equals(PaymentStatuses.DelayedPayment, status, StringComparison.OrdinalIgnoreCase))
            {
                orderListVM.OrderHeaders = await _unitOfWork.OrderHeader.GetAllAsync(order =>
                    order.PaymentStatus.ToLower() == status.ToLower() && order.ApplicationUserId == userId,     
                    includeProperties: "ApplicationUser");
            }
            else
            {
                orderListVM.OrderHeaders = await _unitOfWork.OrderHeader.GetAllAsync(order =>
                    order.OrderStatus.ToLower() == status.ToLower() && order.ApplicationUserId == userId, 
                    includeProperties: "ApplicationUser");
            }
            return View(orderListVM);
        }

        public async Task<IActionResult> Details(Guid orderId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            var orderHeader = await _unitOfWork.OrderHeader.GetAsync(order =>
                order.Id == orderId && order.ApplicationUserId == userId,
                includeProperties: "ApplicationUser");

            if (orderHeader != null)
            {
                OrderVM = new OrderVM()
                {
                    OrderHeader = orderHeader,
                    OrderDetails = await _unitOfWork.OrderDetail.GetAllAsync(detail => detail.OrderHeaderId == orderId,
                        includeProperties: "Product")
                };
                return View(OrderVM);
            }

            // Return not found here
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = ApplicationRoles.Company)]
        public async Task<IActionResult> PayNow()
        {
            OrderVM.OrderHeader = await _unitOfWork.OrderHeader.GetAsync(order =>
                order.Id == OrderVM.OrderHeader.Id, includeProperties: "ApplicationUser");
            OrderVM.OrderDetails = await _unitOfWork.OrderDetail.GetAllAsync(detail =>
                detail.OrderHeaderId == OrderVM.OrderHeader.Id, includeProperties: "Product");


            // Stripe logic payment for regular account
            var domain = Request.Scheme + "://" + Request.Host.Value;
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = $"{domain}/Customer/Order/PaymentConfirmation?orderId={OrderVM.OrderHeader.Id}",
                CancelUrl = $"{domain}/Customer/Order/Details?orderId={OrderVM.OrderHeader.Id}",
                //CancelUrl = $"{domain}/Customer/Order/Details?orderId={OrderVM.OrderHeader.Id}",
                LineItems = new List<Stripe.Checkout.SessionLineItemOptions>(),
                Mode = "payment",
            };

            foreach (var item in OrderVM.OrderDetails)
            {
                var sessionLineItem = new Stripe.Checkout.SessionLineItemOptions
                {
                    PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Product.Price * 100), // In cents
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
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.OrderHeader.UpdateStripePaymentIDAsync(OrderVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            // Redirect to payment page
            Response.Headers.Add("Location", session.Url);

            return new StatusCodeResult(303);
        }

        [HttpGet]
        [Authorize(Roles = ApplicationRoles.Company)]
        public async Task<IActionResult> PaymentConfirmation(Guid orderId)
        {
            var orderHeader = await _unitOfWork.OrderHeader.GetAsync(orderHeader => orderHeader.Id == orderId,
                includeProperties: "ApplicationUser");
            var domain = Request.Scheme + "://" + Request.Host.Value;

            if (orderHeader != null & orderHeader.PaymentStatus == PaymentStatuses.DelayedPayment)
            {
                // Order by company
                var service = new Stripe.Checkout.SessionService();
                Stripe.Checkout.Session session = service.Get(orderHeader.SessionId);

                if (string.IsNullOrEmpty(orderHeader.PaymentIntentId) && session.PaymentStatus.ToLower() == "paid")
                {
                    await _unitOfWork.BeginTransactionAsync();

                    await _unitOfWork.OrderHeader.UpdateStripePaymentIDAsync(orderId, session.Id, session.PaymentIntentId);
                    await _unitOfWork.OrderHeader.UpdateStatusAsync(orderId, orderHeader.OrderStatus, PaymentStatuses.Approved);
                    await _unitOfWork.SaveChangesAsync();

                    await _unitOfWork.CommitAsync();

                    return View(orderId);
                }

                return RedirectToAction(nameof(Details), new { orderId });
            }
            // Return not found here
            return RedirectToAction(nameof(Index));
        }
    }
}
