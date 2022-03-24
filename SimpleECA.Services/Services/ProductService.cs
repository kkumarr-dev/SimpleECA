using SimpleECA.Models;
using SimpleECA.Repos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleECA.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<ProductViewModel> GetProductById(int productId)
        {
            return await _productRepo.GetProductById(productId);
        }

        public async Task<List<ProductViewModel>> GetAllProducts()
        {
            return await _productRepo.GetAllProducts();
        }

        public async Task<List<ProductViewModel>> GetCartProducts(int userid)
        {
            return await _productRepo.GetCartProducts(userid);
        }

        public async Task<List<ProductViewModel>> GetWishListProducts(int userid)
        {
            return await _productRepo.GetWishListProducts(userid);
        }

        public async Task<bool> ProductAddtoCart(int productId,int userid)
        {
            return await _productRepo.ProductAddtoCart(productId, userid);
        }

        public async Task<bool> ProductAddtoWishList(int productId, int userid)
        {
            return await _productRepo.ProductAddtoWishList(productId,userid);
        }
        public async Task<List<ProductViewModel>> SearchProducts(string searchText)
        {
            return await _productRepo.SearchProducts(searchText);
        }
        public async Task<bool> ProductRemovetoCart(int productId, int userid)
        {
            return await _productRepo.ProductRemovetoCart(productId, userid);
        }
        public async Task<bool> ProductRemovetoWishList(int productId, int userid)
        {
            return await _productRepo.ProductRemovetoWishList(productId, userid);
        }
        public async Task<bool> UserCheckOut(UserCheckOutViewModel model)
        {
            return await _productRepo.UserCheckOut(model);
        }
        public async Task<List<OrderViewModel>> GetOrderedProducts(int userid)
        {
            return await _productRepo.GetOrderedProducts(userid);
        }
    }
}
