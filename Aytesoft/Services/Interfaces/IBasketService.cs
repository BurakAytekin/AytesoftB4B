using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entity;
using Aytesoft.Models.View;

namespace Services.Interfaces
{
    public interface IBasketService
    {
        List<BasketView> GetBasketList(int Id);
        void DeleteBasketItem(int ItemId);
        bool InsertBasketItem(Basket BasketItem);
    }
}
