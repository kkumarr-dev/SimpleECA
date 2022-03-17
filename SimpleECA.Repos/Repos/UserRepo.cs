using SimpleECA.Entities;
using SimpleECA.Helpers;
using SimpleECA.Models.UserViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleECA.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly SimpleECADbContext _dBContext;
        private readonly AppSettingsHelper _appsettings;
        public UserRepo(SimpleECADbContext dBContext, AppSettingsHelper appsettings)
        {
            _dBContext = dBContext;
            _appsettings = appsettings;
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
                rpassword = AESCryptoHelper.Encrypt(user.rpassword, _appsettings.Secret.Key)
            };
            await _dBContext.TblUserDetails.AddAsync(dbModel);
            return await _dBContext.SaveChangesAsync() > 0;
        }
    }
}
