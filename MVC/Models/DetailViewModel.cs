using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
namespace MVC.Models
{
    public class DetailViewModel
    {
        public List<CommentView> Comments { get; set; }

        public RateView Rate { get; set; }

        public List<ProductOnPage> RelateProducts { get; set; }

        public MainProductDetail MainProduct { get; set; }
    }

    public class MainProductDetail
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Image { get; set; }

        public string Price { get; set; }

        public string PromotionPrice { get; set; }

        public bool? IncludedVAT { get; set; }

        public int? Quantity { get; set; }

        public List<string> Detail { get; set; }

        public string SavePersent { get; set; }
    }

    public class RateView
    {
        public double? RatePoint { get; set; }

        public List<int> PercentPoint { get; set; }
    }

    public class CommentView
    {
        public Comment Comment { get; set; }

        public List<CommentView> ReplyComment { get; set; }
    }
}