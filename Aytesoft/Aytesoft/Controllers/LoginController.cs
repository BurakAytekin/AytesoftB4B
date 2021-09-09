using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using Services.Interfaces;
using Services;
using Aytesoft.Models.Edit;
using Aytesoft.Models.View;

namespace Aytesoft.Controllers
{
    public class LoginController : Controller
    {
        ILoginService _loginservice;
        public LoginController(ILoginService loginservice)
        {
            _loginservice = loginservice;
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(UserEdit user)
        {
            if(ModelState.IsValid)
            {
                UserView auth = _loginservice.Authorization(user);
                if (auth.UserName != null)
                {
                    TempData["Name"] = auth.Name.ToString();
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}