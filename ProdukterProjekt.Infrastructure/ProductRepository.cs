using Microsoft.EntityFrameworkCore.Diagnostics;
using ProdukterProjekt.Core.DomainService;
using ProdukterProjekt.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProdukterProjekt.Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        readonly ProductContext _ctx;

        public ProductRepository(ProductContext ctx)
        {
            _ctx = ctx;
        }
        public Product CreateProduct(Product p)
        {
            var product = _ctx.ProductsTable.Add(p).Entity;
            _ctx.SaveChanges();

            return product;
        }

        public Product DeleteProduct(int id)
        {
            var productRemove = _ctx.Remove(new Product { Id = id }).Entity;
            _ctx.SaveChanges();
            return productRemove;
        }

        public Product EditProduct(int id, Product p)
        {
            var productFromDB = _ctx.ProductsTable.FirstOrDefault(c => c.Id == id);

            if(productFromDB != null)
            {
                productFromDB.Name = p.Name;
                productFromDB.Price = p.Price;
                productFromDB.Ptype = p.Ptype;
                productFromDB.CreatedDate = p.CreatedDate;

                return productFromDB;
            }
            return null;
        }

        public List<Product> GetProductByFilter()
        {
            throw new NotImplementedException();
        }

        public Product GetProductByID(int id)
        {
            return _ctx.ProductsTable.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Product> ReadProducts()
        {
            return _ctx.ProductsTable;
        }
    }
}
