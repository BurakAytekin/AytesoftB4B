using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IBasketRepository : IRepository<Basket>
    {
        IEnumerable<Basket> getBasketList(int id);
    }
}
