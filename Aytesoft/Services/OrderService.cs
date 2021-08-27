using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Interfaces;
using Aytesoft.Models.Domain;
using DataAccess;

namespace Services
{
    public class OrderService : IOrderService
    {
        public List<Order> GetOrder(int UserId)
        {
            List<Order> OrderList =  DbContext.GetOrders(UserId);
            return OrderListCheck(OrderList);

        }

        public List<OrderDetail> GetOrderDetail(int OrderId)
        {
            List<OrderDetail> OrderDetailList = DbContext.GetOrderDetail(OrderId);
            return OrderDetailListCheck(OrderDetailList);
            
        }

        public bool InsertOrder(int UserId)
        {
            int Result = DbContext.InsertOrder(UserId);
            return AffectedRowCheck(Result);
        }


        public bool AffectedRowCheck(int Result)
        {
            if (Result > 0)
                return true;
            return false;
        }

        public List<Order> OrderListCheck(List<Order> OrderList)
        {
            if (OrderList != null)
            {
                if (OrderList.Count > 1)
                    return OrderList;
            }
            return new List<Order>();
        }

        public List<OrderDetail> OrderDetailListCheck(List<OrderDetail> OrderDetailList)
        {
            if (OrderDetailList != null)
            {
                if (OrderDetailList.Count > 1)
                    return OrderDetailList;
            }
            return new List<OrderDetail>();
        }
    }
}
