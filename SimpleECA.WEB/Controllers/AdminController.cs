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

namespace SimpleECA.WEB.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly ISaveFileToLocal _saveFile;
        private readonly IWebHostEnvironment _environment;
        public AdminController(IAdminService adminService, ISaveFileToLocal saveFile, IWebHostEnvironment environment)
        {
            _adminService = adminService;
            _saveFile = saveFile;
            _environment = environment;
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
    }
}
