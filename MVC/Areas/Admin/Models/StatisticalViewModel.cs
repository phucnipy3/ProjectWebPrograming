using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Admin.Models
{
    public class StatisticalViewModel
    {
        public List<ChartData> AmountSoldAndRevenueByMonth { get; set; }

        public List<ChartData> AmountSoldAndRevenueByQuarters { get; set; }

        public List<ChartData> AmountSoldAndRevenueByYear { get; set; }

        public List<ChartData> AmountSoldAndRevenueByCategory { get; set; }

        public ChartData AmountBillByMonth { get; set; }

        public ChartData AmountBillByYear { get; set; }

        public ChartData AmountBillByQuarters { get; set; }
    }

    public class ChartData
    {
        public string Name { get; set; }

        public List<string> Labels { get; set; }

        public List<decimal?> Values { get; set; }

        public string Dram { get; set; }

        public decimal? Total { get; set; }
    }

}