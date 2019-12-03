using MVC.Areas.Admin.Code;
using MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using MVC.Areas.Admin.Models;
using PagedList;
namespace MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class ProductDetailsController : ApplicationController,IDetailsController<ProductDetail>
    {
        public static ProductDetail currentDetail = new ProductDetail();

        [HttpPost]
        public ActionResult Add(ProductDetail model)
        {
            if (ModelState.IsValid)
            {

                if (ProductDetailHelper.AddProductDetail(model))
                {
                    return Content("success");
                }
                else
                {
                    ModelState.AddModelError("", "Existing product detail!");
                    return PartialView("Add");
                }
            }
            else
            {
                return PartialView("Add");
            }
        }
       
        
        [HttpPost]
        public ActionResult Delete(DeleteDetailViewModel model)
        {
            if (ProductDetailHelper.DeleteProductDetail(model.MainID,model.SubID))
                return Content("success");
            return Content("failure");
        }
        [HttpPost]
        public ActionResult DeleteTransfer(int? productID,int?  productCategoryID)
        {
            return PartialView("ConfirmDelete", new DeleteDetailViewModel() { SubID = (int)productCategoryID,MainID = (int)productID });
        }

        

        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            ViewBag.Active = "#ProductDetails";
            IEnumerable<ProductDetail> models;
            if (String.IsNullOrEmpty(searchString))
                models = ProductDetailHelper.GetProductDetails();
            else
            {
                models = ProductDetailHelper.GetProductDetails(searchString);
            }
            ViewBag.SearchString = searchString;
            return View(models.OrderByDescending(x => x.ProductID).ToPagedList(page, pageSize));
        }
        [HttpPost]
        public ActionResult Update(ProductDetail model)
        {
            if (ProductDetailHelper.DeleteProductDetail(currentDetail) && ProductDetailHelper.AddProductDetail(model))
                return Content("success");
            return Content("failure");
        }
        
        [HttpPost]
        public ActionResult UpdateTransfer(int? productID, int? productCategoryID)
        {
            ProductDetail model = ProductDetailHelper.GetProductDetailsByID((int)productID,(int)productCategoryID);
            currentDetail = model;
            return PartialView("Update", model);
        }
    }
}