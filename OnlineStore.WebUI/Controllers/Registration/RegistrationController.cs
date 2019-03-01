using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using OnlineStore.WebUI.Database;

using OnlineStore.WebUI.Models;
using OnlineStore.Domain.Models;

namespace OnlineStore.WebUI.Controllers.Registration
{
    public class RegistrationController : Controller
    {
        ApplicationContext context;

        public RegistrationController()
        {
            context = new ApplicationContext();
        }
        // GET: Registration
        [HttpGet]
        public ActionResult Index()
        {
            return View("Registration");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserAccount user)
        {
            // Check if email already exist
            if (ModelState.IsValid)
            {

                var isExist = /*IsEmailExist(user.Email);*/ false;
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View("Registration", user);
                }

                user.ActivationCode = Guid.NewGuid().ToString();
                user.Password = Converter.Hash(user.Password);
                user.ConfirmPassword = Converter.Hash(user.ConfirmPassword);

                // add rows in the tables
                context.Users.Add(user);
                context.SaveChanges();

                // create session
                System.Diagnostics.Debug.WriteLine("session was created!!!!!!");
                Session["UserID"] = user.Id.ToString();
                Session["UserName"] = user.FullName.ToString();

                ModelState.Clear();
            }

            return RedirectToAction("../Home/Index");
        }

        private bool IsEmailExist(string email)
        {
                var v = context.Users.Where(a => a.Email == email).FirstOrDefault();
                return v != null;
        }        
    }
}