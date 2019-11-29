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

        public Product MainProduct { get; set; }

        public List<string> DetailOfMainProduct { get { return MainProduct.Detail.Split(';').ToList(); } }

        public string SavePersent
        {
            get
            {
                return (1 - MainProduct.PromotionPrice * 100 / MainProduct.Price) + "%";
            }
        }
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