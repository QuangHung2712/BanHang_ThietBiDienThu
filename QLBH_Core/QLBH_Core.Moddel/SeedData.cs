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
            if (!context.Products.Any())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var newProduct = new List<Product>
                            {
                                new Product {Id = 5,Price = 1000000,Name = "Test 2",ProductTypeId = 1, IsDelete = false,WarrantyPeriod = "6 Năm"},
                                new Product {Id = 6,Price = 2000000,Name = "Test 3",ProductTypeId = 1, IsDelete = false,WarrantyPeriod = "6 Tháng"},
                                new Product {Id = 7,Price = 3000000,Name = "Test 4",ProductTypeId = 1, IsDelete = false,WarrantyPeriod = "6 Tháng"},
                                new Product {Id = 8,Price = 4000000,Name = "Test 5",ProductTypeId = 1, IsDelete = false,WarrantyPeriod = "6 Tháng"},
                                new Product {Id = 9,Price = 5000000,Name = "Test 6",ProductTypeId = 1, IsDelete = false,WarrantyPeriod = "6 Tháng"},
                                new Product {Id = 10,Price = 6000000,Name = "Test 7",ProductTypeId = 3, IsDelete = false,WarrantyPeriod = "6 Năm"},
                                new Product {Id = 11,Price = 7000000,Name = "Test 8",ProductTypeId = 3, IsDelete = false,WarrantyPeriod = "6 Năm"},
                                new Product {Id = 12,Price = 8000000,Name = "Test 9",ProductTypeId = 3, IsDelete = false,WarrantyPeriod = "6 Năm"},
                                new Product {Id = 13,Price = 9000000,Name = "Test 10",ProductTypeId = 3, IsDelete = false,WarrantyPeriod = "6 Tháng"},
                                new Product {Id = 14,Price = 9000000,Name = "Name 1",ProductTypeId = 6, IsDelete = false,WarrantyPeriod = "6 Tháng"},
                                new Product {Id = 15,Price = 8000000,Name = "Name 2",ProductTypeId = 6, IsDelete = false,WarrantyPeriod = "6 Tháng"},
                                new Product {Id = 16,Price = 7000000,Name = "Name 3",ProductTypeId = 6, IsDelete = false,WarrantyPeriod = "6 Năm"},
                                new Product {Id = 17,Price = 6000000,Name = "Tủ lạnh 1",ProductTypeId = 4, IsDelete = false,WarrantyPeriod = "6 Tháng"},
                                new Product {Id = 18,Price = 5000000,Name = "Tủ lạnh 2",ProductTypeId = 4, IsDelete = false,WarrantyPeriod = "6 Tháng"},
                                new Product {Id = 19,Price = 4000000,Name = "Tủ lạnh 3",ProductTypeId = 4, IsDelete = false,WarrantyPeriod = "6 Tháng"},
                                new Product {Id = 20,Price = 3000000,Name = "Bếp 1",ProductTypeId = 5, IsDelete = false,WarrantyPeriod = "6 Năm"},
                                new Product {Id = 21,Price = 2000000,Name = "Bếp 2",ProductTypeId = 5, IsDelete = false,WarrantyPeriod = "6 Năm"}

                            };
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                        context.Products.AddRange(newProduct);
                        context.SaveChanges();
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");
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
