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
    public class BasketService : IBasketService
    {
        UnitOfWork _unitOfWork = new UnitOfWork(new Context());
        public void DeleteBasketItem(int ItemId)
        {
            _unitOfWork.basketRepository.Remove(ItemId);
            _unitOfWork.Complete();
        }

        public List<BasketView> GetBasketList(int id)
        {
            IEnumerable<Basket> BasketList = _unitOfWork.basketRepository.getBasketList(id);
            List<BasketView> MappedList = BasketMap(BasketList);
            if (MappedList.Count > 0)
                return MappedList;
            return new List<BasketView>();
        }

        public bool InsertBasketItem(Basket BasketItem)
        {
            _unitOfWork.basketRepository.Add(BasketItem);
            int result = _unitOfWork.Complete();
            return AffectedRowCheck(result);
        }

        private bool AffectedRowCheck(int Result)
        {
            return Result > 0; 
        }

        private List<BasketView> BasketMap(IEnumerable<Basket> BasketList)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Basket, BasketView>());
            var mapper = new Mapper(config);
            List<BasketView> basket = mapper.Map<IEnumerable<Basket>, List<BasketView>>(BasketList);
            return basket;
        }
    }
}
