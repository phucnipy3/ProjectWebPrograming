using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class ProductOnPage
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string PromotionPrice { get; set; }
        public string ImageLink { get; set; }

        public string Tag { get; set; }
    }
}