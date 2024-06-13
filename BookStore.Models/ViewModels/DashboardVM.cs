#nullable disable

using BookStore.Models.UtilityModels;

namespace BookStore.Models.ViewModels
{
    public class DashboardVM
    {
        public double YearEarning { get; set; }
        public double MonthEarning { get; set; }
        public int MonthOrder { get; set; }
        public List<double> MonthsEarning { get; set; }
        public List<double> YearsEarning { get; set; }
        public IEnumerable<BestSelling> BestSellings { get; set; }
    }
}
