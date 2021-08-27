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
    public class BasketService : IBasketService
    {
        public bool DeleteBasketItem(int ItemId)
        {
            int Result = DbContext.DeleteBasketItem(ItemId);
            return AffectedRowCheck(Result);
        }

        public List<Basket> GetBasketList(int Id)
        {
            List<Basket> BasketList = DbContext.GetBasketItems(Id);
            if (BasketList != null)
                return BasketList;
            return new List<Basket>();
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

    }
}
