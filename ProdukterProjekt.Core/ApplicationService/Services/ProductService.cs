using ProdukterProjekt.Core.DomainService;
using ProdukterProjekt.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProdukterProjekt.Core.ApplicationService.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _ProductRepo;

        public ProductService(IProductRepository productRepository)
        {
            _ProductRepo = productRepository;
        }

        public Product CreateProduct(Product p)
        {
            return _ProductRepo.CreateProduct(p);
        }

        public Product DeleteProduct(int id)
        {
            return _ProductRepo.DeleteProduct(id);
        }

        public List<Product> GetProductByFilter()
        {
            return _ProductRepo.GetProductByFilter();
        }

        public Product GetProductByID(int id)
        {
            return _ProductRepo.GetProductByID(id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _ProductRepo.ReadProducts();
        }

        public Product UpdateProduct(Product p)
        {
            return _ProductRepo.EditProduct(p);
        }
    }
}
