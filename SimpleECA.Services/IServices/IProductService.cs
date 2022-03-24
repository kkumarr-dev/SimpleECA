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
        Task<List<ProductViewModel>> GetCartProducts(int userid);
        Task<List<ProductViewModel>> GetWishListProducts(int userid);
        Task<bool> ProductAddtoCart(int productId,int userid);
        Task<bool> ProductAddtoWishList(int productId, int userid);
        Task<List<ProductViewModel>> SearchProducts(string searchText);
        Task<bool> ProductRemovetoCart(int productId, int userid);
        Task<bool> ProductRemovetoWishList(int productId, int userid);
        Task<bool> UserCheckOut(UserCheckOutViewModel model);
        Task<List<OrderViewModel>> GetOrderedProducts(int userid);
    }
}
