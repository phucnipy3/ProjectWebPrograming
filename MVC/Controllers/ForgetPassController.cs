using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ForgetPassController : ApplicationController
    {
        private DatabaseDetailsContext db = new DatabaseDetailsContext();
        // GET: ForgetPass
        [HttpPost]
        public ActionResult ForgetPass(ForgetPassViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserHelper.GetUsers().SingleOrDefault(x => x.Email == model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Email not exist!");
                    return PartialView("../Shared/ForgetPass", model);
                }

                var mail = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("minhtantanhiep@gmail.com", "Minhtan@99"),
                    EnableSsl = true
                };

                var masage = new MailMessage();
                masage.From = new MailAddress("minhtantanhiep@gmail.com");
                masage.ReplyToList.Add("minhtantanhiep@gmail.com");
                masage.To.Add(new MailAddress(model.Email));
                masage.Subject = "Forget Pass";
                masage.Body = "We send mail from admin opxin.com. " +
                              "Your account is " + user.UserID + ", and you forgot your password. " +
                              "New password is " + Helper.RandomPassword(user.UserID) + ". Login again to change password. Thank you!";
                masage.IsBodyHtml = true;
                mail.Send(masage);
                return Content("success");
            }
            return PartialView("../Shared/ForgetPass", model);
        }
    }
}