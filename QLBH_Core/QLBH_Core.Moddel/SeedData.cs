using Microsoft.EntityFrameworkCore;
using QLBH_Core.Moddel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QLBH_Core.Commons.Constants;

namespace QLBH_Core.Moddel
{
    public partial class SeedData
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var newLandlord = new List<User>
                        {
                            new User{Id = 1,Email = "admin", Password = "1",Name="Quản trị viên",PathAvatar = DefaultValue.DEFAULT_IMG_AVATAR,Position=0,IsDelete = false},
                        };
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Users] ON");
                        context.Users.AddRange(newLandlord);
                        context.SaveChanges();
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Users] OFF");
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

    }
}
