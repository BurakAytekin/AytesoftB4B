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
        IBasketService _basketservice;
        IOrderService _orderservice;
        public BasketController(IBasketService basketservice,IOrderService orderservice)
        {
            _basketservice = basketservice;
            _orderservice = orderservice;
        }
        // GET: Basket
        public ActionResult Index()
        {
            int UserId = Convert.ToInt32(User.Identity.Name);
            TempData.Keep();
            return View(_basketservice.GetBasketList(UserId));
        }

        public ActionResult Delete(int id)
        {
            _basketservice.DeleteBasketItem(id);
            return RedirectToAction("Index");
        }

        public ActionResult OrderSave()
        {
            int userid = Convert.ToInt32(User.Identity.Name);
            _orderservice.InsertOrder(userid);   
            return RedirectToAction("Index", "Home");
        }
    }
}