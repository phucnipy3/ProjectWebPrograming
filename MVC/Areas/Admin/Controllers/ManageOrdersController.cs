using MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Areas.Admin.Controllers
{
    public class ManageOrdersController : ApplicationController
    {
        // GET: Admin/ManageOrders
        public ActionResult Index()
        {
            ViewBag.OrderActive = "#Ordered";
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }
        public ActionResult Confirm()
        {
            return Content("");
        }
        public ActionResult Cancel()
        {
            return Content("");
        }
    }
}