using SimpleECA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleECA.Repos
{
    public interface IProductRepo
    {
        Task<List<ProductViewModel>> GetAllProducts();
        Task<ProductViewModel> GetProductById(int productId);
        Task<List<ProductViewModel>> GetCartProducts(int userid);
        Task<List<ProductViewModel>> GetWishListProducts(int userid);
        Task<bool> ProductAddtoCart(int productId,int userid);
        Task<bool> ProductAddtoWishList(int productId, int userid);
    }
}
