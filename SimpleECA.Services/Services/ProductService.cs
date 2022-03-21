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

        public async Task<List<ProductViewModel>> GetCartProducts()
        {
            return await _productRepo.GetCartProducts();
        }

        public async Task<List<ProductViewModel>> GetWishListProducts()
        {
            return await _productRepo.GetWishListProducts();
        }

        public async Task<bool> ProductAddtoCart(int productId)
        {
            return await _productRepo.ProductAddtoCart(productId);
        }

        public async Task<bool> ProductAddtoWishList(int productId)
        {
            return await _productRepo.ProductAddtoWishList(productId);
        }
    }
}
