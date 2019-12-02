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


namespace MVC.Areas.Admin.Controllers
{
    public class ProductsController : ApplicationController, IAdminController<Product>
    {
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            IPagedList<Product> model = ProductHelper.GetProducts().OrderBy(x => x.ID).ToPagedList(page, pageSize);
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(Product model)
        {
            if(ModelState.IsValid)
            {
                if(ProductHelper.AddProduct(model as Product))
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

        public ActionResult Update(Product model)
        {
            if (ProductHelper.UpdateProduct(model as Product  ))
                return Content("success");
            return Content("failure");
        }

        [HttpPost]

        public ActionResult UpdateTransfer(int? id)
        {
            User model = UserHelper.GetUserByID((int)id);
            return PartialView("Update", model);
        }

    }
}