using Microsoft.EntityFrameworkCore;
using QLBH_Core.Moddel.Entity;

namespace QLBH_Core.Moddel
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<InfoProduct> InfoProducts { get; set; }
        public DbSet<ImgProduct> ImgProducts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Orders> Orders { get; set; }


    }
}

