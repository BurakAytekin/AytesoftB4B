using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aytesoft.Models.Domain;
using System.Web.Security;
using Services.Interfaces;
using Services;

namespace Aytesoft.Controllers
{
    public class OrderController : Controller
    {
        IOrderService _orderservice;
        public OrderController(IOrderService orderservice)
        {
            _orderservice = orderservice;
        }
        // GET: Order
        public ActionResult Index()
        {
            int userid = Convert.ToInt32(User.Identity.Name);
            TempData.Keep();
            return View(_orderservice.GetOrder(userid));
        }

        public ActionResult OrderInformation(int orderid)
        {
            TempData.Keep();
            return View(_orderservice.GetOrderDetail(orderid));
        }
    }
}