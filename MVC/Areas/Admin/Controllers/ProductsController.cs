using MVC.Areas.Admin.Code;
using MVC.Areas.Admin.Models;
using MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Areas.Admin.Controllers
{
    public class ProductsController : ApplicationController, IAdminController
    {
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(object model)
        {
            throw new NotImplementedException();
        }
        [HttpPost]


        public ActionResult Delete(DeleteViewModel model)
        {
            throw new NotImplementedException();
        }
        [HttpPost]


        public ActionResult DeleteTransfer(int? id)
        {
            throw new NotImplementedException();
        }


        [HttpPost]

        public ActionResult Update(object model)
        {
            throw new NotImplementedException();
        }

        [HttpPost]

        public ActionResult UpdateTransfer(int? id)
        {
            throw new NotImplementedException();
        }
    }
}