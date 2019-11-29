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
            if (Product.PromotionPrice != Product.Price)
                return Count * Product.PromotionPrice;
            else
                return Count * Product.Price;
        }
        public string StringSubTotal
        {
            get
            {
                return ((decimal)SubTotal()).ToString("N0");
            }
        }
        public string ProductPrice {
            get
            {
                if (Product.PromotionPrice != Product.Price)
                    return ((Decimal)Product.PromotionPrice).ToString("N0");
                else
                    return ((Decimal)Product.Price).ToString("N0");
            }
        }
    }
}