﻿
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QLBH_Core.API;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static QLBH_Core.Commons.Enums;
using QLBH_Core.Commons;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using QLBH_Core.Commons.Output;

namespace QLNhaTro.API.MiddleWare
{
    public class JwtMiddleWare : IMiddleware
    {
        private readonly AppSettings _appSettings;
        public JwtMiddleWare(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()!;    
            if(string.IsNullOrEmpty(token))
            {
                token = Convert.ToString(context.Request.Query["access_token"]);
            }    
            if(token != null)
            {
                AttachUserToContext(context, token);
            }
            await next(context);
        }
        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.UTF8.GetBytes(_appSettings.Jwt.SecretKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                }, out SecurityToken validatedToken);

                JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
                var permission = jwtToken.Claims.Where(c=>c.Type==ClaimTypes.Role).FirstOrDefault();
                if (permission != null)
                {
                    var grantedPermissions = permission.Value
                        .Split(",")
                        .Select(item => (FeatureCode)Enum.Parse(typeof(FeatureCode), item))
                        .ToList();
                    context.Items[Constants.JWT.Permission] = grantedPermissions;
                }
            }
            catch(Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                var errorResponse = new ErrorResponse
                {
                    Success = false,
                    Message = "Thất bại trong quá trình xử lý",
                };

                context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
