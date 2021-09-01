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
        IOrderService _OrderService;
        public OrderController(IOrderService OrderService)
        {
            _OrderService = OrderService;
        }
        // GET: Order
        public ActionResult Index()
        {
            int userid = Convert.ToInt32(User.Identity.Name);
            TempData.Keep();
            return View(_OrderService.GetOrder(userid));
        }

        public ActionResult OrderInformation(int orderid)
        {
            TempData.Keep();
            return View(_OrderService.GetOrderDetail(orderid));
        }
    }
}