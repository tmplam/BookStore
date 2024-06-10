using BookStore.Models;

namespace BookStore.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);
        Task UpdateStatus(Guid id, string orderStatus, string? paymentStatus = null);
        Task UpdateStripePaymentID(Guid id, string sessionId, string paymentIntentId);
    }
}
