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
            SendMessage("Test thử gửi tin nhắn zalo", data.SDTZaloCustomer);
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
