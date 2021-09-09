using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entity;

namespace DataAccess.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> getProductByName(string name);
        IEnumerable<Product> getProductByCode(string code);
        IEnumerable<Product> getProductBySearch(string key);
    }
}
