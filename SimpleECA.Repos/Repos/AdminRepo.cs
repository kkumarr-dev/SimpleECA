using Microsoft.EntityFrameworkCore;
using SimpleECA.Entities;
using SimpleECA.Models;
using SimpleECA.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleECA.Repos
{
    public class AdminRepo : IAdminRepo
    {
        private readonly SimpleECADbContext _dBContext;
        public AdminRepo(SimpleECADbContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task<List<BrandViewModel>> AllBrands()
        {
            var branddata = await _dBContext.TblBrandMaster
                .Select(x =>
                new BrandViewModel
                {
                    brandid = x.brandid,
                    branddescription = x.branddescription,
                    brandlogopath = x.brandlogo,
                    brandname = x.brandname
                })
                .ToListAsync();

            return branddata;
        }
        public async Task<bool> CreateBrand(BrandViewModel model)
        {
            var branddata = await _dBContext.TblBrandMaster.Where(x => x.brandid == model.brandid).FirstOrDefaultAsync();
            if (branddata != null)
            {
                branddata.brandname = model.brandname;
                branddata.branddescription = model.branddescription;
                branddata.brandlogo = model.brandlogopath;
                branddata.updatedat = DateTime.Now;

                _dBContext.TblBrandMaster.Update(branddata);
            }
            else
            {
                var tbldata = new TblBrandMaster
                {
                    branddescription = model.branddescription,
                    brandlogo = model.brandlogopath,
                    brandname = model.brandname,
                    createdat = DateTime.Now,
                    updatedat = DateTime.Now,
                    isactive = true
                };
                await _dBContext.TblBrandMaster.AddAsync(tbldata);
            }
            return await _dBContext.SaveChangesAsync() > 0;
        }
        public async Task<BrandViewModel> GetBrandById(int brandid)
        {
            var branddata = await _dBContext.TblBrandMaster.Where(x => x.brandid == brandid)
                .Select(x =>
                new BrandViewModel
                {
                    brandid = x.brandid,
                    branddescription = x.branddescription,
                    brandname = x.brandname,
                    brandlogopath = x.brandlogo,
                    isactive = x.isactive ?? false,
                    createdat = x.createdat,
                    updatedat = x.updatedat
                })
                .FirstOrDefaultAsync();
            return branddata;
        }
        public async Task<bool> DeleteBrandById(int brandid)
        {
            var branddata = await _dBContext.TblBrandMaster.Where(x => x.brandid == brandid).FirstOrDefaultAsync();
            branddata.isactive = false;
            _dBContext.TblBrandMaster.Update(branddata);
            return await _dBContext.SaveChangesAsync() > 0;
        }


        public async Task<List<CategoryViewModel>> AllCategories()
        {
            var data = await _dBContext.TblCategoryMaster
                .Select(x =>
                new CategoryViewModel
                {
                    categoryid = x.categoryid,
                    categoryname = x.categoryname,
                    createdat = x.createdat,
                    isactive = x.isactive,
                    updatedat = x.updatedat
                })
                .ToListAsync();

            return data;
        }
        public async Task<bool> CreateCategory(CategoryViewModel model)
        {
            var data = await _dBContext.TblCategoryMaster.Where(x => x.categoryid == model.categoryid).FirstOrDefaultAsync();
            if (data != null)
            {
                data.categoryname = model.categoryname;
                data.updatedat = DateTime.Now;
                _dBContext.TblCategoryMaster.Update(data);
            }
            else
            {
                var tbldata = new TblCategoryMaster
                {
                    categoryname = model.categoryname,
                    createdat = DateTime.Now,
                    updatedat = DateTime.Now,
                    isactive = true
                };
                await _dBContext.TblCategoryMaster.AddAsync(tbldata);
            }
            return await _dBContext.SaveChangesAsync() > 0;
        }
        public async Task<CategoryViewModel> GetCategoryById(int id)
        {
            var branddata = await _dBContext.TblCategoryMaster.Where(x => x.categoryid == id)
                .Select(x =>
                new CategoryViewModel
                {
                    categoryid = x.categoryid,
                    categoryname = x.categoryname,
                    isactive = x.isactive,
                    createdat = x.createdat,
                    updatedat = x.updatedat
                })
                .FirstOrDefaultAsync();
            return branddata;
        }
        public async Task<bool> DeleteCategoryById(int id)
        {
            var data = await _dBContext.TblCategoryMaster.Where(x => x.categoryid == id).FirstOrDefaultAsync();
            data.isactive = false;
            _dBContext.TblCategoryMaster.Update(data);
            return await _dBContext.SaveChangesAsync() > 0;
        }

        public async Task<List<SubCategoryViewModel>> AllSubCategories()
        {
            var data = await (from sc in _dBContext.TblSubCategoryMaster
                              join c in _dBContext.TblCategoryMaster on sc.categoryid equals c.categoryid
                              select new SubCategoryViewModel
                              {
                                  categoryid = c.categoryid,
                                  categoryname = c.categoryname,
                                  isactive = sc.isactive,
                                  createdat = sc.createdat,
                                  updatedat = sc.updatedat,
                                  subcategoryid = sc.subcatid,
                                  subcategoryname = sc.subcategoryname
                              }).ToListAsync();

            return data;
        }
        public async Task<bool> CreateSubCategory(SubCategoryViewModel model)
        {
            var data = await _dBContext.TblSubCategoryMaster.Where(x => x.subcatid == model.subcategoryid).FirstOrDefaultAsync();
            if (data != null)
            {
                data.subcategoryname = model.subcategoryname;
                data.updatedat = DateTime.Now;
                _dBContext.TblSubCategoryMaster.Update(data);
            }
            else
            {
                var tbldata = new TblSubCategoryMaster
                {
                    subcategoryname = model.subcategoryname,
                    createdat = DateTime.Now,
                    updatedat = DateTime.Now,
                    isactive = true,
                    categoryid = model.categoryid
                };
                await _dBContext.TblSubCategoryMaster.AddAsync(tbldata);
            }
            return await _dBContext.SaveChangesAsync() > 0;
        }
        public async Task<SubCategoryViewModel> GetSubCategoryById(int id)
        {
            var data = await (from sc in _dBContext.TblSubCategoryMaster
                                   join c in _dBContext.TblCategoryMaster on sc.categoryid equals c.categoryid
                                   where sc.subcatid == id
                                   select new SubCategoryViewModel
                                   {
                                       categoryid = c.categoryid,
                                       categoryname = c.categoryname,
                                       isactive = sc.isactive,
                                       createdat = sc.createdat,
                                       updatedat = sc.updatedat,
                                       subcategoryid = sc.subcatid,
                                       subcategoryname = sc.subcategoryname
                                   }).FirstOrDefaultAsync();
            return data;
        }
        public async Task<bool> DeleteSubCategoryById(int id)
        {
            var data = await _dBContext.TblSubCategoryMaster.Where(x => x.subcatid == id).FirstOrDefaultAsync();
            data.isactive = false;
            _dBContext.TblSubCategoryMaster.Update(data);
            return await _dBContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateProducts(ProductViewModel model)
        {
            var res = false;
            if (model == null) return res;
            var productModel = new TblProductMaster
            {
                productname = model.productname,
                createdat = DateTime.Now,
                discount = model.discount,
                isactive = true,
                isinoffer = model.isinoffer,
                price = model.price,
                updatedat = DateTime.Now
            };
            await _dBContext.TblProductMaster.AddAsync(productModel);
            res = await _dBContext.SaveChangesAsync() > 0;
            if (res)
            {
                var productDesc = new TblProductDescription
                {
                    createdat = DateTime.Now,
                    longdescription = model.longdescription,
                    productid = productModel.productid,
                    shortdescription = model.shortdescription,
                    updatedat = DateTime.Now
                };
                await _dBContext.TblProductDescription.AddAsync(productDesc);
                res = await _dBContext.SaveChangesAsync() > 0;
            }
            if (res)
            {
                foreach (var item in model.ProductImages)
                {
                    var productImages = new TblProductImages
                    {
                        createdat = DateTime.Now,
                        imagename = item.imagename,
                        imageurl = item.imageurl,
                        isactive = true,
                        isbanner = item.isbanner,
                        isthumbnail = item.isthumbnail,
                        productid = productModel.productid,
                        updatedat = DateTime.Now
                    };
                    await _dBContext.TblProductImages.AddAsync(productImages);
                    res = await _dBContext.SaveChangesAsync() > 0;
                }
            }
            if (res)
            {
                var productMapping = new TblProductMapping
                {
                    brandid = model.brandId,
                    categoryid = model.categoryId,
                    createdat = DateTime.Now,
                    isactive = true,
                    productid = productModel.productid,
                    subcatid = model.subcatId,
                    updatedat = DateTime.Now
                };
                await _dBContext.TblProductMapping.AddAsync(productMapping);
                res = await _dBContext.SaveChangesAsync() > 0;
            }
            
            return res;
        }

    }
}
