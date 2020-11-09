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
        public DbSet<User> UserTable { get; set; }

        public void SeedDB()
        {
            string password = "1234";
            byte[] passwordHashUser, passwordSaltUser, passwordHashAdmin, passwordSaltAdmin;

            CreatePasswordHash(password, out passwordHashUser, out passwordSaltUser);
            CreatePasswordHash(password, out passwordHashAdmin, out passwordSaltAdmin);

            var user1 = UserTable.Add(new User()
            {
                userName = "User",
                passwordHash = passwordHashUser,
                passwordSalt = passwordSaltUser,
                isAdmin = false
            });
            var user2 = UserTable.Add(new User()
            {
                userName = "Admin",
                passwordHash = passwordHashAdmin,
                passwordSalt = passwordSaltAdmin,
                isAdmin = true
            });
            var product1 = ProductsTable.Add(new Product
            {
                Name = "htrhpr",
                Color = "shtrh",
                CreatedDate = DateTime.Now,
                Price = 2.0,
                Ptype = "sgnfgnf"
            });
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
