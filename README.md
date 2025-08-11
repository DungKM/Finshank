# ASP.NET Core Web API Starter

Đây là một dự án mẫu ASP.NET Core Web API sử dụng Swagger và Entity Framework Core.

## 🛠️ Yêu cầu hệ thống

- [.NET SDK 6.0+](https://dotnet.microsoft.com/en-us/download)
- SQL Server (hoặc LocalDB)
- Git (tùy chọn)

---

## 🚀 Khởi tạo dự án

Tạo một project Web API mới trong thư mục `api`:

## 📦 Các lệnh đã chạy để tạo project

```bash
# 1. Tạo project Web API trong thư mục 'api'
dotnet new webapi -o api

# 2. Di chuyển vào thư mục project
cd api

# 3. Cài đặt thư viện Swagger
dotnet add package Swashbuckle.AspNetCore

# 4. Chạy ứng dụng với hot reload
dotnet watch run

# 5. Cài đặt công cụ dotnet-ef (chỉ cần chạy một lần trên máy)
dotnet tool install --global dotnet-ef

# 6. Tạo migration đầu tiên (Init)
dotnet ef migrations add Init

# 7. Cập nhật cơ sở dữ liệu từ migration
dotnet ef database update

dotnet ef migrations add Identity

dotnet ef database update