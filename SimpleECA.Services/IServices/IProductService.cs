using SimpleECA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleECA.Services
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetAllProducts();
        Task<ProductViewModel> GetProductById(int productId);
        Task<List<ProductViewModel>> GetCartProducts();
        Task<List<ProductViewModel>> GetWishListProducts();
        Task<bool> ProductAddtoCart(int productId);
        Task<bool> ProductAddtoWishList(int productId);
    }
}
