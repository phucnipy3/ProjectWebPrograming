using MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Areas.Admin.Controllers
{
    public class HomeController : ApplicationController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return RedirectToAction("Index", "ManageOrders");
        }
    }
}