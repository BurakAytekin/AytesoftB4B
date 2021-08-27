using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aytesoft.Models.Domain;
using Aytesoft.DataAccessLayer;
using Services;
using Services.Interfaces;

namespace Aytesoft.Controllers
{
    public class ProductController : Controller
    {
        IProductService _ProductService;
        IBasketService _BasketService;
        public ProductController(IProductService ProductService, IBasketService BasketService)
        {
            _ProductService = ProductService;
            _BasketService = BasketService;
        }
        // GET: Product
        public ActionResult Index()
        {
            TempData.Keep();
            return View();
        }
        public JsonResult getProducts()
        {

            List<Product> PdList = _ProductService.GetProductList();
            return Json(PdList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult insertBasket(Basket basketitem)
        {
            basketitem.userid = Convert.ToInt32(User.Identity.Name);
            if(_BasketService.InsertBasketItem(basketitem))
                return Json("Insertion Successful", JsonRequestBehavior.AllowGet);
            return Json("Insertion Failed", JsonRequestBehavior.DenyGet);
        }
        public JsonResult Search(string key)
        {
            List<Product> PdList = _ProductService.GetProductBySearch(key);
            return Json(PdList, JsonRequestBehavior.AllowGet);
        }
    }
}