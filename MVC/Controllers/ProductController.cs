using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using PagedList.Mvc;
using PagedList;

namespace MVC.Controllers
{
    public class ProductController : ApplicationController
    {
        // GET: Product
        public ActionResult Index(string searchString, string metaTitle = "Iphone-cases", int page = 1, int sizePage = 10)
        {
            ViewBag.CategoryActive = "#"+metaTitle+"";
            ViewBag.SearchString = searchString;
            ViewBag.MetaTitle = metaTitle;
            IPagedList<ProductOnPage> products;
            IEnumerable<ProductCategory> categories = ProductCategoryHelper.GetProductCategories();
            if (metaTitle == "Lasted")
            {
                products = ProductHelper.GetNewProducts().ToPagedList(page, sizePage);
            }
            else if (metaTitle == "Best-Saler")
            {
                products = ProductHelper.GetBestSalerProducts().ToPagedList(page, sizePage);
            }
            else if (metaTitle == "Hot")
            {
                products = ProductHelper.GetHotProducts().ToPagedList(page, sizePage);
            }
            else
            {
                products = ProductHelper.GetProductsByCategoryMetaTitle(metaTitle).OrderByDescending(x => x.ProductID).ToPagedList(page, sizePage);
            }

            ProductViewModel model = new ProductViewModel() { Products = products, Categories = categories };
            return View(model);
        }

        
        public ActionResult Detail(int id = 1)
        {

            ViewBag.RatePoint = RateHelper.GetRatePoint(HttpContext.User.Identity.Name,id);
            List<ProductOnPage> recentProducts = HttpContext.Session["RecentProducts"] as List<ProductOnPage>;
            if (recentProducts == null)
                recentProducts = new List<ProductOnPage>();
            recentProducts.Add(ProductHelper.GetProductOnPages().Where(x => x.ProductID == id).SingleOrDefault());
            HttpContext.Session["RecentProducts"] = recentProducts;
            return View(ProductHelper.GetProductDetail(id));
        }
        [Authorize]
        [HttpPost]
        public ActionResult Rate(int id, int point)
        {
            int Userid = UserHelper.GetUserByUserID(HttpContext.User.Identity.Name).ID;
            Rate rate = new Rate() { ProductID = id, RatePoint = point,CreateBy = Userid };
            RateHelper.AddRate(rate);
            return Content("success");
        }
        [Authorize]
        [HttpPost]
        public ActionResult Comment(int idProduct, string content)
        {
            int id = UserHelper.GetUserByUserID(HttpContext.User.Identity.Name).ID;
            Comment cmt = new Comment() { ProductID = idProduct, Content = content, CreateBy = id };
            CommentHelper.AddComment(cmt);
            return Redirect("/Product/Detail/" + idProduct.ToString());
        }
        [Authorize]
        [HttpPost]
        public ActionResult Reply(int idProduct, string content,int idParent)
        {
            int id = UserHelper.GetUserByUserID(HttpContext.User.Identity.Name).ID;
            Comment cmt = new Comment() { ProductID = idProduct, Content = content, ParentID = idParent, CreateBy = id };
            CommentHelper.AddComment(cmt);
            return Redirect("/Product/Detail/" + idProduct.ToString());
        }
    }
}