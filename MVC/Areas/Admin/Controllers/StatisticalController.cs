using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Areas.Admin.Models;
using MVC.Controllers;
using MVC.Models;
namespace MVC.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin,Employee")]
    public class StatisticalController : ApplicationController
    {
        // GET: Admin/Statistical
        public ActionResult Index()
        {
            ViewBag.Active = "#Statistical";
            StatisticalViewModel model = Helper.Statistical();
            return View(model);
        }
    }
}