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
        public async Task<bool> CreateBrand(BrandViewModel model)
        {
            return await _adminRepo.CreateBrand(model);
        }

        public async Task<BrandViewModel> GetBrandById(int brandid)
        {
            return await _adminRepo.GetBrandById(brandid);
        }
    }
}
