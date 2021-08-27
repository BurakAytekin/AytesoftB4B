using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aytesoft.Models.Domain;

namespace Services.Interfaces
{
    public interface IProductService
    {
        List<Product> GetProductList();
        List<Product> GetProductBySearch(string key);
        List<Product> ListCheck(List<Product> ProductList);
    }
}
