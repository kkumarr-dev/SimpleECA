using SimpleECA.Models.UserViewModel;
using SimpleECA.Repos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleECA.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<bool> CreateUser(UserDetailsViewModel user)
        {
            return await _userRepo.CreateUser(user);
        }
    }
}
