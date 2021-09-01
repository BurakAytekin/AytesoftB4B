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
    public class BasketService : IBasketService
    {
        public bool DeleteBasketItem(int ItemId)
        {
            int Result = DbContext.DeleteBasketItem(ItemId);
            return AffectedRowCheck(Result);
        }

        public List<BasketView> GetBasketList(int Id)
        {

            List<Basket> BasketList = DbContext.GetBasketItems(Id);
            List<BasketView> MappedList = BasketMap(BasketList);
            if (BasketList.Count > 0)
                return MappedList;
            return new List<BasketView>();
        }

        public bool InsertBasketItem(Basket BasketItem)
        {
            int Result = DbContext.InsertBasketItem(BasketItem);
            return AffectedRowCheck(Result);
        }

        public bool AffectedRowCheck(int Result)
        {
            if (Result > 0)
                return true;
            return false;
        }

        public List<BasketView> BasketMap(List<Basket> BasketList)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Basket, BasketView>());
            var mapper = new Mapper(config);
            List<BasketView> basket = mapper.Map<List<Basket>, List<BasketView>>(BasketList);
            return basket;
        }
    }
}
