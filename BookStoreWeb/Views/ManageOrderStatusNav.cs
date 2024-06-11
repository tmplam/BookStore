using BookStore.Utility.StaticDetails;

#nullable disable

namespace BookStoreWeb.Views
{
    public static class ManageOrderStatusNav
    {
        public static string AllNavClass(HttpContext httpContext) => OrderStatusNavClass(httpContext, null);
        public static string DelayedPaymentNavClass(HttpContext httpContext) => OrderStatusNavClass(httpContext, PaymentStatuses.DelayedPayment);
        public static string ApprovedNavClass(HttpContext httpContext) => OrderStatusNavClass(httpContext, OrderStatuses.Approved);
        public static string InProcessNavClass(HttpContext httpContext) => OrderStatusNavClass(httpContext, OrderStatuses.InProcess);
        public static string ShippedNavClass(HttpContext httpContext) => OrderStatusNavClass(httpContext, OrderStatuses.Shipped);
        public static string CancelledNavClass(HttpContext httpContext) => OrderStatusNavClass(httpContext, OrderStatuses.Cancelled);

        public static string OrderStatusNavClass(HttpContext httpContext, string status)
        {
            var activeTab = httpContext.Request.Query["status"];
            return string.Equals(activeTab, status, StringComparison.OrdinalIgnoreCase) ? "active" : "";
        }
    }
}
