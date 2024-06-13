using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.UtilityModels;
using BookStore.Utility.StaticDetails;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(OrderDetail orderDetail)
        {
            _dbContext.OrderDetails.Update(orderDetail);
        }

        public IEnumerable<BestSelling> GetBestSellingsThisMonth(int? topN = null)
        {
            var bestSellings = _dbContext.OrderDetails
                .Include(detail => detail.Product)
                .Include(detail => detail.OrderHeader)
                .Where(detail => detail.OrderHeader.OrderDate.Month == DateTime.Now.Month
                    && detail.OrderHeader.OrderDate.Year == DateTime.Now.Year
                    && detail.OrderHeader.PaymentStatus == PaymentStatuses.Approved)
                .GroupBy(detail => detail.Product)
                .OrderByDescending(group => group.Sum(orderDetail => orderDetail.Quantity))
                .Select(group => new BestSelling
                {
                    Product = group.Key,
                    UnitsSold = group.Sum(detail => detail.Quantity)
                });


            if (topN != null)
            {
                bestSellings = bestSellings.Take((int) topN);
            }

            return bestSellings;
        }
    }
}
