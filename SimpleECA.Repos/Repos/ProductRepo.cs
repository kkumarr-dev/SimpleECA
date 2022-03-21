using SimpleECA.Entities;
using SimpleECA.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SimpleECA.Repos
{
    public class ProductRepo : IProductRepo
    {
        private readonly SimpleECADbContext _dBContext;
        public ProductRepo(SimpleECADbContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task<List<ProductViewModel>> GetAllProducts()
        {
            var ProductImages = await _dBContext.TblProductImages
                .Where(x => x.isactive)
                .Select(x => new ProductImages
                {
                    imagename = x.imagename,
                    imageurl = x.imageurl,
                    isbanner = x.isbanner,
                    isthumbnail = x.isthumbnail,
                    productid = x.productid
                }).ToListAsync();
            var a = await _dBContext.TblProductMaster.ToListAsync();
            var TblProductDescription = await _dBContext.TblProductDescription.ToListAsync();
            var TblProductMapping = await _dBContext.TblProductMapping.ToListAsync();
            var TblBrandMaster = await _dBContext.TblBrandMaster.ToListAsync();
            var TblCategoryMaster = await _dBContext.TblCategoryMaster.ToListAsync();
            var TblSubCategoryMaster = await _dBContext.TblSubCategoryMaster.ToListAsync();
            var res = await (from p in _dBContext.TblProductMaster
                             join pd in _dBContext.TblProductDescription on p.productid equals pd.productid
                             join pm in _dBContext.TblProductMapping on p.productid equals pm.productid
                             join b in _dBContext.TblBrandMaster on pm.brandid equals b.brandid
                             join ct in _dBContext.TblCategoryMaster on pm.categoryid equals ct.categoryid
                             join sct in _dBContext.TblSubCategoryMaster on pm.subcatid equals sct.subcatid

                             select new ProductViewModel
                             {
                                 productid = p.productid,
                                 productname = p.productname,
                                 price = (float)p.price,
                                 discount = (float)p.discount,
                                 shortdescription = pd.shortdescription,
                                 longdescription = pd.longdescription,
                                 brandId = b.brandid,
                                 brandName = b.brandname,
                                 categoryId = ct.categoryid,
                                 categoryName = ct.categoryname,
                                 subcatId = sct.subcatid,
                                 subcatName = sct.subcategoryname,
                             }).ToListAsync();

            res.ForEach(x => x.ProductImages = ProductImages.Where(e => e.productid == x.productid).ToList());
            return res;
        }
        public async Task<ProductViewModel> GetProductById(int productId)
        {
            var ProductImages = await _dBContext.TblProductImages
                .Where(x => x.isactive && x.productid == productId)
                .Select(x => new ProductImages
                {
                    imagename = x.imagename,
                    imageurl = x.imageurl,
                    isbanner = x.isbanner,
                    isthumbnail = x.isthumbnail,
                    productid = x.productid
                }).ToListAsync();

            var res = await (from p in _dBContext.TblProductMaster
                             join pd in _dBContext.TblProductDescription on p.productid equals pd.productid
                             join pm in _dBContext.TblProductMapping on p.productid equals pm.productid
                             join b in _dBContext.TblBrandMaster on pm.brandid equals b.brandid
                             join ct in _dBContext.TblCategoryMaster on pm.categoryid equals ct.categoryid
                             join sct in _dBContext.TblSubCategoryMaster on pm.subcatid equals sct.subcatid
                             where p.productid == productId
                             select new ProductViewModel
                             {
                                 productid = p.productid,
                                 productname = p.productname,
                                 price = (float)p.price,
                                 discount = (float)p.discount,
                                 shortdescription = pd.shortdescription,
                                 longdescription = pd.longdescription,
                                 brandId = b.brandid,
                                 brandName = b.brandname,
                                 categoryId = ct.categoryid,
                                 categoryName = ct.categoryname,
                                 subcatId = sct.subcatid,
                                 subcatName = sct.subcategoryname,
                                 ProductImages = ProductImages
                             }).FirstOrDefaultAsync();
            return res;
        }
        public async Task<List<ProductViewModel>> GetCartProducts()
        {
            var productData = await GetAllProducts();
            var cartdata = await _dBContext.TblUserCart.Where(x => x.userid == _dBContext.UserId).Select(x=>x.productid).ToListAsync();
            productData = productData.Where(x => cartdata.Contains(x.productid)).ToList();
            return productData;
        }
        public async Task<List<ProductViewModel>> GetWishListProducts()
        {
            var productData = await GetAllProducts();
            var wldata = await _dBContext.TblUserWishList.Where(x => x.userid == _dBContext.UserId).Select(x=>x.productid).ToListAsync();
            productData = productData.Where(x => wldata.Contains(x.productid)).ToList();
            return productData;
        }
        public async Task<bool> ProductAddtoCart(int productId)
        {
            var tbldata = new TblUserCart
            {
                userid = _dBContext.UserId,
                productid = productId,
                isactive = true,
                createdat = DateTime.Now
            };
            await _dBContext.TblUserCart.AddAsync(tbldata);
            return await _dBContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> ProductAddtoWishList(int productId)
        {
            var tbldata = new TblUserWishList
            {
                userid = _dBContext.UserId,
                productid = productId,
                isactive = true,
                createdat = DateTime.Now
            };
            await _dBContext.TblUserWishList.AddAsync(tbldata);
            return await _dBContext.SaveChangesAsync() > 0;
        }
    }
}
