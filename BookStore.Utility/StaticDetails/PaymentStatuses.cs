namespace BookStore.Utility.StaticDetails
{
    public static class PaymentStatuses
    {
        public static string Pending => "Pending";
        public static string Approved => "Approved";
        public static string DelayedPayment => "ApprovedForDelayedPayment";
        public static string Rejected => "Rejected";
        public static string Refunded => "Refunded";
        public static string Cancelled => "Cancelled";
    }
}
