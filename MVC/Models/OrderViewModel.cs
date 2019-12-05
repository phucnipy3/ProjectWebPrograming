using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class OrderManagementViewModel
    {
        public OrderViewModel OrderViewModel { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }
    }

    public class OrderViewModel
    {
        public int ID { get; set; }

        public string ShipName { get; set; }

        public string ShipMobile { get; set; }

        public string ShipAddress { get; set; }

        public string Status { get; set; }

        public List<ProductOnOrder> Products { get; set; }

        public List<TimeLogs> TimeLogs { get; set; }

        public string TotalProductMoney { get; set; }

        public string Transport { get; set; }

        public string TransportationFee { get; set; }

        public string PaymentMethods { get; set; }

        public string TotalMoney { get; set; }

        public string Tag { get; set; }

        public DateTime? CreateDate { get; set; }

        public string AllNames
        {
            get
            {
                string stringBuilder = "";
                Products.ForEach(x => stringBuilder += x.Name.ToString() + " ");
                return stringBuilder;
            }
        }
    }

    public class ProductOnOrder
    {
        public int ID { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public int? Count { get; set; }

        public string Price { get; set; }

        public string PromotionPrice { get; set; }
    }

    public class TimeLogs
    {
        public DateTime? Timeline { get; set; }

        public string Event { get; set; }
    }
}