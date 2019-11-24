using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class ProductViewModel
    {
        public PagedList.IPagedList<MVC.Models.ProductOnPage> Products { get; set; }
        public IEnumerable<ProductCategory> Categories { get; set; }
    }
}

