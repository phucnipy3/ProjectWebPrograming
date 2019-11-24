using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using PagedList.Mvc;
using PagedList;
namespace MVC.Controllers
{
    public class HomeController : ApplicationController
    {
        public ActionResult Index()
        {
            int count = 0;
            List<ProductOnPage> lstHot = ProductHelper.GetHotProducts().Where(x => count++ <= 10 ).ToList();
            count = 0;
            List<ProductOnPage> lstNew = ProductHelper.GetNewProducts().Where(x => count++ <= 10).ToList();
            count = 0;
            List<ProductOnPage> lstBestSaler = ProductHelper.GetBestSalerProducts().Where(x => count++ <= 10).ToList();
            HomeViewModel model = new HomeViewModel() { ListBestSaler = lstBestSaler, ListHot = lstHot, ListNew = lstNew };
            return View(model);
        }
    }
}