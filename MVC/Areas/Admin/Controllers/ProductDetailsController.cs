using MVC.Areas.Admin.Code;
using MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using MVC.Areas.Admin.Models;

namespace MVC.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin,Employee")]
    public class ProductDetailsController : ApplicationController,IAdminController<ProductDetail>
    {
        // GET: Admin/ProductDetails
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add(ProductDetail model)
        {
            throw new NotImplementedException();
        }

        public ActionResult Delete(DeleteViewModel model)
        {
            throw new NotImplementedException();
        }

        public ActionResult DeleteTransfer(int? id)
        {
            throw new NotImplementedException();
        }
        
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public ActionResult Update(ProductDetail model)
        {
            throw new NotImplementedException();
        }

        public ActionResult UpdateTransfer(int? id)
        {
            throw new NotImplementedException();
        }
    }
}