using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
            var result = _Context.Products.Where(item => !item.IsDelete).Select(item => new GetAllProductResModel
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                ProductTypeName = item.ProductType.Name,
                WarrantyPeriod = item.WarrantyPeriod,
            }).ToList();
            return result;
        }
        public GetDetailProductResModel GetDetail(string? nameSlug, long? Id)
        {
            
            var result = _Context.Products.Where(item => (Id != null && item.Id == Id) || (Id == null && !string.IsNullOrEmpty(nameSlug) && item.NameSlug == nameSlug) && !item.IsDelete)
                .Include(item=> item.ProductType)
                .Select(result=> new GetDetailProductResModel
            {
                Id = result.Id,
                Name = result.Name,
                ProductTypeId = result.ProductTypeId,
                ProductTypeName = result.ProductType.Name,
                Capacity = result.Capacity,
                Manufacturer = result.Manufacturer,
                Size = result.Size,
                Price = result.Price,
                WarrantyPeriod = result.WarrantyPeriod,
                InfoProduct = _Context.InfoProducts.Where(item => item.ProductId == result.Id).Select(record => new InfoProductGetResModel { Id = record.Id, Name = record.Name, Describe = record.Describe }).ToList(),
                PathImg = Functions.ConverPathIMG(_Context.ImgProducts.Where(item => item.ProductId == result.Id).Select(record => record.Path).ToList()),
            }).FirstOrDefault() ?? throw new NotFoundException("Sản phẩm");
            return result;
        }
        public async Task CreateEdit(CreateEditProductReqModel data, List<IFormFile> img)
        {
            //Kiểm tra xem loại sản phẩm có tồn tại không
            _Context.ProductTypes.IsGetById(data.ProductType);
            if ((data.InfoProduct == null) || (data.InfoProduct.Count == 0))
            {
                throw new Exception("Bạn vui lòng thêm ít nhất 1 thông số");
            }

            if (data.Id <= 0)
            {
                using var transaction = await _Context.Database.BeginTransactionAsync();
                try
                {
                    string nameSlug = Functions.RemoveVietnameseTone(data.Name);
                    if(_Context.Products.Any(item=> item.NameSlug == nameSlug))
                    {
                        throw new AlreadyExistsException("Tên sản phẩm");
                    }
                    // Thêm mới sản phẩm
                    var newProduct = new Product
                    {
                        Name = data.Name,
                        Price = data.Price,
                        Manufacturer = data.Manufacturer,
                        Capacity = data.Capacity,
                        Size = data.Size,
                        ProductTypeId = data.ProductType,
                        WarrantyPeriod = data.WarrantyPeriod,
                        NameSlug = Functions.RemoveVietnameseTone(data.Name)
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
                    var filepath = Functions.SaveImgToDB(img, directoryPath);
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
                productData.Size = data.Size;
                productData.NameSlug = Functions.RemoveVietnameseTone(data.Name);
                productData.Manufacturer = data.Manufacturer;
                productData.Capacity = data.Capacity;
                productData.ProductTypeId = data.ProductType;
                productData.WarrantyPeriod = data.WarrantyPeriod;
                _Context.Products.Update(productData);

                //Xoá các thông tin sản phẩm cũ và thêm mới lại
                var infoProduct = _Context.InfoProducts.Where(item => item.ProductId == productData.Id).ToList();
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
                var imgProductsOld = _Context.ImgProducts.Where(item => item.ProductId == productData.Id).ToList();
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
                    NameSlug = record.NameSlug,
                    PathImg = Functions.ConverPathIMG(_Context.ImgProducts.Where(img => img.ProductId == record.Id).Select(img => img.Path).FirstOrDefault() ?? ""),
                }).ToList();
            if (result.Count == 0)
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
            if (data.Count == 0)
            {
                throw new Exception("Không có sản phẩm nào thoả mãn điều kiện của bạn");
            }

            return new GetPriceProductResModel
            {
                PriceFrom = data.Min(item => item.Price),
                PriceTo = data.Max(item => item.Price),
            };
        }
        public List<ResultFindProductResModel> GetProductByType(long productType, string nameSlug = "")
        {
            var result = _Context.Products.Where(item => item.ProductTypeId == productType && !item.IsDelete && (string.IsNullOrEmpty(nameSlug) || item.NameSlug != nameSlug)).Select(record => new ResultFindProductResModel
            {
                Id = record.Id,
                Name = record.Name,
                Price = record.Price,
                ProductType = record.ProductTypeId,
                NameSlug = record.NameSlug,
                PathImg = Functions.ConverPathIMG(_Context.ImgProducts.Where(img => img.ProductId == record.Id).Select(img => img.Path).FirstOrDefault() ?? ""),
            }).ToList();
            return result;
        }
        public List<GetAllProductByTypeResModel> GetAllProductByType()
        {
            var result = new List<GetAllProductByTypeResModel>();
            //result = _Context.Products.GroupBy(item => item.ProductTypeId).Select(record => new GetAllProductByTypeResModel { ProductName = record.First().ProductType.Name , Products = new List<ResultFindProductResModel> { } }).ToList();
            foreach (var item in _Context.ProductTypes.ToList()) 
            {
                var newItem = new GetAllProductByTypeResModel();
                newItem.ProductName = item.Name;
                newItem.Products = GetProductByType(item.Id);
                if(newItem.Products.Count != 0)
                {
                    result.Add(newItem);
                }
            }
            return result;
        }
        public List<GetAllProductByIdResModel> GetAllProductByID(List<GetAllProductByIdReqModel> Data)
        {
            var result = new List<GetAllProductByIdResModel>();
            foreach (var item in Data)
            {
                var itemProduct =  _Context.Products.GetAvailableById(item.Id);
                result.Add(new GetAllProductByIdResModel
                {
                    Id = itemProduct.Id,
                    Name = itemProduct.Name,
                    Price = itemProduct.Price,
                    ProductType = itemProduct.ProductTypeId,
                    NameSlug = itemProduct.NameSlug,
                    Quantity = item.Quantity,
                    PathImg = Functions.ConverPathIMG(_Context.ImgProducts.Where(img => img.ProductId == itemProduct.Id).Select(img => img.Path).FirstOrDefault() ?? ""),
                });
            }
            return result;
        }
    }
}
