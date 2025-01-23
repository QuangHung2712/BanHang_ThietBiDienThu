using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using QLBH_Core.Moddel;

namespace QLNhaTro.API
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString(QLBH_Core.Commons.Constants.AppSettingKeys.DEFAULT_CONNECTION);

            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
