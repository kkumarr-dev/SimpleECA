using SimpleECA.Models;
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

        public async Task<List<CategoryViewModel>> AllCategories()
        {
            return await _adminRepo.AllCategories();
        }

        public async Task<List<SubCategoryViewModel>> AllSubCategories()
        {
            return await _adminRepo.AllSubCategories();
        }

        public async Task<bool> CreateBrand(BrandViewModel model)
        {
            return await _adminRepo.CreateBrand(model);
        }

        public async Task<bool> CreateCategory(CategoryViewModel model)
        {
            return await _adminRepo.CreateCategory(model);
        }

        public async Task<bool> CreateSubCategory(SubCategoryViewModel model)
        {
            return await _adminRepo.CreateSubCategory(model);
        }

        public async Task<bool> DeleteBrandById(int brandid)
        {
            return await _adminRepo.DeleteBrandById(brandid);
        }

        public async Task<bool> DeleteCategoryById(int id)
        {
            return await _adminRepo.DeleteCategoryById(id);
        }

        public async Task<bool> DeleteSubCategoryById(int id)
        {
            return await _adminRepo.DeleteSubCategoryById(id);
        }

        public async Task<BrandViewModel> GetBrandById(int brandid)
        {
            return await _adminRepo.GetBrandById(brandid);
        }

        public async Task<CategoryViewModel> GetCategoryById(int id)
        {
            return await _adminRepo.GetCategoryById(id);
        }

        public async Task<SubCategoryViewModel> GetSubCategoryById(int id)
        {
            return await _adminRepo.GetSubCategoryById(id);
        }
        public async Task<bool> CreateProducts(ProductViewModel model)
        {
            return await _adminRepo.CreateProducts(model);
        }
    }
}
