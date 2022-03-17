using SimpleECA.Entities;
using SimpleECA.Models;
using SimpleECA.Models.UserViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleECA.Repos
{
    public interface IAuthRepo
    {
        Task<AuthenticateResponseViewModel> Authenticate(AuthenticateRequestViewModel model);
        Task<List<UserDetailsViewModel>> GetAll();
        Task<AuthUserViewModel> GetById(int id);
    }
}
