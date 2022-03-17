using SimpleECA.Entities;
using SimpleECA.Models;
using SimpleECA.Models.UserViewModel;
using SimpleECA.Repos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleECA.Services
{
    public class AuthService: IAuthService
    {
        private readonly IAuthRepo _authenticationRepo;
        public AuthService(IAuthRepo authenticationRepo)
        {
            _authenticationRepo = authenticationRepo;
        }

        public async Task<AuthenticateResponseViewModel> Authenticate(AuthenticateRequestViewModel model)
        {
            return await _authenticationRepo.Authenticate(model);
        }

        public async Task<List<UserDetailsViewModel>> GetAll()
        {
            return await _authenticationRepo.GetAll();
        }

        public async Task<AuthUserViewModel> GetById(int id)
        {
            return await GetById(id);
        }
    }
}
