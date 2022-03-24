using SimpleECA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleECA.Repos
{
    public interface IUserRepo
    {
        Task<bool> CreateUser(UserDetailsViewModel user);
        Task<bool> CreateUserAddress(UserAddressViewModel model);
        Task<List<UserAddressViewModel>> GetUserAddressList(int userid);
    }
}
