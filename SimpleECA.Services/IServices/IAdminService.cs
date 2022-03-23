using SimpleECA.Models;
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

        Task<List<CategoryViewModel>> AllCategories();
        Task<bool> CreateCategory(CategoryViewModel model);
        Task<CategoryViewModel> GetCategoryById(int id);
        Task<bool> DeleteCategoryById(int id);
        Task<List<SubCategoryViewModel>> AllSubCategories();
        Task<bool> CreateSubCategory(SubCategoryViewModel model);
        Task<SubCategoryViewModel> GetSubCategoryById(int id);
        Task<bool> DeleteSubCategoryById(int id);
        Task<bool> CreateProducts(ProductViewModel model);
    }
}
