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
    }
}
