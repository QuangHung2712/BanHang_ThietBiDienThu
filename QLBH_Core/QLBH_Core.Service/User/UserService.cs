using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using QLBH_Core.Commons;
using QLBH_Core.Moddel;
using QLBH_Core.Moddel.Entity;
using QLBH_Core.Moddel.Model.RequestModels;
using QLBH_Core.Moddel.Model.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QLBH_Core.Commons.Constants;

namespace QLBH_Core.Service.Users
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _Context;

        public UserService(AppDbContext context)
        {
            _Context = context;
        }
        public GetInfoUserResModel GetInfo(long UserId)
        {
            var Info = _Context.Users.GetAvailableById(UserId);
            return new GetInfoUserResModel
            {
                Id = Info.Id,
                Name = Info.Name,
                PathAvatar = Functions.ConverPathIMG(Info.PathAvatar),
            };
        }
        public GetDetailUserResModel GetDetail(long UserId)
        {
            var Info = _Context.Users.GetAvailableById(UserId);
            return new GetDetailUserResModel
            {
                Id = Info.Id,
                Name = Info.Name,
                PathAvatar = Functions.ConverPathIMG(Info.PathAvatar),
                Email = Info.Email,
            };
        }
        public async Task Edit(CreateEditUserReqModel input, IFormFile avatar)
        {
            var user = _Context.Users.GetAvailableById(input.Id);
            
            user.Name = input.Name;
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), $@"{Constants.DefaultValue.DEFAULT_BASE_Directory_IMG}\images\Avatar\{user.Id}");
            user.PathAvatar = Functions.SaveImgToDB(avatar,user.Id.ToString(), directoryPath, user.PathAvatar);
            user.Email = input.Email;
            _Context.Users.Update(user);
            await _Context.SaveChangesAsync();
        }
        public async Task Create(CreateEditUserReqModel input)
        {
            _Context.Users.IsAlreadyExists("Email",input.Email);
            var userNew = new User
            {
                Name = input.Name,
                PathAvatar = DefaultValue.DEFAULT_IMG_AVATAR,
                Email = input.Email,
                Position = input.Position,
                Password = DefaultValue.DEFAULT_PASSWORD,
            };
            _Context.Users.Add(userNew);
            await _Context.SaveChangesAsync();
        }
        public async Task Delete(long Id)
        {
            var user = _Context.Users.GetAvailableById(Id);
            if (!string.IsNullOrEmpty(user.PathAvatar) && user.PathAvatar != DefaultValue.DEFAULT_IMG_AVATAR)
            {
                File.Delete(user.PathAvatar);
            }
            _Context.Users.Delete(Id);
            await _Context.SaveChangesAsync();
        }
    }
}
