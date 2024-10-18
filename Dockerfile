
# Use the official ASP.NET Core 8 SDK as a build environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Bước 1: Sử dụng image chính thức của .NET SDK để xây dựng ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Bước 2: Sao chép các file csproj và phục hồi các phụ thuộc
COPY ["WebUI/WebUI.csproj", "WebUI/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore "WebUI/WebUI.csproj"

# Bước 3: Sao chép tất cả các file còn lại và xây dựng dự án
COPY . .
WORKDIR "/src/WebUI"
RUN dotnet build "WebUI.csproj" -c Release -o /app/build

# Bước 4: Xuất bản ứng dụng
FROM build AS publish
RUN dotnet publish "WebUI.csproj" -c Release -o /app/publish

# Bước 5: Sử dụng image ASP.NET Core để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebUI.dll"]
