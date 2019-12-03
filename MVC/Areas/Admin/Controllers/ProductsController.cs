using MVC.Areas.Admin.Code;
using MVC.Areas.Admin.Models;
using MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using MVC.Models;
using System.IO;

namespace MVC.Areas.Admin.Controllers
{
    public class ProductsController : ApplicationController
    {
        [Authorize(Roles = "Admin,Employee")]

        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            ViewBag.Active = "#Products";
            IEnumerable<Product> models;
            if (String.IsNullOrEmpty(searchString))
                models = ProductHelper.GetProducts();
            else
            {
                models = ProductHelper.GetProducts(searchString);
            }
            ViewBag.SearchString = searchString;
            return View(models.OrderByDescending(x => x.ID).ToPagedList(page, pageSize));
        }
        [HttpPost]
        public ActionResult Add(Product model, HttpPostedFileBase Hinh)
        {
            if(ModelState.IsValid)
            {
                string fileName = Helper.ConvertMetaTitle(model.Name) + DateTime.Now.ToString("-HH-mm-ss-dd-MM-yyyy") + Path.GetExtension(Hinh.FileName);
                model.Image = "~/Resource/chitiet/" + fileName;
                string savePath = Path.Combine(Server.MapPath("~/Resource/chiet/"), fileName);
                Hinh.SaveAs(savePath);
                if (ProductHelper.AddProduct(model))
                { 
                    return Content("success");
                }
            }
            return PartialView("Add", model as Product);
        }
        [HttpPost]


        public ActionResult Delete(DeleteViewModel model)
        {
            if (ProductHelper.DeleteProduct(model.ID))
                return Content("success");
            return Content("failure");
        }
        [HttpPost]


        public ActionResult DeleteTransfer(int? id)
        {
            return PartialView("ConfirmDelete", new DeleteViewModel() { ID = (int)id });
        }


        
        [HttpPost]
        public ActionResult Update(Product model, HttpPostedFileBase Hinh)
        {
            if(Hinh != null)
            {
                string fileName = Helper.ConvertMetaTitle(model.Name) + DateTime.Now.ToString("-HH-mm-ss-dd-MM-yyyy") + Path.GetExtension(Hinh.FileName);
                model.Image = "~/Resource/chitiet/" + fileName;
                string savePath = Path.Combine(Server.MapPath("~/Resource/chitet/"), fileName);
                Hinh.SaveAs(savePath);
            }
            if (ProductHelper.UpdateProduct(model))
                return Content("success");
            return Content("failure");
        }

        [HttpPost]

        public ActionResult UpdateTransfer(int? id)
        {
            Product model = ProductHelper.GetProductByID((int)id);
            return PartialView("Update", model);
        }

    }
}