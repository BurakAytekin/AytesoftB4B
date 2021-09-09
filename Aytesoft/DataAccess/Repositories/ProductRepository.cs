using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entity;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(Context context) : base(context)
        {
        }

        public IEnumerable<Product> getProductByCode(string code)
        {
            return context.Product.Where(x => x.Code.Contains(code)).ToList();
        }

        public IEnumerable<Product> getProductByName(string name)
        {
            return context.Product.Where(x => x.Name.Contains(name)).ToList();
        }

        public IEnumerable<Product> getProductBySearch(string key)
        {
            return context.Product.Where(x => x.Name.Contains(key) || x.Code.Contains(key)).ToList();
        }

        public Context context { get { return _context as Context; } }
    }
}
