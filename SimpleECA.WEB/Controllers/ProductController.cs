using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleECA.Helpers.Authentication;
using SimpleECA.Helpers.Enums;
using SimpleECA.Models;
using SimpleECA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleECA.WEB.Controllers
{
    //[Authorize]
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
            return PartialView("_ProductOverViewPartial", res);
        }
        [Authorize]
        public async Task<IActionResult> Cart()
        {
            var userid = Convert.ToInt32(((ClaimsIdentity)User.Identity).GetSpecificClaim(ClaimType.UserId));
            var res = await _productService.GetCartProducts(userid);
            return PartialView("_UserCartPartial", res);
        }
        [Authorize]
        public async Task<IActionResult> GetCartProducts()
        {
            var userid = Convert.ToInt32(((ClaimsIdentity)User.Identity).GetSpecificClaim(ClaimType.UserId));
            var res = await _productService.GetCartProducts(userid);
            return Ok(res);
        }
        [Authorize]
        public async Task<IActionResult> WishList()
        {
            var userid = Convert.ToInt32(((ClaimsIdentity)User.Identity).GetSpecificClaim(ClaimType.UserId));
            var res = await _productService.GetWishListProducts(userid);
            return PartialView("_UserWishListPartial", res);
        }
        [Authorize]
        public async Task<IActionResult> GetWishListProducts()
        {
            var userid = Convert.ToInt32(((ClaimsIdentity)User.Identity).GetSpecificClaim(ClaimType.UserId));
            var res = await _productService.GetWishListProducts(userid);
            return Ok(res);
        }
        [Authorize]
        public async Task<IActionResult> ProductAddtoCart(int productId)
        {
            var userid = Convert.ToInt32(((ClaimsIdentity)User.Identity).GetSpecificClaim(ClaimType.UserId));
            var res = await _productService.ProductAddtoCart(productId, userid);
            return Ok(res);
        }
        [Authorize]
        public async Task<IActionResult> ProductAddtoWishList(int productId)
        {
            var userid = Convert.ToInt32(((ClaimsIdentity)User.Identity).GetSpecificClaim(ClaimType.UserId));
            var res = await _productService.ProductAddtoWishList(productId, userid);
            return Ok(res);
        }
        public async Task<IActionResult> SearchProducts(string searchText)
        {
            var res = await _productService.SearchProducts(searchText);
            return PartialView("_SearchProductsResultPartial", res);
        }

        [Authorize]
        public async Task<IActionResult> ProductRemovetoCart(int productId)
        {
            var userid = Convert.ToInt32(((ClaimsIdentity)User.Identity).GetSpecificClaim(ClaimType.UserId));
            var res = await _productService.ProductRemovetoCart(productId, userid);
            return Ok(res);
        }
        [Authorize]
        public async Task<IActionResult> ProductRemovetoWishList(int productId)
        {
            var userid = Convert.ToInt32(((ClaimsIdentity)User.Identity).GetSpecificClaim(ClaimType.UserId));
            var res = await _productService.ProductRemovetoWishList(productId, userid);
            return Ok(res);
        }
        [Authorize]
        public async Task<IActionResult> ProductCheckOut()
        {
            var userid = Convert.ToInt32(((ClaimsIdentity)User.Identity).GetSpecificClaim(ClaimType.UserId));
            var res = await _productService.GetCartProducts(userid);
            return PartialView("_ProductCheckOutPartial", res);
        }
        [Authorize]
        public async Task<IActionResult> SingleProductCheckOut(int productId)
        {
            var userid = Convert.ToInt32(((ClaimsIdentity)User.Identity).GetSpecificClaim(ClaimType.UserId));
            var res = await _productService.ProductAddtoCart(productId, userid);
            var productsData = await _productService.GetCartProducts(userid);
            productsData = productsData.Where(x => x.productid == productId).ToList();
            return PartialView("_ProductCheckOutPartial", res);
        }
        [Authorize]
        public async Task<IActionResult> ConfirmProductCheckOut(UserCheckOutViewModel model)
        {
            var userid = Convert.ToInt32(((ClaimsIdentity)User.Identity).GetSpecificClaim(ClaimType.UserId));
            model.userid = userid;
            var res = await _productService.UserCheckOut(model);
            return Ok(res);
        }

        public async Task<IActionResult> GetOrderedProducts()
        {
            var userid = Convert.ToInt32(((ClaimsIdentity)User.Identity).GetSpecificClaim(ClaimType.UserId));
            var res = await _productService.GetOrderedProducts(userid);
            return PartialView("_OrderedProductsPartial", res);
        }
    }
}
