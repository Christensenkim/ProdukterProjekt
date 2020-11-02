using ProdukterProjekt.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProdukterProjekt.Core.DomainService
{
    public interface IUserRepository
    {
        void AddUser(User u);
        void DeleteUser(User u);
        User UpdateUser(int id, User u);
        IEnumerable<User> ReadUsers();
    }
}
