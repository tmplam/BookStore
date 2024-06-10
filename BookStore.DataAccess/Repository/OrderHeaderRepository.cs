﻿using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderHeaderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(OrderHeader orderHeader)
        {
            _dbContext.OrderHeaders.Update(orderHeader);
        }

        public async Task UpdateStatus(Guid id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = await _dbContext.OrderHeaders.FirstOrDefaultAsync(orderHeader => orderHeader.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;

                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }

        public async Task UpdateStripePaymentID(Guid id, string sessionId, string paymentIntentId)
        {
            var orderFromDb = await _dbContext.OrderHeaders.FirstOrDefaultAsync(orderHeader => orderHeader.Id == id);

            if (orderFromDb != null)
            {
                if (!string.IsNullOrEmpty(sessionId))
                {
                    orderFromDb.SessionId = sessionId;
                }

                if (!string.IsNullOrEmpty(paymentIntentId))
                {
                    orderFromDb.PaymentIntentId = paymentIntentId;
                    orderFromDb.PaymentDate = DateTime.Now;
                }
            }
        }
    }
}