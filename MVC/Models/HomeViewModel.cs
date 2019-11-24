using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class HomeViewModel
    {
        public List<ProductOnPage> ListHot { get; set; }
        public List<ProductOnPage> ListNew { get; set; }
        public List<ProductOnPage> ListBestSaler { get; set; }
    }
}