using Microsoft.EntityFrameworkCore;
using SimpleECA.Entities;
using SimpleECA.Helpers;
using SimpleECA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var res = false;
            if (user == null) return false;
            var usrData = await _dBContext.TblUserDetails.Where(x => x.email == user.email).FirstOrDefaultAsync();
            if (usrData == null)
            {
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
            }
            res = await _dBContext.SaveChangesAsync() > 0;
            return res;
        }
        public async Task<bool> CreateUserAddress(UserAddressViewModel model)
        {
            var dbModel = new TblUserAddress
            {
                addressline1 = model.addressline1,
                addressline2 = model.addressline2,
                contactnumber = model.contactnumber,
                createdat = DateTime.Now,
                district = model.district,
                isactive = true,
                pincode = model.pincode,
                state = model.state,
                updatedat = DateTime.Now,
                userid = model.userid,
                istemp = model.istemp?false:true
            };
            await _dBContext.TblUserAddress.AddAsync(dbModel);
            var res = await _dBContext.SaveChangesAsync() > 0;
            return res;
        }
        public async Task<List<UserAddressViewModel>> GetUserAddressList(int userid)
        {
            var data = await _dBContext.TblUserAddress
                .Where(x => x.userid == userid)
                .Select(x =>
                new UserAddressViewModel
                {
                    addressid = x.addressid,
                    userid = x.userid,
                    addressline1 = x.addressline1,
                    addressline2 = x.addressline2 ?? "",
                    contactnumber = x.contactnumber,
                    district = x.district,
                    istemp = x.istemp ?? false,
                    pincode = x.pincode,
                    state = x.state
                })
                .ToListAsync();
            return data;
        }
    }
}
