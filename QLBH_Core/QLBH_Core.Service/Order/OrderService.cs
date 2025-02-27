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
        public async Task Create(CreateOrderReqModel data)
        {
            var newOrder = new Orders
            {
                ProductId = data.ProductId,
                CustomerName = data.CustomerName,
                SDTCustomer = data.SDTCustomer,
                SDTZaloCustomer = data.SDTZaloCustomer,
                FbCustomer = data.FbCustomer,
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
                FbCustomer= record.FbCustomer,
                OrderDate = record.OrderDate,
                ProductId = record.ProductId,
                SDTCustomer= record.SDTCustomer,
                SDTZaloCustomer= record.SDTZaloCustomer,
                ProductName = record.Product.Name,
                ProductIMG = Functions.ConverPathIMG(_Context.ImgProducts.Where(img => img.ProductId == record.ProductId).Select(img => img.Path).FirstOrDefault() ?? "")
            }).ToList();
            return result;
        }
        private void SendMessage(string message,string SDT)
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            using (var driver = new ChromeDriver(options))
            {
                // Mở Zalo Web
                driver.Navigate().GoToUrl("https://chat.zalo.me/");

                // Yêu cầu đăng nhập thủ công nếu cần (lưu session sau đó)
                // Tìm kiếm tài khoản
                var searchBox = driver.FindElement(By.CssSelector("input[placeholder='Tìm bạn bè, tin nhắn...']"));
                searchBox.SendKeys(SDT);
                searchBox.SendKeys(Keys.Enter);

                // Gửi tin nhắn
                var messageBox = driver.FindElement(By.CssSelector("div[contenteditable='true']"));
                messageBox.SendKeys(message);
                messageBox.SendKeys(Keys.Enter);
            }
        }
    }
}
