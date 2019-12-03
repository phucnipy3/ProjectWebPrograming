using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Models;
using System.Web.Mvc;
using PagedList;

namespace MVC.Controllers
{
    public class OrdersController : ApplicationController
    {
        [Authorize]
        // GET: Orders
        public ActionResult Index(string searchString, string status ="All",int page = 1, int pageSize = 10)
        {
            
            string UserID = HttpContext.User.Identity.Name;
            IEnumerable<OrderViewModel> models;
            if (!String.IsNullOrEmpty( searchString ))
                models = OrderHelper.GetOrderViewModels(UserID, status, searchString);
            models = OrderHelper.GetOrderViewModels(UserID, status);
            ViewBag.OrderActive = "#" + status;
            ViewBag.SearchString = searchString;
            ViewBag.Status = status;
            return View(models.ToPagedList(page, pageSize));
        }
        public ActionResult Detail(int OrderID)
        {
            string UserID = HttpContext.User.Identity.Name;
            OrderViewModel models = OrderHelper.GetOrderViewModels(OrderID, UserID);
            return View(models);
        }
    }
}