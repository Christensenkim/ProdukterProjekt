using ProdukterProjekt.Core.DomainService;
using ProdukterProjekt.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProdukterProjekt.Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        public Product CreateProduct(Product p)
        {
            throw new NotImplementedException();
        }

        public Product DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Product EditProduct(Product p)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductByFilter()
        {
            throw new NotImplementedException();
        }

        public Product GetProductByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductsByFilter()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> ReadProducts()
        {
            throw new NotImplementedException();
        }
    }
}
