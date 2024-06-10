using BookStore.Utility.StaticDetails;

#nullable disable

namespace BookStoreWeb.Areas.Admin.Views.Order
{
    public static class ManageOrderStatusNav
    {
        public static string AllNavClass(HttpContext httpContext) => OrderStatusNavClass(httpContext, null);
        public static string DelayedPaymentNavClass(HttpContext httpContext) => OrderStatusNavClass(httpContext, PaymentStatuses.PaymentStatusDelayedPayment);
        public static string ApprovedNavClass(HttpContext httpContext) => OrderStatusNavClass(httpContext, OrderStatuses.StatusApproved);
        public static string InProcessNavClass(HttpContext httpContext) => OrderStatusNavClass(httpContext, OrderStatuses.StatusInProcess);
        public static string ShippedNavClass(HttpContext httpContext) => OrderStatusNavClass(httpContext, OrderStatuses.StatusShipped);
        public static string RefundedNavClass(HttpContext httpContext) => OrderStatusNavClass(httpContext, OrderStatuses.StatusRefunded);
        public static string CancelledNavClass(HttpContext httpContext) => OrderStatusNavClass(httpContext, OrderStatuses.StatusCancelled);

        public static string OrderStatusNavClass(HttpContext httpContext, string status)
        {
            var activeTab = httpContext.Request.Query["status"];
            return string.Equals(activeTab, status, StringComparison.OrdinalIgnoreCase) ? "active" : "";
        }
    }
}
