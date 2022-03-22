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
    public class AdminRepo : IAdminRepo
    {
        private readonly SimpleECADbContext _dBContext;
        public AdminRepo(SimpleECADbContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task<List<BrandViewModel>> AllBrands()
        {
            var branddata = await _dBContext.TblBrandMaster
                .Select(x =>
                new BrandViewModel
                {
                    brandid = x.brandid,
                    branddescription = x.branddescription,
                    brandlogopath = x.brandlogo,
                    brandname = x.brandname
                })
                .ToListAsync();

            return branddata;
        }
        public async Task<bool> CreateBrand(BrandViewModel model)
        {
            var branddata = await _dBContext.TblBrandMaster.Where(x => x.brandid == model.brandid).FirstOrDefaultAsync();
            if (branddata != null)
            {
                branddata.brandname = model.brandname;
                branddata.branddescription = model.branddescription;
                branddata.brandlogo = model.brandlogopath;
                branddata.updatedat = DateTime.Now;

                _dBContext.TblBrandMaster.Update(branddata);
            }
            else
            {
                var tbldata = new TblBrandMaster
                {
                    branddescription = model.branddescription,
                    brandlogo = model.brandlogopath,
                    brandname = model.brandname,
                    createdat = DateTime.Now,
                    updatedat = DateTime.Now,
                    isactive = true
                };
                await _dBContext.TblBrandMaster.AddAsync(tbldata);
            }
            return await _dBContext.SaveChangesAsync() > 0;
        }
        public async Task<BrandViewModel> GetBrandById(int brandid)
        {
            var branddata = await _dBContext.TblBrandMaster.Where(x => x.brandid == brandid)
                .Select(x =>
                new BrandViewModel
                {
                    brandid = x.brandid,
                    branddescription = x.branddescription,
                    brandname = x.brandname,
                    brandlogopath = x.brandlogo,
                    isactive = x.isactive ?? false,
                    createdat = x.createdat,
                    updatedat = x.updatedat
                })
                .FirstOrDefaultAsync();
            return branddata;
        }
        public async Task<bool> DeleteBrandById(int brandid)
        {
            var branddata = await _dBContext.TblBrandMaster.Where(x => x.brandid == brandid).FirstOrDefaultAsync();
            branddata.isactive = false;
            _dBContext.TblBrandMaster.Update(branddata);
            return await _dBContext.SaveChangesAsync() > 0;
        }
    }
}
