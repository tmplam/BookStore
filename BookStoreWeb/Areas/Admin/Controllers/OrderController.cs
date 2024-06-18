using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Utility.StaticDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#nullable disable

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{ApplicationRoles.Admin},{ApplicationRoles.Employee}")]
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
            OrderListVM orderListVM = new OrderListVM
            {
                PaymentPendingCount = await _unitOfWork.OrderHeader.CountAsync(order =>
                    order.PaymentStatus == PaymentStatuses.DelayedPayment),
                ApprovedCount = await _unitOfWork.OrderHeader.CountAsync(order =>
                    order.OrderStatus == OrderStatuses.Approved),
                InProcessCount = await _unitOfWork.OrderHeader.CountAsync(order =>
                    order.OrderStatus == OrderStatuses.InProcess),
            };

            if (string.IsNullOrEmpty(status))
            {
                orderListVM.OrderHeaders = await _unitOfWork.OrderHeader.GetAllAsync(includeProperties: "ApplicationUser");
            }
            else if (string.Equals(PaymentStatuses.DelayedPayment , status, StringComparison.OrdinalIgnoreCase))
            {
                orderListVM.OrderHeaders = await _unitOfWork.OrderHeader.GetAllAsync(order =>
                    order.PaymentStatus.ToLower() == status.ToLower(), includeProperties: "ApplicationUser");
            }
            else
            {
                orderListVM.OrderHeaders = await _unitOfWork.OrderHeader.GetAllAsync(order =>
                    order.OrderStatus.ToLower() == status.ToLower(), includeProperties: "ApplicationUser");
            }
            return View(orderListVM);
        }

        public async Task<IActionResult> Details(Guid orderId)
        {
            OrderVM = new OrderVM()
            {
                OrderHeader = await _unitOfWork.OrderHeader.GetAsync(order => order.Id == orderId,
                    includeProperties: "ApplicationUser"),
                OrderDetails = await _unitOfWork.OrderDetail.GetAllAsync(detail => detail.OrderHeaderId == orderId,
                    includeProperties: "Product")
            };
            return View(OrderVM);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderDetail()
        {
            var orderHeaderFromDB = await _unitOfWork.OrderHeader.GetAsync(order => order.Id == OrderVM.OrderHeader.Id);
            orderHeaderFromDB.Name = OrderVM.OrderHeader.Name;
            orderHeaderFromDB.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderHeaderFromDB.Address = OrderVM.OrderHeader.Address;

            if (!string.IsNullOrEmpty(OrderVM.OrderHeader.Carrier))
            {
                orderHeaderFromDB.Carrier = OrderVM.OrderHeader.Carrier;
            }

            if (!string.IsNullOrEmpty(OrderVM.OrderHeader.TrackingNumber))
            {
                orderHeaderFromDB.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            }

            await _unitOfWork.BeginTransactionAsync();

            _unitOfWork.OrderHeader.Update(orderHeaderFromDB);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitAsync();

            TempData["successMessage"] = "Order details updated successfully";

            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }

        [HttpPost]
        public async Task<IActionResult> StartProcessing()
        {
            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.OrderHeader.UpdateStatusAsync(OrderVM.OrderHeader.Id, OrderStatuses.InProcess);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitAsync();

            TempData["successMessage"] = "Started processing order";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }

        [HttpPost]
        public async Task<IActionResult> ShipOrder()
        {
            var orderHeaderFromDB = await _unitOfWork.OrderHeader.GetAsync(order => order.Id == OrderVM.OrderHeader.Id);
            orderHeaderFromDB.Carrier = OrderVM.OrderHeader.Carrier;
            orderHeaderFromDB.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            orderHeaderFromDB.OrderStatus = OrderStatuses.Shipped;
            orderHeaderFromDB.ShippingDate = DateTime.Now;

            if (orderHeaderFromDB.PaymentStatus == PaymentStatuses.DelayedPayment)
            {
                orderHeaderFromDB.PaymentDueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
            }

            await _unitOfWork.BeginTransactionAsync();

            _unitOfWork.OrderHeader.Update(orderHeaderFromDB);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitAsync();

            TempData["successMessage"] = "Started shipping order";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }

        [HttpPost]
        public async Task<IActionResult> CancelOrder()
        {
            var orderHeaderFromDB = await _unitOfWork.OrderHeader.GetAsync(order => order.Id == OrderVM.OrderHeader.Id);

            await _unitOfWork.BeginTransactionAsync();

            if (orderHeaderFromDB.PaymentStatus == PaymentStatuses.Approved)
            {
                var options = new Stripe.RefundCreateOptions
                {
                    Reason = Stripe.RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderHeaderFromDB.PaymentIntentId,
                };

                var service = new Stripe.RefundService();
                Stripe.Refund refund = await service.CreateAsync(options);

                await _unitOfWork.OrderHeader.UpdateStatusAsync(orderHeaderFromDB.Id, OrderStatuses.Cancelled, PaymentStatuses.Refunded);
            }
            else
            {
                await _unitOfWork.OrderHeader.UpdateStatusAsync(orderHeaderFromDB.Id, OrderStatuses.Cancelled, PaymentStatuses.Cancelled);
            }
            
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitAsync();

            TempData["successMessage"] = "Cancelled orer";
            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }
    }
}
