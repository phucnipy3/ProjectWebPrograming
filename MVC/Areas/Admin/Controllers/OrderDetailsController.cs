using MVC.Areas.Admin.Code;
using MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using MVC.Areas.Admin.Models;
using PagedList;
namespace MVC.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin,Employee")]
    public class OrderDetailsController : ApplicationController, IDetailsController<OrderDetail>
    {
        public ActionResult Add(OrderDetail model)
        {
            if (ModelState.IsValid)
            {

                if (OrderDetailHelper.AddOrderDetail(model))
                {
                    return Content("success");
                }
                else
                {
                    ModelState.AddModelError("", "Existing product detail!");
                    return PartialView("Add");
                }
            }
            else
            {
                return PartialView("Add");
            }
        }
        [HttpPost]
        public ActionResult Delete(DeleteDetailViewModel model)
        {
            if (OrderDetailHelper.DeleteOrderDetail(model.MainID, model.SubID))
                return Content("success");
            return Content("failure");
        }
        [HttpPost]
        public ActionResult DeleteTransfer(int? mainID, int? SubID)
        {
            return PartialView("ConfirmDelete", new DeleteDetailViewModel() { SubID = (int)SubID, MainID = (int)mainID });

        }

        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            ViewBag.Active = "#OrderDetails";
            IEnumerable<OrderDetail> models;
            if (String.IsNullOrEmpty(searchString))
                models = OrderDetailHelper.GetOrderDetails();
            else
            {
                models = OrderDetailHelper.GetOrderDetails(searchString);
            }
            ViewBag.SearchString = searchString;
            return View(models.OrderByDescending(x => x.ProductID).ToPagedList(page, pageSize));
        }
        [HttpPost]
        public ActionResult Update(OrderDetail model)
        {
            if (OrderDetailHelper.UpdateOderDatail(model))
                return Content("success");
            return Content("failure");  
        }
        [HttpPost]
        public ActionResult UpdateTransfer(int? mainID, int? SubID)
        {
            OrderDetail model = OrderDetailHelper.GetOrderDetailByID((int)mainID, (int)SubID);
            return PartialView("Update", model);
        }
    }
}