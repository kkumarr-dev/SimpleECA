using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace SimpleECA.Entities
{
    public class SimpleECADbContext: DbContext
    {
        public int UserId;
        private string _user;
        public SimpleECADbContext()
        {
        }
        public SimpleECADbContext(DbContextOptions<SimpleECADbContext> options, IHttpContextAccessor http, UserResolverService userService)
            : base(options)
        {
            if (http.HttpContext.User.Claims.Any())
            {
                UserId = Convert.ToInt32(http.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == "UserId")?.Value);
            }
            _user = userService.GetUser();
        }
        public virtual DbSet<TblUserDetails> TblUserDetails { get; set; }
        public virtual DbSet<TblRoles> TblRoles { get; set; }
        public virtual DbSet<TblBrandMaster> TblBrandMaster { get; set; }
        public virtual DbSet<TblCategoryMaster> TblCategoryMaster { get; set; }
        public virtual DbSet<TblSubCategoryMaster> TblSubCategoryMaster { get; set; }
        public virtual DbSet<TblProductMaster> TblProductMaster { get; set; }
        public virtual DbSet<TblProductMapping> TblProductMapping { get; set; }
        public virtual DbSet<TblProductDescription> TblProductDescription { get; set; }
        public virtual DbSet<TblProductImages> TblProductImages { get; set; }
        public virtual DbSet<TblUserWishList> TblUserWishList { get; set; }
        public virtual DbSet<TblUserOrders> TblUserOrders { get; set; }
        public virtual DbSet<TblOrderProductMapping> TblOrderProductMapping { get; set; }
        public virtual DbSet<TblUserCart> TblUserCart { get; set; }
        public virtual DbSet<TblUserAddress> TblUserAddress { get; set; }
    }
}
