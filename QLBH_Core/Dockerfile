# Sử dụng hình ảnh ASP.NET chính thức của Microsoft để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Sử dụng hình ảnh SDK của .NET để xây dựng dự án
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Sao chép các file .csproj vào thư mục /src
COPY ["QLBH_Core.API/QLBH_Core.API.csproj", "QLBH_Core.API/"]
COPY ["QLBH_Core.Moddel/QLBH_Core.Moddel.csproj", "QLBH_Core.Moddel/"]
COPY ["QLBH_Core.Commons/QLBH_Core.Commons.csproj", "QLBH_Core.Commons/"]
COPY ["QLBH_Core.Service/QLBH_Core.Service.csproj", "QLBH_Core.Service/"]

# Sao chép toàn bộ mã nguồn vào container
COPY . .

# Khôi phục các package NuGet cho tất cả các dự án
WORKDIR "/src/QLBH_Core.API"
RUN dotnet restore "QLBH_Core.API.csproj"

# Xây dựng và publish dự án API (có các thư viện liên quan)
RUN dotnet publish "QLBH_Core.API.csproj" -c Release -o /app/publish

# Sử dụng image base để chạy ứng dụng
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Đặt entrypoint để chạy API
ENTRYPOINT ["dotnet", "QLBH_Core.API.dll"]
