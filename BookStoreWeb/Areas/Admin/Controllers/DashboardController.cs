using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models.ViewModels;
using BookStore.Utility.StaticDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var dashboardVM = new DashboardVM
            {
                YearEarning = await _unitOfWork.OrderHeader.SumOrderTotalAsync(order =>
                    order.OrderDate.Year == DateTime.Now.Year && order.PaymentStatus == PaymentStatuses.Approved),
                MonthEarning = await _unitOfWork.OrderHeader.SumOrderTotalAsync(order =>
                    order.OrderDate.Year == DateTime.Now.Year
                    && order.OrderDate.Month == DateTime.Now.Month
                    && order.PaymentStatus == PaymentStatuses.Approved),
                MonthOrder = await _unitOfWork.OrderHeader.CountAsync(order =>
                    order.OrderDate.Month == DateTime.Now.Month),
                MonthsEarning = new List<double>(),
                YearsEarning = new List<double>(),
                BestSellings = _unitOfWork.OrderDetail.GetBestSellingsThisMonth()
            };

            // Get earning for each month
            for (int i = 1; i <= 12; i++)
            {
                var month = i;
                var monthEarning = await _unitOfWork.OrderHeader.SumOrderTotalAsync(order =>
                    order.OrderDate.Year == DateTime.Now.Year
                    && order.OrderDate.Month == month
                    && order.PaymentStatus == PaymentStatuses.Approved);
                dashboardVM.MonthsEarning.Add(monthEarning);
            }

            // Get earning for each year
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear - 4; i <= currentYear; i++)
            {
                var year = i;
                var yearEarning = await _unitOfWork.OrderHeader.SumOrderTotalAsync(order =>
                    order.OrderDate.Year == year
                    && order.PaymentStatus == PaymentStatuses.Approved);
                dashboardVM.YearsEarning.Add(yearEarning);
            }

            return View(dashboardVM);
        }
    }
}
