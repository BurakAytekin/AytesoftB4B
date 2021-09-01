using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Interfaces;
using Aytesoft.Models.Domain;
using Aytesoft.Models.View;
using DataAccess;
using AutoMapper;

namespace Services
{
    public class OrderService : IOrderService
    {
        public List<OrderView> GetOrder(int UserId)
        {
            List<Order> OrderList =  DbContext.GetOrders(UserId);
            return MapOrder(OrderListCheck(OrderList));
        }

        public List<OrderDetailView> GetOrderDetail(int OrderId)
        {
            List<OrderDetail> OrderDetailList = DbContext.GetOrderDetail(OrderId);
            List<OrderDetail> CheckedList = OrderDetailListCheck(OrderDetailList);
            return MapOrderDetail(CheckedList);   
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

        public List<OrderDetailView> MapOrderDetail(List<OrderDetail> OrderDetailList)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderDetail, OrderDetailView>()
            .ForMember(dest => dest.Code,act => act.MapFrom(src => src.Product.Code))
            .ForMember(dest => dest.Name,act => act.MapFrom(src => src.Product.Name)));
            var mapper = new Mapper(config);
            List<OrderDetailView> mappedOrderDetail = mapper.Map<List<OrderDetailView>>(OrderDetailList);
            return mappedOrderDetail;
        }

        public List<OrderView> MapOrder(List<Order> OrderList)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderView>());
            var mapper = new Mapper(config);
            List<OrderView> mappedOrder = mapper.Map<List<OrderView>>(OrderList);
            return mappedOrder;
        }
    }
}
