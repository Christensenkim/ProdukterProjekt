using ProdukterProjekt.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProdukterProjekt.Core.DomainService
{
    public interface IProductRepository
    {
        public Product GetProductByID(int id);
        public List<Product> GetProductByFilter();
        public IEnumerable<Product> ReadProducts();
        public Product CreateProduct(Product p);
        public Product EditProduct(int id, Product p);
        public Product DeleteProduct(int id);
    }
}
