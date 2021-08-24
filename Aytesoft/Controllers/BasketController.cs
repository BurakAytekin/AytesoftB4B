using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aytesoft.Models;
using Aytesoft.DataAccessLayer;

namespace Aytesoft.Controllers
{
    public class BasketController : Controller
    {
        // GET: Basket
        public ActionResult Index()
        {
            int userid = Convert.ToInt32(User.Identity.Name);
            List<Basket> basketList = ProductDB.getBasketItems(userid);
            TempData.Keep();
            return View(basketList);
        }

        public ActionResult Delete(int id)
        {
            ProductDB.deleteBasketItem(id);
            return RedirectToAction("Index");
        }

        public ActionResult OrderSave()
        {
            int userid = Convert.ToInt32(User.Identity.Name);
            ProductDB.insertOrder(userid);
            return RedirectToAction("Index", "Home");
        }
    }
}