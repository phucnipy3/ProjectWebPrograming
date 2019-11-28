using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Count { get; set; }
       
        public decimal? SubTotal()
        {
            if (Product.PromotionPrice != 0)
                return Count * Product.PromotionPrice;
            else
                return Count * Product.Price;
        }
    }
}