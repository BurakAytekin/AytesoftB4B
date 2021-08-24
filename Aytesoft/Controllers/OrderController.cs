using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aytesoft.Models;
using Aytesoft.DataAccessLayer;
using System.Web.Security;

namespace Aytesoft.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            int userid = Convert.ToInt32(User.Identity.Name);
            List<Order> orderlist = ProductDB.getOrder(userid);
            TempData.Keep();
            return View(orderlist);
        }

        public ActionResult OrderInformation(int orderid)
        {
            List<OrderDetail> detaillist = ProductDB.getOrderDetail(orderid);
            TempData.Keep();
            return View(detaillist);
        }
    }
}