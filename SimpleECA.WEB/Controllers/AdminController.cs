using Microsoft.AspNetCore.Mvc;
using SimpleECA.Models.Admin;
using SimpleECA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleECA.WEB.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
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
            var res = await _adminService.CreateBrand(model);
            return Ok(res);
        }
        public async Task<IActionResult> GetBrandById(int brandid)
        {
            var res = await _adminService.GetBrandById(brandid);
            return Ok(res);
        }
    }
}
