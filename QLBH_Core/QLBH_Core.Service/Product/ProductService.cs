using Microsoft.AspNetCore.Http;
using QLBH_Core.Commons;
using QLBH_Core.Commons.CustomException;
using QLBH_Core.Moddel;
using QLBH_Core.Moddel.Entity;
using QLBH_Core.Moddel.Model.RequestModels;
using QLBH_Core.Moddel.Model.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Core.Service.ProductS
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _Context;

        public ProductService(AppDbContext appDbContext)
        {
            _Context = appDbContext;
        }
        public List<GetAllProductResModel> GetAll()
        {
            var result =  _Context.Products.Where(item=> !item.IsDelete).Select(item=>new GetAllProductResModel
            {
                Id = item.Id,  
                Name = item.Name,
                Price = item.Price,
                ProductTypeName = item.ProductType.Name,
                WarrantyPeriod = item.WarrantyPeriod,
            }).ToList();
            if(result.Count <= 0)
            {
                throw new NotFoundException("Sản phẩm");
            }
            return result;
        }
        public GetDetailProductResModel GetDetail(long Id)
        {
            var result = _Context.Products.GetAvailableById(Id);
                return new GetDetailProductResModel
            {
                Id = result.Id,
                Name = result.Name,
                ProductTypeId = result.ProductTypeId,
                ProductTypeName = _Context.ProductTypes.GetById(result.ProductTypeId).Name,
                Price = result.Price,
                WarrantyPeriod= result.WarrantyPeriod,  
                InfoProduct = _Context.InfoProducts.Where(item=> item.ProductId == result.Id).Select(record=> new InfoProductGetResModel { Id = record.Id,Name = record.Name, Describe = record.Describe}).ToList(),
                PathImg = Functions.ConverPathIMG(_Context.ImgProducts.Where(item => item.ProductId == result.Id).Select(record => record.Path).ToList()),
            };
        }
        public async Task CreateEdit(CreateEditProductReqModel data,List<IFormFile> img)
        {
            //Kiểm tra xem loại sản phẩm có tồn tại không
            _Context.ProductTypes.IsGetById(data.ProductType);
            if((data.InfoProduct == null ) || (data.InfoProduct.Count == 0))
            {
                throw new Exception("Bạn vui lòng thêm ít nhất 1 thông số");
            }

            if(data.Id <= 0)
            {
                using var transaction = await _Context.Database.BeginTransactionAsync();
                try
                {
                    // Thêm mới sản phẩm
                    var newProduct = new Product
                    {
                        Name = data.Name,
                        Price = data.Price,
                        ProductTypeId = data.ProductType,
                        WarrantyPeriod = data.WarrantyPeriod,
                    };
                    _Context.Products.Add(newProduct);
                    await _Context.SaveChangesAsync();

                    // Thêm mới thông tin sản phẩm
                    var infoProductNew = data.InfoProduct.Select(item => new InfoProduct
                    {
                        Name = item.Name,
                        Describe = item.Describe,
                        ProductId = newProduct.Id,
                    }).ToList();
                    await _Context.InfoProducts.AddRangeAsync(infoProductNew);  
                    //Lưu ảnh vào local
                    string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), $@"{Constants.DefaultValue.DEFAULT_BASE_Directory_IMG}\images\Products\{newProduct.Id}");
                    var filepath =  Functions.SaveImgToDB(img, directoryPath);
                    var imgProducts = filepath.Select(file => new ImgProduct
                    {
                        ProductId = newProduct.Id,
                        Path = file
                    }).ToList();

                    _Context.ImgProducts.AddRange(imgProducts);

                    await _Context.SaveChangesAsync();

                    // Commit transaction nếu tất cả đều thành công
                    await transaction.CommitAsync();
                }
                catch
                {
                    // Rollback nếu xảy ra lỗi
                    await transaction.RollbackAsync();
                    throw; // Ném lại ngoại lệ để xử lý ở tầng cao hơn
                }
            }
            else
            {
                //Cập nhận lại sản phẩm
                var productData = _Context.Products.GetAvailableById(data.Id);
                productData.Name = data.Name;
                productData.Price = data.Price;
                productData.ProductTypeId = data.ProductType;
                productData.WarrantyPeriod = data.WarrantyPeriod;
                _Context.Products.Update(productData);

                //Xoá các thông tin sản phẩm cũ và thêm mới lại
                var infoProduct = _Context.InfoProducts.Where(item=> item.ProductId == productData.Id).ToList();
                _Context.InfoProducts.RemoveRange(infoProduct);

                // Thêm mới thông tin sản phẩm
                var infoProductNew = data.InfoProduct.Select(item => new InfoProduct
                {
                    Name = item.Name,
                    Describe = item.Describe,
                    ProductId = productData.Id,
                }).ToList();
                await _Context.InfoProducts.AddRangeAsync(infoProductNew);

                //Xoá các đường dẫn ảnh sản phẩm cũ
                var imgProductsOld =  _Context.ImgProducts.Where(item => item.ProductId == productData.Id).ToList();
                _Context.ImgProducts.RemoveRange(imgProductsOld);
                string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), $@"{Constants.DefaultValue.DEFAULT_BASE_Directory_IMG}\images\Products\{productData.Id}");
                var filepath = Functions.SaveImgToDB(img, directoryPath);
                var imgProducts = filepath.Select(file => new ImgProduct
                {
                    ProductId = productData.Id,
                    Path = file
                }).ToList();

                _Context.ImgProducts.AddRange(imgProducts);

                await _Context.SaveChangesAsync();
            }
        }
        public async Task Delete(long Id)
        {
            _Context.Products.Delete(Id);
            var imgProductsOld = _Context.ImgProducts.Where(item => item.ProductId == Id).ToList();
            _Context.ImgProducts.RemoveRange(imgProductsOld);
            await _Context.SaveChangesAsync();

            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), $@"{Constants.DefaultValue.DEFAULT_BASE_Directory_IMG}\images\Products\{Id}");
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }
        public List<ResultFindProductResModel> FindProduct(string? name)
        {
            var result = _Context.Products.Where(item => 
                (string.IsNullOrEmpty(name) || item.Name.ToLower().Contains(name.ToLower())))
                .Select(record => new ResultFindProductResModel
                {
                    Id = record.Id,
                    Name = record.Name,
                    Price = record.Price,
                    ProductType = record.ProductTypeId,
                    PathImg = Functions.ConverPathIMG(_Context.ImgProducts.Where(img => img.ProductId == record.Id).Select(img => img.Path).FirstOrDefault()?? ""),
                }).ToList();
            if(result.Count == 0)
            {
                throw new Exception("Không có sản phẩm nào thoả mãn điều kiện của bạn");
            }
            return result;
        }
        public GetPriceProductResModel GetPriceByProductName(string? name)
        {
            var data = _Context.Products
                .Where(item => string.IsNullOrEmpty(name) || item.Name.ToLower().Contains(name.ToLower()))
                .ToList();
            if(data.Count == 0)
            {
                throw new Exception("Không có sản phẩm nào thoả mãn điều kiện của bạn");
            }

            return new GetPriceProductResModel
            {
                PriceFrom = data.Min(item => item.Price),
                PriceTo = data.Max(item => item.Price),
            };
        }
        public List<ResultFindProductResModel> GetProductByType (long productType, long Id)
        {
            var result = _Context.Products.Where(item=> item.ProductTypeId == productType && !item.IsDelete && item.Id != Id).Select(record=> new ResultFindProductResModel
            {
                Id = record.Id, 
                Name = record.Name, 
                Price = record.Price,
                ProductType = record.ProductTypeId,
                PathImg = Functions.ConverPathIMG(_Context.ImgProducts.Where(img => img.ProductId == record.Id).Select(img => img.Path).FirstOrDefault() ?? ""),
            }).ToList();
            return result;
        }
    }
}
