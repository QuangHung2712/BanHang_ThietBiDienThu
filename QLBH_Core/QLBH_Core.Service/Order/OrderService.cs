using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using QLBH_Core.Moddel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBH_Core.Moddel.Model.RequestModels;
using QLBH_Core.Moddel.Entity;
using QLBH_Core.Moddel.Model.ResponseModels;
using Microsoft.EntityFrameworkCore;
using QLBH_Core.Commons;

namespace QLBH_Core.Service.Order
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _Context;

        public OrderService(AppDbContext context)
        {
            _Context = context;
        }
        public GetDetailOrderResModel GetDetail( long Id)
        {
            return _Context.Orders.Where(item => item.Id == Id).Select(record => new GetDetailOrderResModel
            {
                CustomerName = record.CustomerName,
                Products = _Context.OrderProduct.Where(OP=> OP.OrderId == record.Id).Include(op=> op.Product).Select(recordop=> new OrderProductResModel
                {
                    Id = recordop.ProductId,
                    Price = recordop.Product.Price,
                    ProductName = recordop.Product.Name,
                    ProductNumber = recordop.ProductQuantity,
                    ProductIMG = Functions.ConverPathIMG(_Context.ImgProducts.Where(img => img.ProductId == recordop.ProductId).Select(img => img.Path).FirstOrDefault() ?? ""),
                }).ToList(),
                Address = record.Address,
                Gender = record.Gender == 1 ? "Nam" : "Nữ",
                Note = record.Note,
                SDTCustomer = record.SDTCustomer,
                OrderDate = record.OrderDate.ToString("dd/MM/yyyy")
            }).FirstOrDefault() ?? throw new NotFoundException("Đơn hàng");
        }
        public async Task Create(CreateOrderReqModel data)
        {
            foreach(var item in data.Products)
            {
                _Context.Products.IsGetById(item.Id);
            }
            using var transaction = await _Context.Database.BeginTransactionAsync();
            try
            {
                var newOrder = new Orders
                {
                    CustomerName = data.CustomerName,
                    SDTCustomer = data.SDTCustomer,
                    Address = data.Address,
                    Gender = data.Gender,
                    Note = data.Note,
                    OrderDate = DateTime.Now,
                };
                _Context.Orders.Add(newOrder);
                await _Context.SaveChangesAsync();


                var orderProduct = data.Products.Select(item => new OrderProduct
                {
                    OrderId = newOrder.Id,
                    ProductId = item.Id,
                    ProductQuantity = item.Quantity,
                }).ToList();
                _Context.OrderProduct.AddRange(orderProduct);
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
        public List<GetAllOrderResModel> GetAll()
        {
            var result = _Context.Orders.Select(record=> new GetAllOrderResModel
            {
                Id = record.Id,
                CustomerName= record.CustomerName,
                Address = record.Address,
                OrderDate = record.OrderDate,
                Products = _Context.OrderProduct.Where(item=> item.OrderId == record.Id).Include(item=> item.Product).Select(recordOp => new OrderProductResModel
                {
                    Id = recordOp.Id,
                    Price = recordOp.Product.Price,
                    ProductName = recordOp.Product.Name,
                    ProductIMG = Functions.ConverPathIMG(_Context.ImgProducts.Where(img => img.ProductId == recordOp.ProductId).Select(img => img.Path).FirstOrDefault() ?? ""),
                    ProductNumber = recordOp.ProductQuantity
                }).ToList(),
                SDTCustomer= record.SDTCustomer,
                Gender = record.Gender,
                Note = record.Note,
            }).OrderByDescending(item=> item.OrderDate).ToList();
            return result;
        }
    }
}
