using MVC.Areas.Admin.Code;
using MVC.Areas.Admin.Models;
using MVC.Controllers;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MVC.Areas.Admin.Controllers
{

    [Authorize(Roles ="Admin,Employee")]
    public class OrdersController : ApplicationController,IAdminController<Order>
    {
        public ActionResult Add(Order model)
        {
            if (ModelState.IsValid)
            {
                if (OrderHelper.AddOrder(model))
                {
                    return Content("success");
                }
            }
            return PartialView("Add", model);
        }
        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            if (OrderHelper.DeleteOrder(model.ID))
                return Content("success");
            return Content("failure");
        }
        [HttpPost]
        public ActionResult DeleteTransfer(int? id)
        {
            return PartialView("ConfirmDelete", new DeleteViewModel() { ID = (int)id });
        }

        // GET: Admin/Orders


        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            ViewBag.Active = "#Orders";
            IEnumerable<Order> models;
            if (String.IsNullOrEmpty(searchString))
                models = OrderHelper.GetOrders();
            else
            {
                models = OrderHelper.GetOrders(searchString);
            }
            ViewBag.SearchString = searchString;
            return View(models.OrderByDescending(x => x.ID).ToPagedList(page, pageSize));
        }
        [HttpPost]
        public ActionResult Update(Order model)
        {
            if (OrderHelper.UpdateOder(model))
                return Content("success");
            return Content("failure");
        }
        [HttpPost]
        public ActionResult UpdateTransfer(int? id)
        {
            Order model = OrderHelper.GetOrderByID((int)id);
            return PartialView("Update", model);
        }
    }
}