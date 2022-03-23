using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleECA.Helpers;
using SimpleECA.Models.Admin;
using SimpleECA.Services;
using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using SimpleECA.Models;
using Microsoft.AspNetCore.Authorization;

namespace SimpleECA.WEB.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IProductService _productService;
        private readonly ISaveFileToLocal _saveFile;
        private readonly IWebHostEnvironment _environment;
        public AdminController(IAdminService adminService, ISaveFileToLocal saveFile, IWebHostEnvironment environment, IProductService productService)
        {
            _adminService = adminService;
            _saveFile = saveFile;
            _environment = environment;
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateBrand()
        {
            return PartialView("_CreateBrandPartial");
        }
        public async Task<IActionResult> AddBrand(BrandViewModel model)
        {
            var formfile = new List<IFormFile>();
            formfile.Add(model.brandlogo);
            var filePaths = _saveFile.SaveFiles(formfile);
            string filename = $@"{filePaths.FirstOrDefault()}";
            var img = new Bitmap(filename);
            var fileName = $"{model.brandname}.jpg";
            var filepath = $@"{_environment.WebRootPath}\Uploads\{fileName}";
            FileCompress.CompressFile(img, filepath, 50);
            var bytes = System.IO.File.ReadAllBytes(filepath);
            var b64 = Convert.ToBase64String(bytes);
            model.brandlogopath = b64;
            var res = await _adminService.CreateBrand(model);
            return Ok(res);
        }
        public async Task<IActionResult> AllBrands()
        {
            var res = await _adminService.AllBrands();
            return PartialView("_AllBrandsPartial", res);
        }
        public async Task<IActionResult> GetBrandById(int brandid)
        {
            var res = await _adminService.GetBrandById(brandid);
            return PartialView("_EditBrandPartial", res);
        }
        public async Task<IActionResult> DeleteBrandById(int brandid)
        {
            var res = await _adminService.DeleteBrandById(brandid);
            return Ok(res);
        }

        public async Task<IActionResult> AllCategories()
        {
            var res = await _adminService.AllCategories();
            return PartialView("_AllCategoriesPartial", res);
        }
        public IActionResult CreateCategory()
        {
            return PartialView("_CreateCategoryPartial");
        }
        public async Task<IActionResult> AddCategory(CategoryViewModel model)
        {
            var res = await _adminService.CreateCategory(model);
            return Ok(res);
        }
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var res = await _adminService.GetCategoryById(id);
            return PartialView("_EditCategoryPartial", res);
        }
        public async Task<IActionResult> DeleteCategoryById(int id)
        {
            var res = await _adminService.DeleteCategoryById(id);
            return Ok(res);
        }

        public async Task<IActionResult> AllSubCategories()
        {
            var res = await _adminService.AllSubCategories();
            return PartialView("_AllSubCategoriesPartial", res);
        }
        public async Task<IActionResult> CreateSubCategory()
        {
            var allCat = await _adminService.AllCategories();
            return PartialView("_CreateSubCategoryPartial", allCat);
        }
        public async Task<IActionResult> AddSubCategory(SubCategoryViewModel model)
        {
            var res = await _adminService.CreateSubCategory(model);
            return Ok(res);
        }
        public async Task<IActionResult> GetSubCategoryById(int id)
        {
            var res = await _adminService.GetSubCategoryById(id);
            res.CategoryViewModel = await _adminService.AllCategories();
            return PartialView("_EditSubCategoryPartial", res);
        }
        public async Task<IActionResult> DeleteSubCategoryById(int id)
        {
            var res = await _adminService.DeleteSubCategoryById(id);
            return Ok(res);
        }

        public async Task<IActionResult> CreateNewProduct()
        {
            var res = new CreateNewProductViewModel
            {
                brandViewModel = await _adminService.AllBrands(),
                categoryViewModel = await _adminService.AllCategories(),
                subCategoryViewModel = await _adminService.AllSubCategories()
            };
            return PartialView("_CreateProductPartial", res);
        }
        public async Task<IActionResult> CreateProducts(ProductViewModel model)
        {
            var imgs = new List<ProductImages>();
            if (model.bannerFile.Length > 0)
            {
                var imgobj = new ProductImages
                {
                    imageFile = model.bannerFile,
                    isbanner = true,
                };
                imgs.Add(imgobj);
            }
            if (model.thumbanailFile.Length > 0)
            {
                var imgobj = new ProductImages
                {
                    imageFile = model.thumbanailFile,
                    isthumbnail = true,
                };
                imgs.Add(imgobj);
            }
            if (model.productFile.Length > 0)
            {
                var imgobj = new ProductImages
                {
                    imageFile = model.productFile,
                };
                imgs.Add(imgobj);
            }
            model.ProductImages = imgs;
            foreach (var item in model.ProductImages)
            {
                var formfile = new List<IFormFile>();
                formfile.Add(item.imageFile);
                var filePaths = _saveFile.SaveFiles(formfile);
                string filename = $@"{filePaths.FirstOrDefault()}";
                var img = new Bitmap(filename);
                var fileName = $"{model.productname}{DateTime.Now.ToString("ddMMMyyyyHHmmss")}.jpg";
                var filepath = $@"{_environment.WebRootPath}\Uploads\{fileName}";
                FileCompress.CompressFile(img, filepath, 50);
                var bytes = System.IO.File.ReadAllBytes(filepath);
                var b64 = Convert.ToBase64String(bytes);
                item.imageurl = b64;
                item.imagename = fileName;
            }

            var res = await _adminService.CreateProducts(model);
            return Ok(res);
        }

        public async Task<IActionResult> AllProducts()
        {
            var res = await _productService.GetAllProducts();
            return PartialView("_AllProductsPartial", res);
        }
    }
}
