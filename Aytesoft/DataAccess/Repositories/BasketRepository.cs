using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories.Interfaces;
using DataAccess.Entity;
using System.Data.Entity;

namespace DataAccess.Repositories
{
    public class BasketRepository : Repository<Basket>, IBasketRepository
    {
        public BasketRepository(Context context) : base(context)
        {
        }
        public IEnumerable<Basket> getBasketList(int id)
        {
            return context.Basket.Where(x => x.UserId == id).ToList();
        }
        public Context context { get { return _context as Context; } }


    }
}
