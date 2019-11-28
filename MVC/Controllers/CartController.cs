using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class CartController : ApplicationController
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
    }
}