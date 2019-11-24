using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Areas.Admin.Models;
using MVC.Models;
namespace MVC.Areas.Admin.Controllers
{
    public class StatisticalController : Controller
    {
        // GET: Admin/Statistical
        public ActionResult Index()
        {
            StatisticalViewModel model = Helper.Statistical();
            return View(model);
        }
    }
}