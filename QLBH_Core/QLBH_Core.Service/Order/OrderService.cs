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
            return _Context.Orders.Where(item => item.Id == Id).Include(item=> item.Product).Select(record => new GetDetailOrderResModel
            {
                CustomerName = record.CustomerName,
                ProductName = record.Product.Name,
                Price = record.Product.Price,
                ProductImg = Functions.ConverPathIMG(_Context.ImgProducts.Where(img => img.ProductId == record.ProductId).Select(img => img.Path).FirstOrDefault() ?? ""),
                Address = record.Address,
                Email = record.Email,   
                Gender = record.Gender == 1 ? "Nam" : "Nữ",
                Note = record.Note,
                SDTCustomer = record.SDTCustomer,
                OrderDate = record.OrderDate.ToString("dd/MM/yyyy")
            }).FirstOrDefault() ?? throw new NotFoundException("Đơn hàng");
        }
        public async Task Create(CreateOrderReqModel data)
        {
            var newOrder = new Orders
            {
                ProductId = data.ProductId,
                CustomerName = data.CustomerName,
                SDTCustomer = data.SDTCustomer,
                Address = data.Address,
                Gender= data.Gender,
                Note = data.Note,
                OrderDate = DateTime.Now,
            };
            _Context.Orders.Add(newOrder);
            await _Context.SaveChangesAsync();
        }
        public List<GetAllOrderResModel> GetAll()
        {
            var result = _Context.Orders.Include(item=> item.Product).Select(record=> new GetAllOrderResModel
            {
                Id = record.Id,
                CustomerName= record.CustomerName,
                Address = record.Address,
                OrderDate = record.OrderDate,
                ProductId = record.ProductId,
                SDTCustomer= record.SDTCustomer,
                ProductName = record.Product.Name,
                Gender = record.Gender,
                Note = record.Note,
                ProductIMG = Functions.ConverPathIMG(_Context.ImgProducts.Where(img => img.ProductId == record.ProductId).Select(img => img.Path).FirstOrDefault() ?? "")
            }).OrderByDescending(item=> item.OrderDate).ToList();
            return result;
        }
    }
}
