using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
namespace MVC.Models
{
    public class DetailViewModel
    {
        public PagedList<Comment> Comments { get; set; }
        public Rate Rate { get; set; }
        public List<ProductOnPage> RelateProducts { get; set; }
        public Product MainProduct { get; set; }
    }
}