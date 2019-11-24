using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using System.Web.Security;
namespace MVC.Controllers
{
    public class RegisterController : ApplicationController
    {
        private DatabaseDetailsContext db = new DatabaseDetailsContext();
        // GET: Register
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                User user = new User()
                {
                    UserID = model.Email,
                    Name = model.FullName,
                    Sex = model.Sex,
                    Phone = model.Tel,
                    Password = model.Password,
                    Email = model.Email,
                    Role = "Customer",
                    Active = false,
                    Status = true,
                    Address = model.Address
                };
                if (UserHelper.AddUser(user))
                {
                    if (HttpContext.User.Identity.IsAuthenticated)
                    {
                        UserHelper.ModifyActive(HttpContext.User.Identity.Name, false);
                        FormsAuthentication.SignOut();
                    }
                    FormsAuthentication.SetAuthCookie(user.UserID, true);
                    UserHelper.ModifyActive(user.UserID, true);
                    return Content("success");
                }
            }
            return PartialView("../Shared/Register", model);
        }
    }
}