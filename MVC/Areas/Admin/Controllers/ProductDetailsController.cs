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
    [Authorize(Roles ="Admin,Employee")]
    public class ProductDetailsController : ApplicationController,IAdminController<ProductDetail>
    {
        
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
                    ModelState.AddModelError("", "Existing User ID!");
                    return PartialView("Add");
                }
            }
            else
            {
                return PartialView("Add");
            }
        }

        public ActionResult Delete(DeleteViewModel model)
        {
            throw new NotImplementedException();
        }
        public ActionResult Delete(DeleteProductDetailViewModel model)
        {
            if (ProductDetailHelper.DeleteProductDetail(model.ProductID,model.ProductCategoryID))
                return Content("success");
            return Content("failure");
        }
        public ActionResult DeleteTransfer(int? productID,int?  productCategoryID)
        {
            return PartialView("ConfirmDelete", new DeleteProductDetailViewModel() { ProductCategoryID = (int)productCategoryID,ProductID = (int)productID });
        }

        public ActionResult DeleteTransfer(int? id)
        {
            throw new NotImplementedException();
        }

        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
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

        public ActionResult Update(ProductDetail model)
        {
            if (ProductDetailHelper.AddProductDetail(model))
                return Content("success");
            return Content("failure");
        }

        public ActionResult UpdateTransfer(int? id)
        {
            throw new NotImplementedException();
        }
        public ActionResult UpdateTransfer(int? productID, int? productCategoryID)
        {
            ProductDetail model = ProductDetailHelper.GetProductDetails().FirstOrDefault();
            return PartialView("Update", model);
        }
    }
}