# 🚀 TaskFlow Management

> **Đồ án Lập trình Quản lý – Giai đoạn 1**  
> Chủ đề: **Quản lý dự án phần mềm tin học**  
> Môn: Lập trình Quản lý | Nhóm N5 – Thứ 2

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com)
[![EF Core](https://img.shields.io/badge/EF_Core-8.0-purple)](https://learn.microsoft.com/en-us/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-Express_2022-CC2927?logo=microsoftsqlserver)](https://www.microsoft.com/sql-server)
[![BCrypt](https://img.shields.io/badge/Password-BCrypt-green)](https://github.com/BcryptNet/bcrypt.net)

---

## 📋 Mục lục

- [Giới thiệu](#-giới-thiệu)
- [Công nghệ & Kiến trúc](#-công-nghệ--kiến-trúc)
- [Diagrams](#-diagrams)
- [Yêu cầu hệ thống](#-yêu-cầu-hệ-thống)
- [Cài đặt & Chạy chương trình](#-cài-đặt--chạy-chương-trình)
- [Tài khoản mặc định](#-tài-khoản-mặc-định)
- [Phân quyền hệ thống](#-phân-quyền-hệ-thống)
- [Cấu trúc project](#-cấu-trúc-project)

---

## 📖 Giới thiệu

**TaskFlow Management** là ứng dụng Windows Forms (.NET 8) quản lý toàn vòng đời dự án phần mềm.  
Hệ thống áp dụng **Clean Architecture** tách biệt 4 tầng rõ ràng với Service Layer làm trung tâm nghiệp vụ.

### ✨ Tính năng Giai đoạn 1

| Module | Mô tả |
|--------|-------|
| 🔐 **Đăng nhập** | BCrypt password, eye icon toggle, nhớ username, phân quyền Role |
| 👥 **Người dùng** | Quản lý tài khoản (Admin), nhân viên (Manager) |
| 🏢 **Khách hàng** | Danh sách, tìm kiếm khách hàng |
| 📁 **Dự án** | Xem danh sách, tạo dự án, quản lý thành viên |
| ✅ **Công việc** | TaskList, Kanban Board, "Công việc của tôi" |
| 📊 **Báo cáo** | Dashboard, tiến độ, ngân sách |

---

## 🛠 Công nghệ & Kiến trúc

| Thành phần | Chi tiết |
|-----------|---------|
| **Runtime** | .NET 8.0 / C# 12 |
| **UI** | Windows Forms (.NET 8) |
| **ORM** | Entity Framework Core 8 |
| **Database** | SQL Server Express 2022 |
| **DI** | Microsoft.Extensions.DependencyInjection |
| **Password** | BCrypt.Net-Next (WorkFactor 12) |
| **Architecture** | Clean Architecture – 4 tầng |

### Luồng dữ liệu

```
Form  →  Service  →  Repository  →  DbContext  →  SQL Server
 UI       Logic        Data           ORM           Database
```

---

## 📐 Diagrams

| Tài liệu | Mô tả |
|---------|-------|
| [Architecture Diagram](docs/architecture.md) | Sơ đồ kiến trúc Clean Architecture 4 tầng + request flow |
| [ERD](docs/erd.md) | Entity Relationship Diagram – 15 bảng, quan hệ, indexes |
| [Use Case Diagram](docs/usecase.md) | Phân quyền Admin / Manager / Developer |

---

## 💻 Yêu cầu hệ thống

| Phần mềm | Phiên bản | Tải về |
|---------|-----------|--------|
| **Windows** | 10 / 11 (64-bit) | — |
| **.NET SDK** | 8.0 | [dotnet.microsoft.com](https://dotnet.microsoft.com/download/dotnet/8.0) |
| **SQL Server Express** | 2022 | [microsoft.com/sql-server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) |
| **Visual Studio** | 2022 (17.x) | [visualstudio.microsoft.com](https://visualstudio.microsoft.com/) |
| **Git** | Latest | [git-scm.com](https://git-scm.com/) |

---

## ▶ Cài đặt & Chạy chương trình

### Bước 1 – Clone project từ GitHub

**Terminal / CMD / PowerShell:**
```bash
git clone https://github.com/DTH235712-Lap-Trinh-Quan-Ly-N5-T2
cd DTH235712-Lap-Trinh-Quan-Ly-N5-T2
```

**Hoặc Visual Studio:**
```
File → Clone Repository
URL: https://github.com/DTH235712-Lap-Trinh-Quan-Ly-N5-T2
→ Clone
```

---

### Bước 2 – Cài SQL Server Express 2022

1. Tải **SQL Server 2022 Express** tại [microsoft.com/sql-server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
2. Cài đặt → chọn **Express** (miễn phí)
3. Kiểm tra service đang chạy:
   - Mở **SQL Server Configuration Manager**
   - Vào **SQL Server Services**
   - Đảm bảo `SQL Server (SQLEXPRESS2022)` → **Running** ✅

> 💡 Nếu không thấy **SQL Server Configuration Manager**: tìm trong Start Menu hoặc chạy `SQLServerManager16.msc`

---

### Bước 3 – Kiểm tra Connection String

Mở file `TaskFlowManagement.WinForms/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\SQLEXPRESS2022;Database=TaskFlowManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

> ⚠️ **Quan trọng:** Nếu tên SQL instance khác `SQLEXPRESS2022`, sửa lại cho đúng.  
> Kiểm tra tên instance: **SQL Server Configuration Manager → SQL Server Services** → xem tên trong ngoặc đơn.

---

### Bước 4 – Mở Solution trong Visual Studio

```
File → Open → Project/Solution → chọn TaskFlowManagement.sln
```

---

### Bước 5 – Restore NuGet Packages

**Cách A – Visual Studio (tự động):**
```
Build → Restore NuGet Packages
```

**Cách B – Package Manager Console:**
```
Tools → NuGet Package Manager → Package Manager Console
```
```powershell
dotnet restore
```

**Cách C – Terminal:**
```bash
dotnet restore
```

> Các package sẽ được tải tự động: EF Core, BCrypt.Net-Next, Microsoft.Extensions...

---

### Bước 6 – Build project

**Visual Studio:** `Ctrl + Shift + B`

**Terminal:**
```bash
dotnet build
```

> Nếu có lỗi **BCrypt not found** → chạy lại `dotnet restore` rồi build lại.

---

### Bước 7 – Chạy chương trình

**Visual Studio:**
```
Startup Project: TaskFlowManagement.WinForms
Nhấn F5 (Debug) hoặc Ctrl+F5 (chạy không debug)
```

**Terminal:**
```bash
cd TaskFlowManagement.WinForms
dotnet run
```

---

### Bước 8 – Lần đầu chạy (tự động)

Chương trình sẽ **tự động**:
1. ✅ Tạo database `TaskFlowManagementDb` trên SQL Server
2. ✅ Chạy migration tạo 15 bảng
3. ✅ Seed 15 users + 5 projects + 50 tasks (mật khẩu hash BCrypt)

> 🕒 Lần đầu chạy có thể mất **30-60 giây** do BCrypt hash 15 mật khẩu.  
> Từ lần 2 trở đi: khởi động bình thường vì DB đã có data.

---

### ⚠️ Xử lý lỗi thường gặp

#### Lỗi "Sai mật khẩu" khi đăng nhập
Nguyên nhân: DB cũ đang chứa hash SHA-256, app mới dùng BCrypt → không khớp.

**Cách fix – chọn 1 trong 3:**

**[A] Package Manager Console** *(khuyến nghị)*:
```
Tools → NuGet Package Manager → Package Manager Console
Default project: TaskFlowManagement.Infrastructure
```
```powershell
Drop-Database
```
```
Xác nhận: Y → Enter
```

**[B] Terminal:**
```bash
cd TaskFlowManagement.Infrastructure
dotnet ef database drop --force
```

**[C] SQL Server Management Studio (SSMS):**
```
Kết nối SQL Server → Databases
→ Chuột phải TaskFlowManagementDb → Delete → OK
```

Sau khi drop → **chạy lại app** → DB tự tạo mới với BCrypt hash ✅

---

#### Lỗi kết nối database khi khởi động
```
Kiểm tra:
  1. SQL Server Express đang chạy (xem Bước 2)
  2. Tên instance đúng trong appsettings.json (xem Bước 3)  
  3. Windows Firewall không chặn SQL Server
     (Control Panel → Windows Defender Firewall → Allow an app)
```

#### Lỗi "dotnet ef not found"
```bash
dotnet tool install --global dotnet-ef
```

---

---

## 🔑 Tài khoản mặc định

| Username | Password | Role | Quyền |
|----------|----------|------|-------|
| `admin` | `Admin@123` | **Admin** | Toàn quyền hệ thống |
| `manager1` | `Manager@123` | **Manager** | Projects, Tasks, Reports |
| `manager2` | `Manager@123` | **Manager** | Projects, Tasks, Reports |
| `manager3` | `Manager@123` | **Manager** | Projects, Tasks, Reports |
| `dev1` – `dev11` | `Dev@123` | **Developer** | Chỉ "Công việc của tôi" |

> 🔒 Mật khẩu được hash bằng **BCrypt** (WorkFactor 12) – không thể đọc ngược.

---

## 🛡 Phân quyền hệ thống

```
Admin     → Toàn quyền: Users, Customers, Projects, Tasks, Reports
Manager   → Projects, Tasks (all), Customers, Reports
Developer → Chỉ "Công việc của tôi" (MyTasks)
```

Xem chi tiết: [docs/usecase.md](docs/usecase.md)

---

## 📁 Cấu trúc project

```
TaskFlowManagement/
│
├── 📦 TaskFlowManagement.Application/     ← CORE LAYER (Domain)
│   ├── Entities/          (15 entity classes)
│   ├── DTOs/              (LoginResult, UserSessionDto)
│   ├── Interfaces/
│   │   ├── IRepository.cs, IUserRepository.cs, ...
│   │   └── Services/      (IAuthService, IUserService, ...)
│   └── Services/          (AuthService, UserService, ...)
│
├── 🗄 TaskFlowManagement.Infrastructure/  ← DATA LAYER
│   ├── Data/              (AppDbContext, DbSeeder)
│   ├── Repositories/      (4 repositories)
│   └── Migrations/
│
├── 🖥 TaskFlowManagement.WinForms/        ← PRESENTATION LAYER
│   ├── Forms/             (frmLogin, frmMain)
│   ├── Common/            (AppSession)
│   └── Program.cs         (DI setup + entry point)
│
└── 📐 docs/
    ├── architecture.md    (Sơ đồ kiến trúc)
    ├── erd.md             (Entity Relationship Diagram)
    └── usecase.md         (Use Case + phân quyền)
```

---

## 📄 License

MIT License – xem file [LICENSE](LICENSE).

---

<div align="center">

**TaskFlow Management v1.0** · .NET 8 · WinForms · EF Core · SQL Server · BCrypt · Clean Architecture

</div>
