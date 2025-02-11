using QLBH_Core.Commons;
using QLBH_Core.Commons.CustomException;
using QLBH_Core.Moddel;
using QLBH_Core.Moddel.Entity;
using QLBH_Core.Moddel.Model;
using QLBH_Core.Moddel.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH_Core.Service.ProductTypeS
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly AppDbContext _Context;

        public ProductTypeService(AppDbContext context)
        {
            _Context = context;
        }
        public List<BaseModel> GetAll()
        {
            return _Context.ProductTypes.Select(record => new BaseModel 
                    {
                        Id = record.Id,
                        Name = record.Name,
                    }).ToList();
        }
        public async Task CreateEdit(CreateEditProductTypeReqModel data)
        {
            _Context.ProductTypes.IsAlreadyExists("Name", data.Name,"Loại sản phẩm");
            if(data.Id <= 0)
            {
                var newProductType = new ProductType
                {
                    Name = data.Name,
                };
                _Context.ProductTypes.Add(newProductType);
                await _Context.SaveChangesAsync();
            }
            else
            {
                var productTypeData = _Context.ProductTypes.GetById(data.Id);
                productTypeData.Name = data.Name;
                _Context.ProductTypes.Update(productTypeData);
                await _Context.SaveChangesAsync();
            }
        }
        public async Task Delete(long Id)
        {
            if(_Context.Products.Any(p => p.ProductTypeId == Id))
            {
                throw new InUseException("Loại sản phẩm này");
            }
            _Context.ProductTypes.DeletePermanent(Id);
            await _Context.SaveChangesAsync();
        }
        public List<BaseModel> GetProductypeByName(string? name)
        {
            return _Context.Products.Where(item=> string.IsNullOrEmpty(name) || item.Name.ToLower().Contains(name.ToLower()))
                .GroupBy(item=>item.ProductTypeId)
                .Select(record => new BaseModel
                {
                    Id = record.First().ProductTypeId,
                    Name = record.First().ProductType.Name,
                }).ToList();
        }
    }
}
