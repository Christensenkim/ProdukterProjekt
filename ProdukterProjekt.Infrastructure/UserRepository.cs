using ProdukterProjekt.Core.DomainService;
using ProdukterProjekt.Core.Entity;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProdukterProjekt.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        readonly ProductContext _ctx;

        public UserRepository(ProductContext ctx)
        {
            _ctx = ctx;
        }

        public void AddUser(User u)
        {
            _ctx.UserTable.Add(u);
            _ctx.SaveChanges();
        }

        public void DeleteUser(User u)
        {
            _ctx.UserTable.Remove(u);
            _ctx.SaveChanges();
        }

        public IEnumerable<User> ReadUsers()
        {
            return _ctx.UserTable;
        }

        public User UpdateUser(int id, User u)
        {
            var UserFromDB = _ctx.UserTable.FirstOrDefault(u => u.ID == id);

            if (UserFromDB != null)
            {
                UserFromDB.userName = u.userName;
                UserFromDB.isAdmin = u.isAdmin;

                var product = _ctx.Update(UserFromDB).Entity;
                _ctx.SaveChanges();
                return product;
            }
            return null;
        }
    }
}
