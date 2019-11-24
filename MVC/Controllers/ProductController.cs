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
        public ActionResult Index(string searchString, string metaTitle = "Iphone-cases", int page = 1, int sizePage = 9)
        {
            ViewBag.CategoryActive = "#"+metaTitle+"";
            ViewBag.SearchString = searchString;
            ViewBag.MetaTitle = metaTitle;
            IPagedList<ProductOnPage> products = ProductHelper.GetProductsByCategoryMetaTitle(metaTitle).OrderByDescending(x => x.ProductID).ToPagedList(page,sizePage);
            IEnumerable<ProductCategory> categories = ProductCategoryHelper.GetProductCategories();
            ProductViewModel model = new ProductViewModel() { Products = products, Categories = categories };
            return View(model);
        }

        //public ActionResult Hot(int page = 1, int sizePage = 9)
        //{
        //    ViewBag.CategoryActive = "#IphoneCategory";
        //    List<ProductViewModel> listProduct = new List<ProductViewModel>();
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    IEnumerable<ProductViewModel> models = listProduct.OrderBy(x => x.Price).ToPagedList(page, sizePage);
        //    return View("Index",models);
        //}
        //public ActionResult Trend(int page = 1, int sizePage = 9)
        //{
        //    ViewBag.CategoryActive = "#TrendCategory";
        //    List<ProductViewModel> listProduct = new List<ProductViewModel>();
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    IEnumerable<ProductViewModel> models = listProduct.OrderBy(x => x.Price).ToPagedList(page, sizePage);
        //    return View("Index", models);
        //}
        //public ActionResult Discount(int page = 1, int sizePage = 9)
        //{
        //    ViewBag.CategoryActive = "#DiscountCategory";
        //    List<ProductViewModel> listProduct = new List<ProductViewModel>();
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    IEnumerable<ProductViewModel> models = listProduct.OrderBy(x => x.Price).ToPagedList(page, sizePage);
        //    return View("Index", models);
        //}
        //public ActionResult Gentlement(int page = 1, int sizePage = 9)
        //{
        //    ViewBag.CategoryActive = "#GentlementCategory";
        //    List<ProductViewModel> listProduct = new List<ProductViewModel>();
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    IEnumerable<ProductViewModel> models = listProduct.OrderBy(x => x.Price).ToPagedList(page, sizePage);
        //    return View("Index", models);
        //}
        //public ActionResult Kawaii(int page = 1, int sizePage = 9)
        //{
        //    ViewBag.CategoryActive = "#KawaiiCategory";
        //    List<ProductViewModel> listProduct = new List<ProductViewModel>();
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    IEnumerable<ProductViewModel> models = listProduct.OrderBy(x => x.Price).ToPagedList(page, sizePage);
        //    return View("Index", models);
        //}
        //public ActionResult Marvel(int page = 1, int sizePage = 9)
        //{
        //    ViewBag.CategoryActive = "#MarvelCategory";
        //    List<ProductViewModel> listProduct = new List<ProductViewModel>();
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    IEnumerable<ProductViewModel> models = listProduct.OrderBy(x => x.Price).ToPagedList(page, sizePage);
        //    return View("Index", models);
        //}

        //public ActionResult HighQuality(int page = 1, int sizePage = 9)
        //{
        //    ViewBag.CategoryActive = "#HighQualityCategory";
        //    List<ProductViewModel> listProduct = new List<ProductViewModel>();
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    IEnumerable<ProductViewModel> models = listProduct.OrderBy(x => x.Price).ToPagedList(page, sizePage);
        //    return View("Index", models);
        //}
        //public ActionResult Laser(int page = 1, int sizePage = 9)
        //{
        //    ViewBag.CategoryActive = "#LaserCategory";
        //    List<ProductViewModel> listProduct = new List<ProductViewModel>();
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 25000 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    listProduct.Add(new ProductViewModel { Price = 50000, ProductName = "Op lung sieu xin", PromotionPrice = 0 });
        //    IEnumerable<ProductViewModel> models = listProduct.OrderBy(x => x.Price).ToPagedList(page, sizePage);
        //    return View("Index", models);
        //}
        public ActionResult Search(string searchString)
        {

            return Redirect("/Product");
        }
        public ActionResult Detail()
        {
            return View();
        }
    }
}