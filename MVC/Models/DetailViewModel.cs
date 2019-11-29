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
    }

    public class RateView
    {
        public double? RatePoint { get; set; }

        public double? PercentOnePoint { get; set; }

        public double? PercentTwoPoint { get; set; }

        public double? PercentThreePoint { get; set; }

        public double? PercentFourPoint { get; set; }

        public double? PercentFivePoint { get; set; }
    }

    public class CommentView
    {
        public Comment Comment { get; set; }

        public List<CommentView> ReplyComment { get; set; }
    }
}