using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Interfaces;
using Services;
using Aytesoft.Models.Domain;

namespace Aytesoft.Controllers
{
    public class BasketController : Controller
    {
        IBasketService _BasketService;
        IOrderService _OrderService;
        public BasketController(IBasketService BasketService,IOrderService OrderService)
        {
            _BasketService = BasketService;
            _OrderService = OrderService;
        }
        // GET: Basket
        public ActionResult Index()
        {
            int UserId = Convert.ToInt32(User.Identity.Name);
            TempData.Keep();
            return View(_BasketService.GetBasketList(UserId));
        }

        public ActionResult Delete(int id)
        {
            _BasketService.DeleteBasketItem(id);
            return RedirectToAction("Index");
        }

        public ActionResult OrderSave()
        {
            int userid = Convert.ToInt32(User.Identity.Name);
            _OrderService.InsertOrder(userid);   
            return RedirectToAction("Index", "Home");
        }
    }
}