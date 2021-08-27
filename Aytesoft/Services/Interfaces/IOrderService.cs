using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aytesoft.Models.Domain;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        bool InsertOrder(int UserId);
        List<Order> GetOrder(int UserId);
        List<OrderDetail> GetOrderDetail(int OrderId);
        List<Order> OrderListCheck(List<Order> OrderList);
        List<OrderDetail> OrderDetailListCheck(List<OrderDetail> OrderDetailList);
        bool AffectedRowCheck(int Result);
    }
}
