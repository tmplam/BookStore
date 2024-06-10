using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Utility.StaticDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string? status)
        {
            IEnumerable<OrderHeader> orderHeaders;

            if (string.IsNullOrEmpty(status))
            {
                orderHeaders = await _unitOfWork.OrderHeader.GetAllAsync(includeProperties: "ApplicationUser");
            }
            else if (string.Equals(PaymentStatuses.PaymentStatusDelayedPayment, status, StringComparison.OrdinalIgnoreCase))
            {
                orderHeaders = await _unitOfWork.OrderHeader.GetAllAsync(order => 
                    order.PaymentStatus.ToLower() == status.ToLower(), includeProperties: "ApplicationUser");
            }
            else
            {
                orderHeaders = await _unitOfWork.OrderHeader.GetAllAsync(order =>
                    order.OrderStatus.ToLower() == status.ToLower(), includeProperties: "ApplicationUser");
            }
            return View(orderHeaders);
        }
    }
}
