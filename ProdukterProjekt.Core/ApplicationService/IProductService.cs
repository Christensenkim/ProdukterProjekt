using ProdukterProjekt.Core.ApplicationService.Services;
using ProdukterProjekt.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProdukterProjekt.Core.ApplicationService
{
    public interface IProductService
    {
        public Product GetProductByID(int id);
        public List<Product> GetProductByFilter();
        public IEnumerable<Product> GetProducts();
        public Product CreateProduct(Product p);
        public Product DeleteProduct(int id);
        public Product UpdateProduct(Product p);
    }
}
