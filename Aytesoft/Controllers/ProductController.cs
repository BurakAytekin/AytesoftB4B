using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aytesoft.Models;
using Aytesoft.DataAccessLayer;

namespace Aytesoft.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            TempData.Keep();
            return View();
        }
        public JsonResult getProducts()
        {

            List<Product> PdList = ProductDB.getProductList();
            return Json(PdList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult insertBasket(Basket basketitem)
        {
            basketitem.userid = Convert.ToInt32(User.Identity.Name);
            ProductDB.insertBasketItem(basketitem);
            return Json("Insertion success", JsonRequestBehavior.AllowGet);
        }
    }
}