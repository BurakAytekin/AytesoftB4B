using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Domain;
using Aytesoft.Models.View;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        bool InsertOrder(int UserId);
        List<OrderView> GetOrder(int UserId);
        List<OrderDetailView> GetOrderDetail(int OrderId);
    }
}
