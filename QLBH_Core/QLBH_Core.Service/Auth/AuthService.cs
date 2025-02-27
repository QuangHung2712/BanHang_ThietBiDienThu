﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QLBH_Core.Commons;
using QLBH_Core.Moddel;
using QLBH_Core.Moddel.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static QLBH_Core.Commons.Enums;

namespace QLBH_Core.Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _Context;
        private readonly AppSettings _appSettings;

        public AuthService(AppDbContext appDbContext, IOptions<AppSettings> appSettings)
        {
            _Context = appDbContext;
            _appSettings = appSettings.Value;
        }
        public string Login(LoginReqModel input)
        {
            if(input.Email != "admin" || input.Password != "1")
            {
                throw new Exception("Tài khoản hoặc mật khẩu không đúng");
            }
            return GenerateToken(new List<FeatureCode> { FeatureCode.Admin });
        }
        private string GenerateToken(List<FeatureCode> permissions)
        {
            try
            {
                List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.Role, string.Join(",", permissions)) };
                SymmetricSecurityKey secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Jwt.SecretKey));
                SigningCredentials credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
                JwtSecurityToken token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public async Task ChangePassword(ChangePasswordReqModel data)
        {
            var infoUser = _Context.Users.GetAvailableById(data.Id);
            if(infoUser.Password != data.PasswordOld)
            {
                throw new Exception("Mật khẩu cũ không đúng");
            }
            if(infoUser.Password == data.PasswordNew)
            {
                throw new Exception("Mật khẩu mới không được trùng với mật khẩu cũ");
            }
            infoUser.Password = data.PasswordNew;
            _Context.Users.Update(infoUser);
            await _Context.SaveChangesAsync();
        }
    }
}
