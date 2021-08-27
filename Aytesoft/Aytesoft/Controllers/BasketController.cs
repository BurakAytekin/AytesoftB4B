using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Interfaces;
using Services;
using Aytesoft.Models.Domain;
using Aytesoft.DataAccessLayer;

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
            List<Basket> BasketList = _BasketService.GetBasketList(UserId);
            TempData.Keep();
            return View(BasketList);
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