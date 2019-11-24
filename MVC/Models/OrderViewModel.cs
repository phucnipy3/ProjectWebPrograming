using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class OrderViewModel
    {
        public int ID { get; set; }

        public string ShipName { get; set; }

        public string ShipMobile { get; set; }

        public string ShipAddress { get; set; }

        public string Status { get; set; }

        public List<ProductOnOrder> Products { get; set; }

        public List<TimeLogs> TimeLogs { get; set; }

        public decimal? TotalProductMoney { get; set; }

        public string Transport { get; set; }

        public decimal? TransportationFee { get; set; }

        public string PaymentMethods { get; set; }

        public decimal? TotalMoney { get; set; }

    }

    public class ProductOnOrder
    {
        public string Image { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public decimal? Price { get; set; }

        public decimal? PromotionPrice { get; set; }
    }

    public class TimeLogs
    {
        public DateTime? Timeline { get; set; }

        public string Event { get; set; }
    }
}