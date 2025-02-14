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
            if (!context.ProductTypes.Any())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var newProductType = new List<ProductType>
                            {
                                new ProductType{Id = 1,Name = "Test"},
                                new ProductType{Id = 2,Name = "Test thử"},
                                new ProductType{Id = 3,Name = "Vẫn là test"},
                                new ProductType{Id = 4,Name = "Tủ lạnh"},
                                new ProductType{Id = 5,Name = "Bếp"},
                                new ProductType{Id = 6,Name = "Name"}
                            };
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductTypes] ON");
                        context.ProductTypes.AddRange(newProductType);
                        context.SaveChanges();
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductTypes] OFF");
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
            if (!context.ImgProducts.Any())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var newImgProduct = new List<ImgProduct>
                            {
                                new ImgProduct{Id = 1,ProductId = 5, Path = "D:\\Du_An\\BanHang_ThietBiDienThu\\QLBH_Application\\public\\images\\Products\\5\\f78d29bd-da06-4dec-9153-248133379b61.jpg"},
                                new ImgProduct{Id = 2,ProductId = 6, Path = "D:\\Du_An\\BanHang_ThietBiDienThu\\QLBH_Application\\public\\images\\Products\\6\\e5982bef-d110-4333-8f92-b2036ed579e5.jpg"},
                                new ImgProduct{Id = 3,ProductId = 7, Path = "D:\\Du_An\\BanHang_ThietBiDienThu\\QLBH_Application\\public\\images\\Products\\7\\428e803a-a12b-47e7-94f7-f91b3347d6b7.jpg"},
                                new ImgProduct{Id = 4,ProductId = 8, Path = "D:\\Du_An\\BanHang_ThietBiDienThu\\QLBH_Application\\public\\images\\Products\\8\\5ece6ef4-4d0a-461c-b0ef-f1e0deae043f.jpg"},
                                new ImgProduct{Id = 5,ProductId = 9, Path = "D:\\Du_An\\BanHang_ThietBiDienThu\\QLBH_Application\\public\\images\\Products\\9\\159a946c-7721-4013-ba94-76c3be2d0caa.jpg"},
                                new ImgProduct{Id = 6,ProductId = 10, Path = "D:\\Du_An\\BanHang_ThietBiDienThu\\QLBH_Application\\public\\images\\Products\\10\\710a6068-a0a5-4414-9b86-074c5bd6e3a1.jpg"},
                                new ImgProduct{Id = 7,ProductId = 11, Path = "D:\\Du_An\\BanHang_ThietBiDienThu\\QLBH_Application\\public\\images\\Products\\11\\53b43682-4b47-4b27-a241-be3072ddc714.jpg"},
                                new ImgProduct{Id = 8,ProductId = 12, Path = "D:\\Du_An\\BanHang_ThietBiDienThu\\QLBH_Application\\public\\images\\Products\\14\\57318506-87b0-49b9-bcb7-d8fd15caeb9e.jpg"},
                                new ImgProduct{Id = 9,ProductId = 13, Path = "D:\\Du_An\\BanHang_ThietBiDienThu\\QLBH_Application\\public\\images\\Products\\13\\5969ca7c-22e5-47e6-92fd-4437fbc6e1f8.jpg"},
                                new ImgProduct{Id = 10,ProductId = 14, Path = "D:\\Du_An\\BanHang_ThietBiDienThu\\QLBH_Application\\public\\images\\Products\\12\\0d7ac6d0-c34b-4b9e-9ade-edece7c1fbbe.jpg"},
                                new ImgProduct{Id = 11,ProductId = 15, Path = "D:\\Du_An\\BanHang_ThietBiDienThu\\QLBH_Application\\public\\images\\Products\\21\\269832a5-0da3-4764-8881-bab36ef66ebd.jpg"},
                                new ImgProduct{Id = 12,ProductId = 16, Path = "D:\\Du_An\\BanHang_ThietBiDienThu\\QLBH_Application\\public\\images\\Products\\20\\bab932c1-0ad1-4dd4-9f96-67383f35baf0.jpg"},
                                new ImgProduct{Id = 13,ProductId = 17, Path = "D:\\Du_An\\BanHang_ThietBiDienThu\\QLBH_Application\\public\\images\\Products\\19\\810edc03-6338-43cb-83d3-28a9f1ced13a.jpg"},
                                new ImgProduct{Id = 14,ProductId = 18, Path = "D:\\Du_An\\BanHang_ThietBiDienThu\\QLBH_Application\\public\\images\\Products\\18\\d6b1a638-af4d-465e-9506-7432cf0986da.jpg"},
                                new ImgProduct{Id = 15,ProductId = 19, Path = "D:\\Du_An\\BanHang_ThietBiDienThu\\QLBH_Application\\public\\images\\Products\\15\\299eb22e-dfd9-400f-a232-e955cb468cd4.jpg"},
                                new ImgProduct{Id = 16,ProductId = 20, Path = "D:\\Du_An\\BanHang_ThietBiDienThu\\QLBH_Application\\public\\images\\Products\\16\\b0a3228c-7b77-46d6-a53a-717a822179d7.jpg"},
                                new ImgProduct{Id = 17,ProductId = 21, Path = "D:\\Du_An\\BanHang_ThietBiDienThu\\QLBH_Application\\public\\images\\Products\\17\\551f5a6f-797a-4a46-a7e6-da9bae9216fc.jpg"}

                            };
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ImgProducts] ON");
                        context.ImgProducts.AddRange(newImgProduct);
                        context.SaveChanges();
                        context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ImgProducts] OFF");
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
