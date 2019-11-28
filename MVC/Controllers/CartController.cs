using MVC.Models;
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
        [HttpPost]
        public ActionResult Order(OrderInfomation model)
        {
            if(ModelState.IsValid)
            {
                // Order
                return Content("success");
            }
            return PartialView("FormInfo", model);
        }
    }
}