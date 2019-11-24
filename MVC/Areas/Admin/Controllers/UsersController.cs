using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using PagedList;
using PagedList.Mvc;
using MVC.Areas.Admin.Models;
using MVC.Areas.Admin.Code;
using MVC.Controllers;

namespace MVC.Areas.Admin.Controllers
{
    [Authorize]
    public class UsersController : ApplicationController, IAdminController
    {
        private DatabaseDetailsContext db = new DatabaseDetailsContext();

        // GET: Users
        public ActionResult Index(string searchString, int page = 1, int sizePage = 10)
        {
            IEnumerable<User> models;
            if (String.IsNullOrEmpty(searchString))
                models = UserHelper.GetUsersExcludeAdmins();
            else
            {
                models = UserHelper.GetUsersExcludeAdmins(searchString);
            }
            ViewBag.SearchString = searchString;
            return View(models.OrderByDescending(x => x.ID).ToPagedList(page, sizePage));
        }
        [HttpPost]
        public ActionResult Add(object model)
        {
            if (ModelState.IsValid)
            {
                if(UserHelper.AddUser(model as User))
                {
                    return Content("success");
                }
                else
                {
                    ModelState.AddModelError("", "Existing User ID!");
                    return PartialView("Add");
                }
            }
            else
            {
                return PartialView("Add");
            }
        }

        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)  
        {
            if (UserHelper.DeleteUser(model.ID))
                return Content("success");
            return Content("failure");
        }

        [HttpPost]
        public ActionResult DeleteTransfer(int? id)
        {
            return PartialView("ConfirmDelete", new DeleteViewModel() { ID = (int)id });
        }
        [HttpPost]
        public ActionResult UpdateTransfer(int? id)
        {
            User model = UserHelper.GetUserByID((int)id);
            return PartialView("Update", model);
        }

        [HttpPost]
        public ActionResult Update(object model)
        {
            if (UserHelper.UpdateUser(model as User))
                return Content("success");
            return Content("failure");
            
        }

    }
}
