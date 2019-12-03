using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ErrorController : ApplicationController
    {
        // GET: Error
        public ActionResult Index()
        {
            return View("~/View/Shared/Error");
        }
    }
}