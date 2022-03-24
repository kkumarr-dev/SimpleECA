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
        private readonly IUserRepo _userRepo;
        public ProductRepo(SimpleECADbContext dBContext, IUserRepo userRepo)
        {
            _dBContext = dBContext;
            _userRepo = userRepo;
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
                                 discountprice = (float)(p.price - ((p.discount / 100) * p.price)),
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
        public async Task<List<ProductViewModel>> GetCartProducts(int userid)
        {
            var productData = await GetAllProducts();
            var cartdata = await _dBContext.TblUserCart.Where(x => x.userid == userid && x.isactive).Select(x => x.productid).ToListAsync();
            productData = productData.Where(x => cartdata.Contains(x.productid)).ToList();
            productData.ForEach(x => x.iscart = true);
            return productData;
        }
        public async Task<List<ProductViewModel>> GetWishListProducts(int userid)
        {
            var productData = await GetAllProducts();
            var wldata = await _dBContext.TblUserWishList.Where(x => x.userid == userid && x.isactive).Select(x => x.productid).ToListAsync();
            productData = productData.Where(x => wldata.Contains(x.productid)).ToList();
            productData.ForEach(x => x.iswishlist = true);
            return productData;
        }
        public async Task<List<OrderViewModel>> GetOrderedProducts(int userid)
        {
            var productData = await GetAllProducts();
            var addressList = await _userRepo.GetUserAddressList(userid);
            var oddata = await (from o in _dBContext.TblUserOrders
                                where o.userid == userid
                                select new OrderViewModel
                                {
                                    addressid = o.addressid,
                                    canceleddate = o.canceleddate,
                                    delivereddate = o.delivereddate,
                                    deliverydate = o.deliverydate,
                                    dispatchdate = o.dispatchdate,
                                    iscanceled = o.iscanceled,
                                    isdelivered = o.isdelivered,
                                    ispaid = o.ispaid,
                                    ordereddate = o.ordereddate,
                                    orderid = o.orderid,
                                    price = o.price,
                                    userid = o.userid,
                                }).ToListAsync();
            foreach (var item in oddata)
            {
                var omData = await _dBContext.TblOrderProductMapping.Where(x => x.orderid == item.orderid).Select(x => x.productid).ToListAsync();
                item.OrderedAddress = addressList.Where(x => x.addressid == item.addressid).FirstOrDefault();
                item.OrderedProducts = productData.Where(x => omData.Contains(x.productid)).ToList();
                item.OrderedProducts.ForEach(x => x.isordered = true);
            }
            return oddata;
        }
        public async Task<bool> ProductAddtoCart(int productId, int userid)
        {
            var res = false;
            var checkdata = await _dBContext.TblUserCart.AnyAsync(x => x.productid == productId && x.userid == userid);
            if (!checkdata)
            {
                var tbldata = new TblUserCart
                {
                    userid = userid,
                    productid = productId,
                    isactive = true,
                    createdat = DateTime.Now
                };
                await _dBContext.TblUserCart.AddAsync(tbldata);
                res = await _dBContext.SaveChangesAsync() > 0;
            }
            return res;
        }
        public async Task<bool> ProductRemovetoCart(int productId, int userid)
        {
            var tbldata = await _dBContext.TblUserCart.Where(x => x.productid == productId).ToListAsync();
            tbldata.ForEach(x => x.isactive = false);
            _dBContext.TblUserCart.UpdateRange(tbldata);
            return await _dBContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> ProductAddtoWishList(int productId, int userid)
        {
            var res = false;
            var checkdata = await _dBContext.TblUserWishList.AnyAsync(x => x.productid == productId && x.userid == userid);
            if (!checkdata)
            {
                var tbldata = new TblUserWishList
                {
                    userid = userid,
                    productid = productId,
                    isactive = true,
                    createdat = DateTime.Now
                };
                await _dBContext.TblUserWishList.AddAsync(tbldata);
                res = await _dBContext.SaveChangesAsync() > 0;
            }
            return res;
        }
        public async Task<bool> ProductRemovetoWishList(int productId, int userid)
        {
            var tbldata = await _dBContext.TblUserWishList.Where(x => x.productid == productId).ToListAsync();
            tbldata.ForEach(x => x.isactive = false);
            _dBContext.TblUserWishList.UpdateRange(tbldata);
            return await _dBContext.SaveChangesAsync() > 0;
        }

        public async Task<List<ProductViewModel>> SearchProducts(string searchText)
        {
            if (searchText == null) return null;
            else
            {
                searchText = searchText.Trim();
                var allProducts = await GetAllProducts();
                var searchResults = allProducts.Where(x => x.productname.ToLower().Contains(searchText.ToLower())).ToList();
                if (searchResults.Count == 0)
                {
                    searchResults = allProducts.Where(x => x.brandName.ToLower().Contains(searchText.ToLower())).ToList();
                }
                if (searchResults.Count == 0)
                {
                    searchResults = allProducts.Where(x => x.categoryName.ToLower().Contains(searchText.ToLower())).ToList();
                }
                if (searchResults.Count == 0)
                {
                    searchResults = allProducts.Where(x => x.subcatName.ToLower().Contains(searchText.ToLower())).ToList();
                }
                return searchResults;
            }
        }

        public async Task<bool> UserCheckOut(UserCheckOutViewModel model)
        {
            var res = false;
            var orderData = new TblUserOrders
            {
                userid = model.userid,
                addressid = model.addressid,
                deliverydate = DateTime.Now.AddDays(7),
                dispatchdate = DateTime.Now.AddDays(2),
                ordereddate = DateTime.Now,
                price = model.totalPrice,
                ispaid = false,
                isdelivered = false,
                iscanceled = false,
            };
            await _dBContext.TblUserOrders.AddAsync(orderData);
            res = await _dBContext.SaveChangesAsync() > 0;
            if (res)
            {
                var ordermap = model.productList.Select(x => new TblOrderProductMapping
                {
                    createdat = DateTime.Now,
                    orderid = orderData.orderid,
                    price = x.price,
                    productid = x.productId,
                    isremoved = false,
                }).ToList();
                await _dBContext.TblOrderProductMapping.AddRangeAsync(ordermap);
                res = await _dBContext.SaveChangesAsync() > 0;
            }
            if (res)
            {
                foreach (var item in model.productList)
                {
                    var cartData = await _dBContext.TblUserCart.Where(x => x.productid == item.productId && x.userid == model.userid).FirstOrDefaultAsync();
                    if (cartData != null)
                    {
                        cartData.isactive = false;
                    }
                    _dBContext.TblUserCart.Update(cartData);
                    res = await _dBContext.SaveChangesAsync() > 0;
                }
            }

            return res;
        }
    }
}
