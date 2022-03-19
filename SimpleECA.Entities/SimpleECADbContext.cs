using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace SimpleECA.Entities
{
    public class SimpleECADbContext: DbContext
    {
        public SimpleECADbContext()
        {
        }
        public SimpleECADbContext(DbContextOptions<SimpleECADbContext> options)
            : base(options)
        { }
        public virtual DbSet<TblUserDetails> TblUserDetails { get; set; }
        public virtual DbSet<TblRoles> TblRoles { get; set; }
        public virtual DbSet<TblBrandMaster> TblBrandMaster { get; set; }
        public virtual DbSet<TblCategoryMaster> TblCategoryMaster { get; set; }
        public virtual DbSet<TblSubCategoryMaster> TblSubCategoryMaster { get; set; }
        public virtual DbSet<TblProducts> TblProducts { get; set; }
        public virtual DbSet<TblProductMapping> TblProductMapping { get; set; }
    }
}
