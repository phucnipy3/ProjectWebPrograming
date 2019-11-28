using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ProfileController : ApplicationController
    {
        // GET: Profile
        [Authorize]
        public ActionResult Index()
        {
            string userName = HttpContext.User.Identity.Name;
            User user = UserHelper.GetUserByUserID(userName);
            return View(user);
        }

        public ActionResult Update(User user)
        {
            UserHelper.UpdateUser(user);
            return View("Index");
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordInfo model)
        {
            if(ModelState.IsValid)
            {
                // đổi pass
                return Content("success");

            }
            return PartialView("ChangePassword", model);
        }

    }
}