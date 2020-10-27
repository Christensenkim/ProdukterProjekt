using Microsoft.EntityFrameworkCore;
using ProdukterProjekt.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProdukterProjekt.Infrastructure.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> opt)
            : base(opt) { }

        public DbSet<Product> ProductsTable { get; set; }
    }
}
