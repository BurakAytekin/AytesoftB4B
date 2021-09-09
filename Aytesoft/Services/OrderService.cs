using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Interfaces;
using DataAccess.Domain;
using Aytesoft.Models.View;
using DataAccess;
using AutoMapper;

namespace Services
{
    public class OrderService : IOrderService
    {
        public List<OrderView> GetOrder(int userId)
        {
            List<Order> orderlist =  DbContext_1.GetOrders(userId);
            return MapOrder(OrderListCheck(orderlist));
        }

        public List<OrderDetailView> GetOrderDetail(int orderId)
        {
            List<OrderDetail> orderdetaillist = DbContext_1.GetOrderDetail(orderId);
            List<OrderDetail> checkedList = OrderDetailListCheck(orderdetaillist);
            return MapOrderDetail(checkedList);   
        }

        public bool InsertOrder(int userId)
        {
            int result = DbContext_1.InsertOrder(userId);
            return AffectedRowCheck(result);
        }


        private bool AffectedRowCheck(int result)
        {
            return result > 0;
        }

        private List<Order> OrderListCheck(List<Order> orderlist)
        {
            if (orderlist != null)
            {
                if (orderlist.Count > 1)
                    return orderlist;
            }
            return new List<Order>();
        }

        private List<OrderDetail> OrderDetailListCheck(List<OrderDetail> orderdetaillist)
        {
            if (orderdetaillist != null)
            {
                if (orderdetaillist.Count > 1)
                    return orderdetaillist;
            }
            return new List<OrderDetail>();
        }

        private List<OrderDetailView> MapOrderDetail(List<OrderDetail> orderdetaillist)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderDetail, OrderDetailView>()
            .ForMember(dest => dest.Code,act => act.MapFrom(src => src.Product.Code))
            .ForMember(dest => dest.Name,act => act.MapFrom(src => src.Product.Name)));
            var mapper = new Mapper(config);
            List<OrderDetailView> mappedOrderDetail = mapper.Map<List<OrderDetailView>>(orderdetaillist);
            return mappedOrderDetail;
        }

        private List<OrderView> MapOrder(List<Order> orderlist)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderView>());
            var mapper = new Mapper(config);
            List<OrderView> mappedOrder = mapper.Map<List<OrderView>>(orderlist);
            return mappedOrder;
        }
    }
}
