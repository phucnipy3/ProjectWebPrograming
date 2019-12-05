using MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using PagedList;
namespace MVC.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin,Employee")]
    public class ManageOrdersController : ApplicationController
    {
        // GET: Admin/ManageOrders
        public ActionResult Index(string searchString, string status = "All", int page = 1, int pageSize = 10)
        {
            ViewBag.Active = "#ManageOrders";
            IEnumerable<OrderManagementViewModel> models;
            models = OrderHelper.GetOrderManagementViewModels(status, searchString);
            ViewBag.OrderActive = "#" + status;
            ViewBag.SearchString = searchString;
            ViewBag.Status = status;
            return View(models.ToPagedList(page, pageSize));
        }
        [HttpPost]
        public ActionResult Detail(int? orderID)
        {
            OrderViewModel models = OrderHelper.GetOrderViewModels((int)orderID);
            return View(models);
        }
        [HttpPost]
        public ActionResult Confirm(int? id)
        {
            if (OrderHelper.Confirmed((int)id))
                return Content("success");
            return Content("failure");
        }
        [HttpPost]
        public ActionResult Cancel(int? id)
        {
            if (OrderHelper.Canceled((int)id))
                return Content("success");
            return Content("failure");
        }
    }
}