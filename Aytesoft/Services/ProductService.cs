using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aytesoft.Models.Domain;
using DataAccess;
using Services.Interfaces;


namespace Services
{
    public class ProductService : IProductService
    {
        public List<Product> GetProductBySearch(string key)
        {
            List<Product> ProductList = DbContext.GetProductBySearch(key);
            return ListCheck(ProductList);
        }

        public List<Product> GetProductList()
        {
            List<Product> ProductList = DbContext.GetProductList();
            return ListCheck(ProductList);
        }

        public List<Product> ListCheck(List<Product> ProductList)
        {
            if (ProductList != null)
                return ProductList;
            return new List<Product>();
        }
    }
}
