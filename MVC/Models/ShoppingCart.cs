using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; }

        public int Count
        {
            get
            {
                return Items.Count;
            }
        }

        public decimal? GrantTotal()
        {
            return Items.Sum(x => x.Count * x.Product.PromotionPrice);
        }

        public string StringGrantTotal
        {
            get
            {
                return ((decimal)GrantTotal()).ToString("N0");
            }
        }
    }
}