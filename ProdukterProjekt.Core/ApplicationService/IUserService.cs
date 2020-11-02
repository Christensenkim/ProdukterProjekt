using ProdukterProjekt.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProdukterProjekt.Core.ApplicationService
{
    public interface IUserService
    {
        void addUser(User u);
        void deleteUser(User u);
        User updateUser(int id, User u);
        IEnumerable<User> ReadUsers();
    }
}
