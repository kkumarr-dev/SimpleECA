using SimpleECA.Models.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleECA.Services
{
    public interface IAdminService
    {
        Task<bool> CreateBrand(BrandViewModel model);
        Task<BrandViewModel> GetBrandById(int brandid);
        Task<List<BrandViewModel>> AllBrands();
        Task<bool> DeleteBrandById(int brandid);
    }
}
