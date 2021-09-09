using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entity;

namespace Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetProductList();
        IEnumerable<Product> GetProductBySearch(string key);
    }
}
