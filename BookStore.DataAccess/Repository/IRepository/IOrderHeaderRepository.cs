using BookStore.Models;
using System.Linq.Expressions;

namespace BookStore.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);
        Task UpdateStatusAsync(Guid id, string orderStatus, string? paymentStatus = null);
        Task UpdateStripePaymentIDAsync(Guid id, string sessionId, string paymentIntentId);

        Task<int> CountAsync(Expression<Func<OrderHeader, bool>>? filter = null);
        Task<double> SumOrderTotalAsync(Expression<Func<OrderHeader, bool>>? filter = null);
    }
}
