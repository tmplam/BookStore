using BookStore.Models;
using BookStore.Models.UtilityModels;

namespace BookStore.DataAccess.Repository.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        void Update(OrderDetail orderDetail);
        IEnumerable<BestSelling> GetBestSellingsThisMonth(int? topN = null);
    }
}
