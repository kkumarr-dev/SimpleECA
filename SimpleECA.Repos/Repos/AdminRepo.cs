using Microsoft.EntityFrameworkCore;
using SimpleECA.Entities;
using SimpleECA.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleECA.Repos
{
    public class AdminRepo:IAdminRepo
    {
        private readonly SimpleECADbContext _dBContext;
        public AdminRepo(SimpleECADbContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task<bool> CreateBrand(BrandViewModel model)
        {
            var tbldata = new TblBrandMaster
            {
                branddescription = model.branddescription,
                brandlogo = model.branddescription,
                brandname = model.brandname,
                createdat = DateTime.Now,
                updatedat = DateTime.Now
            };
            await _dBContext.TblBrandMaster.AddAsync(tbldata);
            return await _dBContext.SaveChangesAsync() > 0;
        }
        public async Task<BrandViewModel> GetBrandById(int brandid)
        {
            var branddata = await _dBContext.TblBrandMaster.Where(x => x.brandid == brandid)
                .Select(x=>
                new BrandViewModel
                {
                    brandid = x.brandid,
                    branddescription = x.branddescription,
                    brandlogo = x.brandlogo,
                    brandname = x.brandname
                })
                .FirstOrDefaultAsync();
            return branddata;
        }
    }
}
