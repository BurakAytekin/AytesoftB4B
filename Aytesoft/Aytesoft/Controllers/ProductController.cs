using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;
using Services.Interfaces;
using DataAccess.Entity;

namespace Aytesoft.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productservice;
        IBasketService _basketservice;
        public ProductController(IProductService productservice, IBasketService basketservice)
        {
            _productservice = productservice;
            _basketservice = basketservice;
        }
        // GET: Product
        public ActionResult Index()
        {
            TempData.Keep();
            return View();
        }
        public JsonResult getProducts()
        {

            IEnumerable<Product> PdList = _productservice.GetProductList();
            return Json(PdList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult insertBasket(Basket basketitem)
        {
            basketitem.UserId = Convert.ToInt32(User.Identity.Name);
            if(_basketservice.InsertBasketItem(basketitem))
                return Json("Insertion Successful", JsonRequestBehavior.AllowGet);
            return Json("Insertion Failed", JsonRequestBehavior.DenyGet);
        }
        public JsonResult Search(string key)
        {
            IEnumerable<Product> PdList = _productservice.GetProductBySearch(key);
            return Json(PdList, JsonRequestBehavior.AllowGet);
        }
    }
}