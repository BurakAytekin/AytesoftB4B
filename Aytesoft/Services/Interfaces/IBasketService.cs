using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aytesoft.Models.Domain;

namespace Services.Interfaces
{
    public interface IBasketService
    {
        List<Basket> GetBasketList(int Id);
        bool DeleteBasketItem(int ItemId);
        bool InsertBasketItem(Basket BasketItem);
        bool AffectedRowCheck(int Result);
    }
}
