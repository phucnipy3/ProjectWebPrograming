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
        public ActionResult Index(string searchString, string metaTitle = "Iphone-cases", int page = 1, int sizePage = 2)
        {
            ViewBag.CategoryActive = "#"+metaTitle+"";
            ViewBag.SearchString = searchString;
            ViewBag.MetaTitle = metaTitle;
            IPagedList<ProductOnPage> products;
            IEnumerable<ProductCategory> categories;
            if (String.IsNullOrEmpty(searchString))
            {
                products = ProductHelper.GetProductsByCategoryMetaTitle(metaTitle).OrderByDescending(x => x.ProductID).ToPagedList(page, sizePage);
                categories = ProductCategoryHelper.GetProductCategories();
            }
            else
            {
                // Thiếu searchString
                products = ProductHelper.GetProductsByCategoryMetaTitle(metaTitle).OrderByDescending(x => x.ProductID).ToPagedList(page, sizePage);
                categories = ProductCategoryHelper.GetProductCategories();
            }
            ProductViewModel model = new ProductViewModel() { Products = products, Categories = categories };
            return View(model);
        }

        
        public ActionResult Detail(int id = 1)
        {
            ViewBag.RatePoint = 2;
            return View(ProductHelper.GetProductDetail(id));
        }

        [HttpPost]
        public ActionResult Rate(int id, int point)
        {

            return Content("success");
        }
        [HttpPost]
        public ActionResult Comment(int idProduct, string content)
        {
            return Content("success");
        }
        [HttpPost]
        public ActionResult Reply(int idProduct, string content,int idParent)
        {
            return Content("success");
        }
    }
}