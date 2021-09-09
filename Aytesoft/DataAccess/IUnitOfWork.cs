using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories.Interfaces;

namespace DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository productRepository { get; }
        IUserRepository userRepository { get; }
        IBasketRepository basketRepository { get; }
        IOrderRepository orderRepository { get; }
        IOrderDetailRepository orderDetailRepository { get; }
        
        int Complete();
    }
}
