using ProdukterProjekt.Core.DomainService;
using ProdukterProjekt.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProdukterProjekt.Core.ApplicationService.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _UserRepo;

        public UserService(IUserRepository userRepository)
        {
            _UserRepo = userRepository;
        }

        public void addUser(User u)
        {
            _UserRepo.AddUser(u);
        }

        public void deleteUser(User u)
        {
            _UserRepo.DeleteUser(u);
        }

        public IEnumerable<User> ReadUsers()
        {
            return _UserRepo.ReadUsers();
        }

        public User updateUser(int id, User u)
        {
            return _UserRepo.UpdateUser(id, u);
        }
    }
}
