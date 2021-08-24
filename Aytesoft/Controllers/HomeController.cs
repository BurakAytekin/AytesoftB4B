using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Aytesoft.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (TempData["Name"] != null)
                ViewBag.user = TempData["Name"].ToString();
            TempData.Keep();
            return View();
        }
        
    }
}