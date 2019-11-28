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
            decimal? sum = 0;
            foreach(var item in Items)
            {
                sum += item.SubTotal();
            }
            return sum;
        }
    }
}