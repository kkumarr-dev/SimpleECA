using SimpleECA.Models.Admin;
using SimpleECA.Repos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleECA.Services
{
    public class AdminService: IAdminService
    {
        private readonly IAdminRepo _adminRepo;
        public AdminService(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
        }
        public async Task<List<BrandViewModel>> AllBrands()
        {
            return await _adminRepo.AllBrands();
        }
        public async Task<bool> CreateBrand(BrandViewModel model)
        {
            return await _adminRepo.CreateBrand(model);
        }
        public async Task<bool> DeleteBrandById(int brandid)
        {
            return await _adminRepo.DeleteBrandById(brandid);
        }

        public async Task<BrandViewModel> GetBrandById(int brandid)
        {
            return await _adminRepo.GetBrandById(brandid);
        }
    }
}
