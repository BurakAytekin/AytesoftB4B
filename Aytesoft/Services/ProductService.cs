using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entity;
using DataAccess;
using Services.Interfaces;
using AutoMapper;

namespace Services
{
    public class ProductService : IProductService
    {
        UnitOfWork _unitOfWork = new UnitOfWork(new Context());
        public IEnumerable<Product> GetProductBySearch(string key)
        {
            IEnumerable<Product> productlist = _unitOfWork.productRepository.getProductBySearch(key);
            return ListCheck(productlist);
        }

        public IEnumerable<Product> GetProductList()
        {
            IEnumerable<Product> productlist = _unitOfWork.productRepository.GetAll();
            return ListCheck(productlist);
        }

        private IEnumerable<Product> ListCheck(IEnumerable<Product> productlist)
        {
            if (productlist != null)
                return productlist;
            return new List<Product>();
        }

    }
}
