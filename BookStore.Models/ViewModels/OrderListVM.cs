namespace BookStore.Models.ViewModels
{
    public class OrderListVM
    {
        public IEnumerable<OrderHeader> OrderHeaders { get; set; }
        public int PaymentPendingCount { get; set; }
        public int ApprovedCount { get; set; }
        public int InProcessCount { get; set; }
    }
}
