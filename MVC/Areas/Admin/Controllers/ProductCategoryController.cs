using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using PagedList;
using PagedList.Mvc;
using MVC.Areas.Admin.Models;
using MVC.Areas.Admin.Code;
using MVC.Controllers;

namespace MVC.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductCategoryController : ApplicationController, IAdminController<ProductCategory>
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(string searchString, int page = 1, int sizePage = 10)
        {
            IEnumerable<ProductCategory> models;
            if (String.IsNullOrEmpty(searchString))
                models = ProductCategoryHelper.GetProductCategories();
            else
            {
                models = ProductCategoryHelper.GetProductCategories(searchString);
            }
            ViewBag.SearchString = searchString;
            return View(models.OrderByDescending(x => x.ID).ToPagedList(page, sizePage));
        }
        [HttpPost]
        public ActionResult Add(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                if (ProductCategoryHelper.AddProductCategory(model as ProductCategory))
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

        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            if (ProductCategoryHelper.DeleteProductCategory(model.ID))
                return Content("success");
            return Content("failure");
        }

        [HttpPost]
        public ActionResult DeleteTransfer(int? id)
        {
            return PartialView("ConfirmDelete", new DeleteViewModel() { ID = (int)id });
        }
        [HttpPost]
        public ActionResult UpdateTransfer(int? id)
        {
            ProductCategory model = ProductCategoryHelper.GetProductCategoryByID((int)id);
            return PartialView("Update", model);
        }

        [HttpPost]
        public ActionResult Update(ProductCategory model)
        {
            if (ProductCategoryHelper.UpdateProductCategory(model as ProductCategory))
                return Content("success");
            return Content("failure");
        }
    }
}