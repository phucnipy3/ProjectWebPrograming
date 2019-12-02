using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class CartItem
    {
        public Product Product { get; set; }

        public int? Count { get; set; }

        public string StringSubTotal { get { return ((decimal)(Count * Product.PromotionPrice)).ToString("N0"); } }

        public string ProductPrice { get { return ((decimal)Product.PromotionPrice).ToString("N0"); } }
    }
}