using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleECA.Models;
using SimpleECA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleECA.WEB.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var res = await _productService.GetAllProducts();
            return View(res);
        }
        public async Task<IActionResult> GetAllProducts()
        {
            var res = await _productService.GetAllProducts();
            return View(res);
        }
        public async Task<IActionResult> GetProductById(int productId)
        {
            var res = await _productService.GetProductById(productId);
            return View(res);
        }
        public async Task<IActionResult> GetCartProducts()
        {
            var res = await _productService.GetCartProducts();
            return View(res);
        }
        public async Task<IActionResult> GetWishListProducts()
        {
            var res = await _productService.GetWishListProducts();
            return View(res);
        }
        public async Task<IActionResult> ProductAddtoCart(int productId)
        {
            var res = await _productService.ProductAddtoCart(productId);
            return Ok(res);
        }
        public async Task<IActionResult> ProductAddtoWishList(int productId)
        {
            var res = await _productService.ProductAddtoWishList(productId);
            return Ok(res);
        }
    }
}
