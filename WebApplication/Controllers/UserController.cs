using ClassLibraryData.Abstract;
using ClassLibraryData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            { 
                FormsAuthentication.SetAuthCookie(user.Email, true);
               
                return Redirect("/Books/List");
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return Redirect("/Books/List");
        }
            
    }
}