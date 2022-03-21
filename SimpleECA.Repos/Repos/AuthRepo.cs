using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleECA.Entities;
using SimpleECA.Helpers;
using SimpleECA.Models;
using SimpleECA.Models.UserViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleECA.Repos
{
    public class AuthRepo : IAuthRepo
    {
        private readonly AppSettingsHelper _appSettings;
        private readonly SimpleECADbContext _dBContext;
        public AuthRepo(AppSettingsHelper appSettings, SimpleECADbContext dBContext)
        {
            _appSettings = appSettings;
            _dBContext = dBContext;
        }
        public async Task<AuthenticateResponseViewModel> Authenticate(AuthenticateRequestViewModel model)
        {
            var userList = await GetAll();
            var userdata = userList.Where(x => x.email == model.Username && x.rpassword == model.Password).FirstOrDefault();
            if (userdata == null) return null;

            var user = new AuthUserViewModel()
            {
                FullName = $"{userdata.firstname} {userdata.lastname}",
                Username = userdata.email,
                Email = userdata.email,
                PhoneNumber = userdata.mobilenumber,
                Id = userdata.userid,
                Password = userdata.rpassword
            };
            var token = GenerateJwtToken.Generate(user, _appSettings.Secret.Key);

            return new AuthenticateResponseViewModel(user, token);
        }

        public async Task<List<UserDetailsViewModel>> GetAll()
        {

            var userdata = await _dBContext.TblUserDetails.Select(x=>
            new UserDetailsViewModel {
                email = x.email,
                firstname = x.firstname,
                isactive = x.isactive,
                lastname = x.lastname,
                mobilenumber = x.mobilenumber,
                rpassword = x.rpassword,
                userid = x.userid,
                userroleid = x.userroleid
            }).ToListAsync();
            //userdata.ForEach(e =>
            //{
            //    e.rpassword = AESCryptoHelper.Decrypt(e.rpassword,_appSettings.Secret.Key);
            //});
            return userdata;

        }

        public async Task<AuthUserViewModel> GetById(int id)
        {
            var userList = await GetAll();
            var userdata = userList.Where(x => x.userid == id).FirstOrDefault();
            var user = new AuthUserViewModel()
            {
                FullName = $"{userdata.firstname} {userdata.lastname}",
                Username = userdata.email,
                Email = userdata.email,
                PhoneNumber = userdata.mobilenumber,
                Password = userdata.rpassword
            };
            return user;
        }

        public async Task<bool> CreateUser(UserDetailsViewModel user)
        {
            if (user == null) return false;
            var dbModel = new TblUserDetails
            {
                createdon = DateTime.Now,
                email = user.email,
                firstname = user.firstname,
                isactive = user.isactive,
                lastname = user.lastname,
                mobilenumber = user.mobilenumber,
                updatedon = DateTime.Now,
                userroleid = user.userroleid,
                rpassword = AESCryptoHelper.Encrypt(user.rpassword, _appSettings.Secret.Key)
            };
            await _dBContext.TblUserDetails.AddAsync(dbModel);
            return await _dBContext.SaveChangesAsync() > 0;
        }
    }
}
