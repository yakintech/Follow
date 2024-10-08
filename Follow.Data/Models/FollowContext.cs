using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow.Data.Models
{
    public class FollowContext : DbContext
    {
        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-EET2RGT;Database=FollowDb;Trusted_Connection=True;TrustServerCertificate=True");
        }

        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
