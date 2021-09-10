using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Interfaces;
using DataAccess.Entity;
using Aytesoft.Models.View;
using DataAccess;
using AutoMapper;

namespace Services
{
    public class OrderService : IOrderService
    {
        UnitOfWork _unitOfWork = new UnitOfWork(new Context());
        public List<OrderView> GetOrder(int userId)
        {
            IEnumerable<Order> orderlist = _unitOfWork.orderRepository.GetOrderWithUserId(userId);
            return MapOrder(OrderListCheck(orderlist));
        }

        public List<OrderDetailView> GetOrderDetail(int orderId)
        {
            IEnumerable<OrderDetail> orderdetaillist = _unitOfWork.orderDetailRepository.getOrderDetailWithOrderId(orderId);
            IEnumerable<OrderDetail> checkedList = OrderDetailListCheck(orderdetaillist);
            return MapOrderDetail(checkedList);   
        }

        public bool InsertOrder(int userId)
        {
            IEnumerable<Basket> basketlist = _unitOfWork.basketRepository.getBasketList(userId);
            Order order = new Order();
            foreach (var item in basketlist)
            {
                order.ProductId += "<" + item.ProductId + ">";
                order.TotalPrice += item.ProductPrice * item.Quantity;
                order.Quantity += item.Quantity;
            }
            order.TotalPrice = order.TotalPrice * 0.9;
            order.TotalPrice = order.TotalPrice + order.TotalPrice * 0.18;
            order.UserID = userId;
            DateTime now = DateTime.Now;
            string date = now.ToString("dd/MM/yyyy");
            order.Date = date;
            _unitOfWork.orderRepository.Add(order);
            _unitOfWork.Complete();
            int lastinsertedid = order.Id;
            List <OrderDetail> orderDetailList = new List<OrderDetail>();
            foreach (var item in basketlist)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.ProductPrice = item.ProductPrice;
                orderDetail.product_Id = item.ProductId;
                orderDetail.Quantity = item.Quantity;
                orderDetail.OrderId = lastinsertedid;
                orderDetailList.Add(orderDetail);
            }
            _unitOfWork.basketRepository.RemoveRange(basketlist);
            _unitOfWork.orderDetailRepository.AddRange(orderDetailList);
            int result = _unitOfWork.Complete(); 
            return AffectedRowCheck(result);
        }


        private bool AffectedRowCheck(int result)
        {
            return result > 0;
        }

        private IEnumerable<Order> OrderListCheck(IEnumerable<Order> orderlist)
        {
            if (orderlist != null)
            {
                if (orderlist.Count() > 1)
                    return orderlist;
            }
            return new List<Order>();
        }

        private IEnumerable<OrderDetail> OrderDetailListCheck(IEnumerable<OrderDetail> orderdetaillist)
        {
            if (orderdetaillist != null)
            {
                if (orderdetaillist.Count() > 0)
                    return orderdetaillist;
            }
            return new List<OrderDetail>();
        }

        private List<OrderDetailView> MapOrderDetail(IEnumerable<OrderDetail> orderdetaillist)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderDetail, OrderDetailView>()
            .ForMember(dest => dest.Code,act => act.MapFrom(src => src.Product.Code))
            .ForMember(dest => dest.Name,act => act.MapFrom(src => src.Product.Name)));
            var mapper = new Mapper(config);
            List<OrderDetailView> mappedOrderDetail = mapper.Map<List<OrderDetailView>>(orderdetaillist);
            return mappedOrderDetail;
        }

        private List<OrderView> MapOrder(IEnumerable<Order> orderlist)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderView>());
            var mapper = new Mapper(config);
            List<OrderView> mappedOrder = mapper.Map<List<OrderView>>(orderlist);
            return mappedOrder;
        }
    }
}
