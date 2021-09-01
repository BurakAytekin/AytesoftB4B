using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aytesoft.Models.Domain;
using Aytesoft.Models.View;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        bool InsertOrder(int UserId);
        List<OrderView> GetOrder(int UserId);
        List<OrderDetailView> GetOrderDetail(int OrderId);
        List<Order> OrderListCheck(List<Order> OrderList);
        List<OrderDetail> OrderDetailListCheck(List<OrderDetail> OrderDetailList);
        List<OrderDetailView> MapOrderDetail(List<OrderDetail> OrderDetailList);
        List<OrderView> MapOrder(List<Order> OrderList);
        bool AffectedRowCheck(int Result);
    }
}
