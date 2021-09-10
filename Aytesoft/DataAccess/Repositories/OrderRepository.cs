using DataAccess.Entity;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(Context context) : base(context)
        {
        }
        public Context context { get { return _context as Context; } }

        public IEnumerable<Order> GetOrderWithUserId(int userid)
        {
            IEnumerable<Order> orderlist = context.Order.Where(x => x.UserID == userid).ToList();
            return orderlist;
        }
    }
}
