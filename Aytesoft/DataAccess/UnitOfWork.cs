using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private Context _context;
        public UnitOfWork(Context context)
        {
            _context = context;
            productRepository = new ProductRepository(_context);
            userRepository = new UserRepository(_context);
            basketRepository = new BasketRepository(_context);
            orderRepository = new OrderRepository(_context);
            orderDetailRepository = new OrderDetailRepository(_context);
        }

        public IProductRepository productRepository { get; private set; }

        public IUserRepository userRepository { get; private set; }

        public IBasketRepository basketRepository { get; private set; }

        public IOrderRepository orderRepository { get; private set; }

        public IOrderDetailRepository orderDetailRepository { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
