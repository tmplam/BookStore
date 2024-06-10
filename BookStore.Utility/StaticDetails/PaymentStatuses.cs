namespace BookStore.Utility.StaticDetails
{
    public static class PaymentStatuses
    {
        public static string PaymentStatusPending => "Pending";
        public static string PaymentStatusApproved => "Approved";
        public static string PaymentStatusDelayedPayment => "ApprovedForDelayedPayment";
        public static string PaymentStatusRejected => "Rejected";
    }
}
