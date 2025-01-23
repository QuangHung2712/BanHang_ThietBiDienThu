using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using QLBH_Core.API.Extensions;
using QLBH_Core.Commons;
using QLBH_Core.Moddel;
using QLBH_Core.Service;
using QLNhaTro.API.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(); //
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);//
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString(Constants.AppSettingKeys.DEFAULT_CONNECTION)
    );
}); //Kết nối DataBase

builder.Services.ServiceRegister();
builder.Services.AddTransient<JwtMiddleWare>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "BoardingHouseManagement API", Version = "v1" });
    option.CustomSchemaIds(type => type.ToString());
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});
var app = builder.Build();

app.UseDbMigration(app.Environment.IsDevelopment());

app.UseMiddleware<JwtMiddleWare>();//  tự động update database vào db
app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
    .WithOrigins("http://localhost:8080/")
    .AllowAnyMethod() //
    .AllowAnyHeader() //
    .SetIsOriginAllowed(origin => true) //
    .AllowCredentials() //
    ); //


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
