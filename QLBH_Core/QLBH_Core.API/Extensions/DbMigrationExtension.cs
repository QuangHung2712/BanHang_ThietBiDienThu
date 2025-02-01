using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QLBH_Core.Moddel;
using System;

namespace QLBH_Core.API.Extensions
{
    public static class DbMigrationExtension
    {
        public static void UseDbMigration(this IApplicationBuilder app, bool isDevelopment)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
                SeedData.Seed(context);

                /*if (isDevelopment)
                {
                    DataSeeding.DevelopmentSeeding(context);
                }
                else
                {
                    DataSeeding.ProductionSeeding(context);
                } */
            }
        }
    }
}
