using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Admin.Models
{
    public class DeleteProductDetailViewModel
    {
        public int ProductID { get; set; }
        public int ProductCategoryID { get; set; }
    }
}