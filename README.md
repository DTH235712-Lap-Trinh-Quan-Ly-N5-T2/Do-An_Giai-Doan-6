# 📋 TaskFlow Management – Hệ thống Quản lý Dự án Phần mềm

---

## 📝 Thông tin đồ án

| | |
|---|---|
| **Môn học** | Lập Trình Quản Lý |
| **Giảng viên hướng dẫn** | Huỳnh Lý Thanh Nhàn |
| **Nhóm** | Nhóm 5 – Tổ thực hành 2 |
| **Sinh viên thực hiện** | Trần Trí Nhân |
| **MSSV** | DTH235712 |
| **Lớp** | DH24TH2 |
| **Năm học** | 2025 – 2026 |
| **Tiến độ** | Giai đoạn 1 ✅  Giai đoạn 2 ✅  Giai đoạn 3 ✅ Giai đoạn 4 ✅  (4 / 10 hoàn thành) |

---

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com)
[![EF Core](https://img.shields.io/badge/EF_Core-8.0-purple)](https://learn.microsoft.com/en-us/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-Express_2022-CC2927?logo=microsoftsqlserver)](https://www.microsoft.com/sql-server)
[![BCrypt](https://img.shields.io/badge/Password-BCrypt_WorkFactor_12-green)](https://github.com/BcryptNet/bcrypt.net)
[![Architecture](https://img.shields.io/badge/Architecture-Clean_Architecture-blue)]()

---

## 📌 Mục lục

1. [Tổng quan hệ thống](#1-tổng-quan-hệ-thống)
2. [Đặc tả chức năng](#2-đặc-tả-chức-năng)
3. [Kiến trúc hệ thống](#3-kiến-trúc-hệ-thống)
4. [Cơ sở dữ liệu](#4-cơ-sở-dữ-liệu)
5. [Phân quyền người dùng](#5-phân-quyền-người-dùng)
6. [Bảo mật](#6-bảo-mật)
7. [Quy trình phát triển 10 giai đoạn](#7-quy-trình-phát-triển-10-giai-đoạn)
8. [Cài đặt & Chạy chương trình](#8-cài-đặt--chạy-chương-trình)
9. [Tài khoản mặc định](#9-tài-khoản-mặc-định)
10. [Cấu trúc thư mục](#10-cấu-trúc-thư-mục)
11. [Công nghệ sử dụng](#11-công-nghệ-sử-dụng)

---

## 1. Tổng quan hệ thống

**TaskFlow Management** là ứng dụng desktop (Windows Forms / .NET 8) phục vụ quản lý toàn vòng đời dự án phần mềm trong một công ty tin học vừa và nhỏ.

Hệ thống hỗ trợ **3 vai trò**: Admin, Manager và Developer — mỗi vai trò có giao diện và quyền hạn riêng biệt. Toàn bộ dữ liệu được lưu trên SQL Server Express, truy cập qua Entity Framework Core với kiến trúc Clean Architecture 4 tầng.

### Vấn đề thực tế hệ thống giải quyết

| Vấn đề | Giải pháp |
|--------|-----------|
| Khó theo dõi tiến độ nhiều dự án cùng lúc | Dashboard tổng quan + Kanban board theo dự án |
| Phân công công việc không rõ ràng | Task giao cho từng developer, có deadline + priority |
| Không kiểm soát được ngân sách | Module Expense theo dõi chi phí thực tế vs ngân sách |
| Không có phân quyền rõ ràng | 3 Role với menu tự ẩn/hiện theo quyền |
| Mật khẩu lưu dạng plain text hoặc hash yếu | BCrypt WorkFactor 12 với salt riêng cho mỗi user |

---

## 2. Đặc tả chức năng

### 2.1 Module Xác thực (Authentication)

**UC-01: Đăng nhập**
- Nhập username và password → hệ thống xác thực bằng BCrypt
- Hiển thị lỗi rõ ràng khi sai thông tin (không tiết lộ username có tồn tại không)
- Nút 👁 toggle hiện/ẩn mật khẩu
- Checkbox "Nhớ tên đăng nhập" – lưu vào `user_prefs.json`
- Sau khi đăng nhập: load Role → ẩn/hiện menu theo quyền → vào `frmMain`

**UC-02: Đăng xuất**
- Xác nhận trước khi logout
- Xóa AppSession → hiện lại form Login
- Nếu đăng nhập lại: refresh giao diện theo user/role mới

---

### 2.2 Module Quản lý Người dùng (Users) — Admin

**UC-03: Xem danh sách tài khoản**
- Hiển thị toàn bộ user, lọc theo Role và trạng thái (Active/Inactive)

**UC-04: Tạo tài khoản mới**
- Nhập Username, Họ tên, Email, Mật khẩu, Role
- Validate: username duy nhất, email hợp lệ, mật khẩu tối thiểu 6 ký tự
- Hash BCrypt trước khi lưu DB

**UC-05: Cập nhật thông tin tài khoản**
- Sửa họ tên, email, số điện thoại
- Không cho sửa username (khóa chính nghiệp vụ)

**UC-06: Vô hiệu hóa tài khoản**
- Soft delete: set `IsActive = false`, không xóa dữ liệu thật
- Tài khoản bị vô hiệu hóa không thể đăng nhập

**UC-07: Đổi mật khẩu** *(Giai đoạn 2)*
- Verify mật khẩu cũ → hash BCrypt mật khẩu mới

---

### 2.3 Module Quản lý Khách hàng (Customers) — Manager/Admin

**UC-08: Danh sách khách hàng**
- Xem toàn bộ, tìm kiếm theo tên công ty / liên hệ / email

**UC-09: Thêm / Sửa / Xóa khách hàng**
- Thông tin: Tên công ty, Người liên hệ, Email, Điện thoại, Địa chỉ

**UC-10: Chi tiết khách hàng**
- Xem danh sách dự án đã/đang thực hiện cho khách hàng đó

---

### 2.4 Module Quản lý Dự án (Projects) — Manager/Admin

**UC-11: Danh sách dự án**
- Manager/Admin: thấy tất cả dự án
- Developer: chỉ thấy dự án mình là thành viên

**UC-12: Tạo dự án mới**
- Thông tin: Tên, Mô tả, Khách hàng, Ngày bắt đầu, Deadline, Ngân sách, Độ ưu tiên

**UC-13: Cập nhật / Đổi trạng thái dự án**
- Trạng thái: `NotStarted → InProgress → OnHold → Completed / Cancelled`

**UC-14: Quản lý thành viên dự án**
- Thêm Developer vào dự án, gán vai trò trong dự án
- Ghi nhận ngày tham gia, ngày rời nhóm

**UC-15: Chi tiết dự án**
- Xem toàn bộ Members, Tasks, Expenses, tiến độ tổng

---

### 2.5 Module Quản lý Công việc (Tasks) — Manager + Developer

**UC-16: Danh sách Task (Task List)**
- Phân trang, lọc theo: Dự án / Người phụ trách / Status / Priority / Từ khóa
- Chỉ Manager trở lên thấy task của người khác

**UC-17: Kanban Board**
- Cột: Todo → InProgress → InReview → Done
- Kéo-thả task giữa các cột *(Giai đoạn 5)*

**UC-18: Công việc của tôi (My Tasks)**
- Developer xem riêng task được giao cho mình
- Sắp xếp theo deadline gần nhất

**UC-19: Tạo / Sửa Task**
- Thông tin: Tiêu đề, Mô tả, Dự án, Người phụ trách, Priority, Status, Category, Deadline
- Hỗ trợ sub-task (task lồng nhau)

**UC-20: Cập nhật tiến độ**
- Kéo thanh % từ 0-100
- Tự động đánh dấu hoàn thành khi = 100%

**UC-21: Bình luận & Đính kèm** *(Giai đoạn 7)*

---

### 2.6 Module Báo cáo (Reports) — Manager/Admin

**UC-22: Dashboard tổng quan**
- Widget đỏ: Task quá hạn
- Widget vàng: Task sắp đến hạn (7 ngày tới)
- Pie chart: Task theo Status mỗi dự án
- Danh sách dự án đang hoạt động

**UC-23: Báo cáo ngân sách**
- So sánh Budget vs TotalExpense theo từng dự án

---

## 3. Kiến trúc hệ thống

Dự án áp dụng **Clean Architecture** – tách biệt 4 tầng, phụ thuộc chỉ đi từ ngoài vào trong:

```
┌─────────────────────────────────────────────────────────────────┐
│                   PRESENTATION LAYER                             │
│              TaskFlowManagement.WinForms                         │
│   frmLogin  │  frmMain  │  (G2: frmUsers, frmProjects, ...)     │
│   AppSession (Static session)  │  Program.cs (DI Container)     │
└──────────────────────┬──────────────────────────────────────────┘
                       │ gọi Interface (không gọi trực tiếp)
┌──────────────────────▼──────────────────────────────────────────┐
│                  APPLICATION LAYER (Core)                        │
│            TaskFlowManagement.Application                        │
│                                                                  │
│  Services/Auth/     Services/Users/    Services/Projects/        │
│  Services/Tasks/                                                 │
│  ├── AuthService       UserService     ProjectService            │
│  └── UserSessionMapper                TaskService               │
│                                                                  │
│  Interfaces/Services/   Interfaces/        DTOs/    Helpers/     │
│  IAuthService           IRepository<T>     LoginResult  PasswordHelper │
│  IUserService           IUserRepository    UserSessionDto ValidationHelper │
│  IProjectService        IProjectRepository                       │
│  ITaskService           ITaskRepository                          │
│                         ICustomerRepository                      │
│                                                                  │
│  Entities/ (15 entity classes)                                   │
└──────────────────────┬──────────────────────────────────────────┘
                       │ implements Interface
┌──────────────────────▼──────────────────────────────────────────┐
│                 INFRASTRUCTURE LAYER                             │
│           TaskFlowManagement.Infrastructure                      │
│                                                                  │
│  Repositories/                    Data/                          │
│  UserRepository                   AppDbContext (EF Core)         │
│  ProjectRepository                DbSeeder (auto seed)           │
│  TaskRepository                   Seed/LookupSeeder              │
│  CustomerRepository                    /UserSeeder               │
│                                        /ProjectSeeder            │
│                                        /SeedHelper               │
│  Migrations/ (auto-generated)                                    │
└──────────────────────┬──────────────────────────────────────────┘
                       │
┌──────────────────────▼──────────────────────────────────────────┐
│                    DATABASE LAYER                                │
│              SQL Server Express 2022                             │
│              TaskFlowManagementDb                                │
└─────────────────────────────────────────────────────────────────┘
```

### Luồng xử lý đăng nhập (ví dụ minh họa)

```
btnLogin_Click()
  → IAuthService.LoginAsync(username, password)
      → IUserRepository.GetByUsernameAsync()   [SELECT WHERE Username=? AND IsActive=1]
      → PasswordHelper.Verify(plain, hash)      [BCrypt.Verify()]
      → IUserRepository.GetWithRolesAsync()     [SELECT + JOIN UserRoles + Roles]
      → UserSessionMapper.ToDto()               [Entity → DTO (bỏ PasswordHash)]
      → IUserRepository.UpdateLastLoginAsync()  [fire-and-forget]
      → return LoginResult.Ok(dto)
  → AppSession.Login(dto)                       [lưu vào static session]
  → this.DialogResult = OK → frmMain mở
```

---

## 4. Cơ sở dữ liệu

**15 bảng** được tổ chức thành 4 nhóm:

### Nhóm Core
| Bảng | Mô tả | Cột quan trọng |
|------|-------|----------------|
| `Users` | Tài khoản người dùng | Username (unique), PasswordHash (BCrypt), IsActive |
| `Projects` | Dự án | OwnerId, CustomerId, Status, Budget, StartDate, PlannedEndDate |
| `TaskItems` | Công việc | ProjectId, AssignedToId, StatusId, PriorityId, ProgressPercent |
| `Customers` | Khách hàng | CompanyName, ContactName, Email |

### Nhóm Lookup (dữ liệu danh mục)
| Bảng | Giá trị |
|------|---------|
| `Roles` | Admin, Manager, Developer |
| `Priorities` | Low (1), Medium (2), High (3), Critical (4) |
| `Statuses` | CREATED, ASSIGNED, IN-PROGRESS, FAILED, REVIEW-1, REVIEW-2, APPROVED, IN-TEST, RESOLVED, CLOSED |
| `Categories` | Bug, Feature, Improvement, Research, Testing |
| `Tags` | Urgent, UI, Backend, Database, API, Security, Performance |

### Nhóm Junction (quan hệ nhiều-nhiều)
| Bảng | Liên kết |
|------|---------|
| `UserRoles` | User ↔ Role |
| `ProjectMembers` | Project ↔ User (kèm ProjectRole, JoinedAt, LeftAt) |
| `TaskTags` | TaskItem ↔ Tag |

### Nhóm Chi tiết
| Bảng | Mô tả |
|------|-------|
| `Comments` | Bình luận trên Task |
| `Attachments` | File đính kèm trên Task |
| `Expenses` | Chi phí phát sinh trong dự án |

### Indexes tối ưu hiệu năng (12 indexes)
```
IX_TaskItems_ProjectId             – filter task theo dự án
IX_TaskItems_StatusId              – Kanban board
IX_TaskItems_PriorityId            – filter theo mức độ
IX_TaskItems_AssignedToId          – "Công việc của tôi"
IX_TaskItems_DueDate               – Overdue / DueSoon query
IX_TaskItems_CreatedAt             – sắp xếp mới nhất
IX_TaskItems_ProjectId_StatusId    – composite index (Kanban filter)
IX_Projects_OwnerId                – dự án của Manager
IX_Projects_Status                 – dự án đang active
IX_Projects_CustomerId             – dự án theo khách hàng
IX_Comments_TaskItemId             – load bình luận nhanh
IX_Expenses_ProjectId              – tính tổng chi phí
```

---

## 5. Phân quyền người dùng

| Chức năng | Developer | Manager | Admin |
|-----------|:---------:|:-------:|:-----:|
| Đăng nhập / Đăng xuất | ✅ | ✅ | ✅ |
| Xem "Công việc của tôi" | ✅ | ✅ | ✅ |
| Cập nhật tiến độ task | ✅ | ✅ | ✅ |
| Đổi mật khẩu bản thân | ✅ | ✅ | ✅ |
| Xem danh sách Task (tất cả) | ❌ | ✅ | ✅ |
| Kanban Board dự án | ❌ | ✅ | ✅ |
| Tạo / Sửa / Xóa Task | ❌ | ✅ | ✅ |
| Quản lý Dự án | ❌ | ✅ | ✅ |
| Quản lý Khách hàng | ❌ | ✅ | ✅ |
| Xem Báo cáo / Dashboard | ❌ | ✅ | ✅ |
| Quản lý tài khoản Users | ❌ | ❌ | ✅ |
| Vô hiệu hóa tài khoản | ❌ | ❌ | ✅ |

> Giao diện tự động ẩn/hiện menu theo Role ngay khi đăng nhập — Developer không thấy menu không được phép.

---

## 6. Bảo mật

### BCrypt Password Hashing

| Tiêu chí | SHA-256 (cũ) | BCrypt WorkFactor 12 (hiện tại) |
|---------|-------------|--------------------------------|
| Tốc độ hash | ~1ms | ~300ms (cố tình chậm) |
| Salt | Tĩnh, dùng chung | Tự động, riêng cho mỗi user |
| Cùng password → | Cùng hash | Khác hash (do salt khác) |
| Chống brute-force GPU | ❌ Dễ bẻ | ✅ Khó hơn ~1 tỷ lần |
| Chuẩn ngành | ❌ | ✅ |

### Các biện pháp bảo mật khác

- Thông báo lỗi login dùng chung cho cả "sai username" lẫn "sai password" → tránh username enumeration
- `UserSessionDto` không chứa `PasswordHash` → hash không rò ra UI layer
- Soft delete User (`IsActive = false`) → tài khoản bị khóa không đăng nhập được nhưng dữ liệu lịch sử vẫn toàn vẹn
- `UpdateAsync` bảo vệ cột `PasswordHash` không bị ghi đè khi cập nhật thông tin thông thường

---

## 7. Quy trình phát triển 10 giai đoạn

Đồ án được thực hiện theo lộ trình **10 giai đoạn** — từ phân tích yêu cầu đến sản phẩm hoàn chỉnh.  
Mỗi giai đoạn có mục tiêu rõ ràng, danh sách việc cần làm và kết quả bàn giao cụ thể.

---

### ✅ Giai đoạn 1 — Nền tảng kiến trúc & Đăng nhập *(Đã hoàn thành)*

> Mục tiêu: Xây dựng toàn bộ khung kiến trúc, kết nối database, và hoàn thiện luồng đăng nhập hoạt động thực sự.

**Phân tích & Thiết kế**
- Xác định bài toán, 3 actor (Admin / Manager / Developer), 23 Use Case
- Thiết kế ERD 15 bảng với quan hệ, ràng buộc khóa ngoại và 12 indexes
- Chọn Clean Architecture 4 tầng + Repository Pattern + Service Layer
- Thiết kế phân quyền: ma trận quyền theo Role

**Cài đặt Backend**
- Khởi tạo Solution 3 project: `.Application`, `.Infrastructure`, `.WinForms`
- Viết 15 Entity class + Fluent API cấu hình toàn bộ quan hệ trong `AppDbContext`
- Tạo Migration, Auto-seed dữ liệu mẫu (15 users, 5 projects, 50 tasks)
- Viết `IRepository<T>` + 4 Repository với query tối ưu (`AsNoTracking`, `ExecuteUpdateAsync`, `GetPagedAsync`)
- Viết 4 Service: `AuthService`, `UserService`, `ProjectService`, `TaskService`
- Bảo mật mật khẩu BCrypt WorkFactor 12 qua `PasswordHelper`
- `ValidationHelper`, `LoginResult` Result Pattern, `UserSessionDto` (không expose PasswordHash)

**Cài đặt UI**
- `frmLogin`: dark navy 2-panel, logo gradient, input có icon, loading bar, shake animation khi sai mật khẩu
- `frmMain`: MDI Container, menu ẩn/hiện theo Role, đồng hồ realtime, luồng logout → login lại
- `AppSession` static session, DI Container đầy đủ trong `Program.cs`

**Kết quả bàn giao:** Ứng dụng chạy được — đăng nhập, phân quyền menu, đăng xuất hoạt động đúng với cả 3 loại tài khoản

---

### ✅ Giai đoạn 2 — Quản lý Người dùng & Khách hàng *(Đã hoàn thành)*

> Mục tiêu: Admin quản lý được tài khoản, Manager quản lý được danh sách khách hàng.

**Việc cần làm**
- `frmUsers` (Admin only):
  - DataGridView hiển thị danh sách users, lọc theo Role / Active
  - Tạo tài khoản mới: validate username duy nhất, email, hash BCrypt
  - Sửa thông tin: họ tên, email, số điện thoại (không cho sửa username)
  - Vô hiệu hóa tài khoản: soft delete `IsActive = false`
- `frmCustomers` (Manager/Admin):
  - DataGridView danh sách khách hàng, tìm kiếm real-time
  - Form thêm/sửa: tên công ty, người liên hệ, email, điện thoại, địa chỉ
  - Xóa khách hàng (kiểm tra không còn dự án đang active)
- `frmChangePassword` (tất cả user):
  - Verify mật khẩu cũ → validate mật khẩu mới → hash BCrypt → lưu

**Kết quả bàn giao:** Admin tạo/khóa tài khoản được, Manager thêm/sửa khách hàng được

---

### ✅ Giai đoạn 3 — Quản lý Dự án *(Đã hoàn thành)*

> Mục tiêu: Manager tạo và quản lý dự án, thêm thành viên vào dự án.

**Cài đặt Backend**
- Bổ sung `IProjectRepository`: HasTasksAsync, GetMembersAsync, AddMemberAsync, RemoveMemberAsync
- Bổ sung `IProjectService`: ChangeStatusAsync, implement AddMember/RemoveMember đầy đủ
- `DeleteProjectAsync` kiểm tra có task trước khi xóa (bảo vệ dữ liệu)
- Entity `TaskItem` thêm: Reviewer1Id, Reviewer2Id, TesterId (chuẩn bị workflow GD4)
- Entity `Status` thêm Description + thay thế 4 status cũ bằng 10 status mới (workflow phần mềm)
- `AppDbContext` thêm Fluent API cho Reviewer1/Reviewer2/Tester + 3 indexes mới
- Migration `GD3_WorkflowStatus` cập nhật DB schema

**Cài đặt UI**
- `frmProjects` (Manager/Admin + Developer read-only):
  - DataGridView: Tên, Khách hàng, PM, Trạng thái, Thành viên, Deadline, Ngân sách
  - Lọc theo Status, tìm kiếm theo tên/khách hàng/PM
  - Tô màu: hoàn thành (xanh), hủy (xám italic), quá hạn (đỏ)
  - Developer: xem read-only, double-click xem chi tiết
- `frmProjectEdit`: Form tạo/sửa dự án, dropdown khách hàng + PM, validate deadline > ngày bắt đầu
- `frmProjectMembers`: Thêm/xóa thành viên, gán vai trò (Developer/Tester/BA/Tech Lead), soft delete
- Chi tiết dự án (UC-15): hiển thị info + members + task count + ngân sách
- Menu "Dự án" kết nối cho cả 3 role, "Quản lý nhân viên" → frmUsers
- DI Container đăng ký frmProjects

**Kết quả bàn giao:** Manager tạo/sửa/xóa dự án, quản lý thành viên, Developer xem dự án mình tham gia


---

### 🔲 Giai đoạn 4 — Quản lý Công việc (Task)

> Mục tiêu: Manager phân công task, Developer cập nhật tiến độ công việc của mình.

**Việc cần làm**
- `frmTaskList` (Manager/Admin):
  - DataGridView phân trang, multi-filter: dự án / người phụ trách / status / priority / từ khóa
  - Tạo/sửa task: tiêu đề, mô tả, dự án, người phụ trách, priority, category, deadline
  - Xóa task (kiểm tra không còn sub-task)
  - Tạo sub-task: chọn task cha, hiển thị dạng cây
- `frmMyTasks` (tất cả):
  - Danh sách task được giao cho user đang đăng nhập
  - Sắp xếp theo deadline gần nhất, highlight task quá hạn màu đỏ
  - TrackBar cập nhật tiến độ 0–100% → tự đánh dấu Done khi = 100%

**Kết quả bàn giao:** Task CRUD hoàn chỉnh, developer cập nhật tiến độ được

---

### 🔲 Giai đoạn 5 — Kanban Board

> Mục tiêu: Manager theo dõi tiến độ dự án theo dạng bảng Kanban trực quan.

**Việc cần làm**
- `frmKanban` (Manager/Admin):
  - 4 cột cố định: **Todo** | **InProgress** | **InReview** | **Done**
  - Mỗi cột là Panel cuộn được, hiển thị task card (tên + assignee + deadline + priority color)
  - Kéo-thả task card giữa các cột → tự động cập nhật `StatusId` trong DB
  - Lọc theo dự án (ComboBox chọn project)
  - Đếm số task trên header mỗi cột
  - Double-click task card → mở form chi tiết/sửa

**Kết quả bàn giao:** Kanban board kéo-thả hoạt động, cập nhật realtime vào DB

---

### 🔲 Giai đoạn 6 — Dashboard & Báo cáo

> Mục tiêu: Manager/Admin có màn hình tổng quan nắm bắt toàn bộ tình trạng hệ thống.

**Việc cần làm**
- `frmDashboard` (Manager/Admin):
  - **Widget đỏ** — Task quá hạn: số lượng + danh sách, click để xem chi tiết
  - **Widget vàng** — Task sắp hạn 7 ngày tới
  - **Widget xanh** — Dự án đang active (InProgress)
  - **Pie chart** — Phân bổ task theo Status (dùng `GetStatusSummaryByProjectAsync`)
  - **Bảng dự án** — Tiến độ % + Budget vs thực tế
- `frmReportBudget` (Manager/Admin):
  - Bảng so sánh Budget vs TotalExpense từng dự án
  - Highlight đỏ dự án vượt ngân sách

**Kết quả bàn giao:** Dashboard tổng quan hoạt động, báo cáo ngân sách đầy đủ

---

### 🔲 Giai đoạn 7 — Bình luận & Đính kèm

> Mục tiêu: Thành viên trao đổi trực tiếp trên từng task, đính kèm file liên quan.

**Việc cần làm**
- Panel **Comments** trong form chi tiết task:
  - Hiển thị danh sách bình luận theo thời gian (avatar + tên + nội dung + giờ)
  - Thêm bình luận mới (TextBox + nút Gửi hoặc Enter)
  - Xóa bình luận của chính mình
- Panel **Attachments** trong form chi tiết task:
  - Upload file: lưu đường dẫn vào bảng `Attachments`, copy file vào thư mục `uploads/`
  - Danh sách file đính kèm: tên, dung lượng, người upload
  - Mở file bằng ứng dụng mặc định (`Process.Start`)
  - Xóa file đính kèm

**Kết quả bàn giao:** Trao đổi và đính kèm file trên task hoạt động được

---

### 🔲 Giai đoạn 8 — Quản lý Chi phí (Expense)

> Mục tiêu: Theo dõi chi phí thực tế phát sinh trong từng dự án, so sánh với ngân sách.

**Việc cần làm**
- `frmExpenses` trong chi tiết dự án (Manager/Admin):
  - DataGridView danh sách chi phí: tên khoản, số tiền, ngày, người nhập
  - Thêm khoản chi phí mới (tên, amount, ghi chú)
  - Sửa / Xóa khoản chi phí
  - Tổng chi phí hiển thị realtime, so sánh với Budget
  - Cảnh báo khi `TotalExpense > Budget` (label đỏ + icon ⚠️)
- Cập nhật Dashboard: widget ngân sách phản ánh dữ liệu Expense mới nhất

**Kết quả bàn giao:** Nhập/xem chi phí dự án, biết ngay dự án có vượt ngân sách không

---

### 🔲 Giai đoạn 9 — Tìm kiếm toàn cục & Tối ưu UX

> Mục tiêu: Tăng tốc độ thao tác, cải thiện trải nghiệm người dùng trên toàn ứng dụng.

**Việc cần làm**
- **Tìm kiếm toàn cục** trên MenuStrip: gõ từ khóa → hiện kết quả gộp (task + project + user)
- **Phím tắt** cho thao tác thường dùng: `Ctrl+N` tạo mới, `F5` refresh, `Escape` đóng form con
- **Thông báo Toast** thay thế MessageBox.Show cho các thao tác thành công (nhẹ nhàng hơn)
- **Xác nhận trước khi xóa** — hiện dialog với tên cụ thể của bản ghi sắp xóa
- **Pagination UX** — nút Prev/Next + hiển thị "Trang X / Y (Z bản ghi)"
- **DataGridView** — double-click mở chi tiết, click cột header để sort
- **Remember last filter** — lưu lại bộ lọc cuối cùng khi thoát form

**Kết quả bàn giao:** Ứng dụng dùng nhanh, ít click hơn, ít popup làm phiền hơn

---

### 🔲 Giai đoạn 10 — Kiểm thử, Hoàn thiện & Báo cáo

> Mục tiêu: Đảm bảo ứng dụng ổn định, tài liệu đầy đủ, sẵn sàng demo trước giảng viên.

**Việc cần làm**
- **Kiểm thử toàn bộ luồng** với cả 3 role (Admin / Manager / Developer):
  - Tạo user → login → phân công task → cập nhật tiến độ → xem Dashboard
  - Kiểm tra phân quyền: Developer không truy cập được menu Manager
  - Kiểm tra edge case: xóa user đang có task, xóa project đang có thành viên
- **Kiểm thử dữ liệu lớn**: seed 500+ task, kiểm tra phân trang và tốc độ load
- **Sửa toàn bộ bug** phát sinh trong quá trình test
- **Cập nhật tài liệu**: README, CHANGELOG, comments trong code
- **Chuẩn bị demo**: slide tóm tắt kiến trúc, kịch bản demo 10 phút, tài khoản test sẵn
- **Code review**: đảm bảo comment đầy đủ, xóa code thừa, format nhất quán

**Kết quả bàn giao:** Sản phẩm hoàn chỉnh — chạy ổn định, tài liệu đầy đủ, sẵn sàng báo cáo cuối kỳ

---

## 8. Cài đặt & Chạy chương trình

### Yêu cầu hệ thống
- Windows 10/11 (64-bit)
- [.NET 8.0 Runtime](https://dotnet.microsoft.com/download/dotnet/8.0) (hoặc .NET 8 SDK)
- [SQL Server Express 2022](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (Community trở lên)

---

### Bước 1 — Clone repository

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

### Bước 2 — Kiểm tra SQL Server đang chạy

1. Mở **SQL Server Configuration Manager**
2. Vào **SQL Server Services**
3. Đảm bảo `SQL Server (SQLEXPRESS2022)` → **Running** ✅

> Nếu không tìm thấy SQL Server Configuration Manager: nhấn `Win + R` → gõ `SQLServerManager16.msc`

---

### Bước 3 — Kiểm tra Connection String

Mở `TaskFlowManagement/TaskFlowManagement.WinForms/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS2022;Database=TaskFlowManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

> ⚠️ Nếu tên SQL instance khác `SQLEXPRESS2022`, sửa lại cho đúng.  
> Kiểm tra tên: **SQL Server Configuration Manager → SQL Server Services** → xem tên trong ngoặc đơn.

---

### Bước 4 — Mở Solution

```
File → Open → Project/Solution
→ Chọn: TaskFlowManagement/TaskFlowManagement.sln
```

---

### Bước 5 — Restore NuGet Packages

**Visual Studio:**
```
Build → Restore NuGet Packages
```

**Package Manager Console** (`Tools → NuGet Package Manager → Package Manager Console`):
```powershell
dotnet restore
```

---

### Bước 6 — Build & Chạy

**Visual Studio:**
- Startup Project: `TaskFlowManagement.WinForms`
- Nhấn `F5` (Debug) hoặc `Ctrl+F5` (Run without debug)

**Terminal:**
```bash
cd TaskFlowManagement/TaskFlowManagement.WinForms
dotnet run
```

---

### Lần đầu chạy — Tự động setup

Chương trình sẽ **tự động** khi khởi động lần đầu:
1. ✅ Tạo database `TaskFlowManagementDb` trên SQL Server
2. ✅ Chạy migration tạo 15 bảng + indexes
3. ✅ Seed dữ liệu mẫu: 15 users, 5 projects, 50 tasks

> 🕒 Lần đầu có thể mất **30–60 giây** vì BCrypt hash 15 mật khẩu (WorkFactor 12 ≈ 300ms/hash).

---

### ⚠️ Xử lý lỗi "Sai mật khẩu" sau khi update code

**Nguyên nhân:** DB cũ lưu hash SHA-256, code mới dùng BCrypt → không khớp.

**Cách fix — chọn 1 trong 3:**

**[A] Package Manager Console** (khuyến nghị):
```
Tools → NuGet Package Manager → Package Manager Console
Default project: TaskFlowManagement.Infrastructure
```
```powershell
Drop-Database
```

**[B] Terminal:**
```bash
cd TaskFlowManagement/TaskFlowManagement.Infrastructure
dotnet ef database drop --force
```

**[C] SQL Server Management Studio:**
```
Kết nối → Databases → chuột phải TaskFlowManagementDb → Delete → OK
```

Sau khi drop → **chạy lại app** → DB tự tạo mới với BCrypt ✅

---

## 9. Tài khoản mặc định

| Username | Mật khẩu | Role | Họ tên |
|----------|----------|------|--------|
| `admin` | `Admin@123` | Admin | Quản trị viên |
| `manager1` | `Manager@123` | Manager | Nguyễn Văn Minh |
| `manager2` | `Manager@123` | Manager | Trần Thị Lan |
| `manager3` | `Manager@123` | Manager | Lê Quang Huy |
| `dev1` | `Dev@123` | Developer | Phạm Văn An |
| `dev2` | `Dev@123` | Developer | Hoàng Thị Bình |
| `dev3` | `Dev@123` | Developer | Vũ Minh Cường |
| `dev4` → `dev11` | `Dev@123` | Developer | *(xem UserSeeder.cs)* |

---

## 10. Cấu trúc thư mục

```
Do-An_Giai-Doan-3/
├── README.md                          ← File này
├── TaskFlowManagement/
│   ├── TaskFlowManagement.sln
│   ├── README.md                      ← Hướng dẫn chi tiết cài đặt
│   ├── CHANGELOG.md                   ← Lịch sử thay đổi
│   │
│   ├── docs/
│   │   ├── architecture.md            ← Sơ đồ kiến trúc + request flow
│   │   ├── erd.md                     ← ERD 15 bảng + indexes
│   │   └── usecase.md                 ← Use Case + ma trận phân quyền
│   │
│   ├── TaskFlowManagement.Application/     ← CORE LAYER
│   │   ├── Entities/                       ← 15 Entity class (ánh xạ DB)
│   │   ├── Interfaces/                     ← IRepository + IService interfaces
│   │   │   └── Services/
│   │   ├── Services/                       ← Business Logic
│   │   │   ├── Auth/                       ← AuthService, UserSessionMapper
│   │   │   ├── Users/                      ← UserService
│   │   │   ├── Projects/                   ← ProjectService
│   │   │   └── Tasks/                      ← TaskService
│   │   ├── DTOs/                           ← LoginResult, UserSessionDto
│   │   ├── Helpers/                        ← PasswordHelper, ValidationHelper
│   │   └── GlobalUsings.cs
│   │
│   ├── TaskFlowManagement.Infrastructure/  ← DATA LAYER
│   │   ├── Data/
│   │   │   ├── AppDbContext.cs              ← EF Core DbContext (Fluent API)
│   │   │   ├── DbSeeder.cs                 ← Orchestrator seed
│   │   │   └── Seed/                       ← LookupSeeder, UserSeeder, ProjectSeeder
│   │   ├── Repositories/                   ← 4 Repository implementations
│   │   └── Migrations/                     ← EF Core migrations (auto-generated)
│   │
│   └── TaskFlowManagement.WinForms/        ← PRESENTATION LAYER
│       ├── Program.cs                       ← DI Container + startup
│       ├── appsettings.json                 ← Connection string
│       ├── Common/
│       │   └── AppSession.cs                ← Static session (user đang đăng nhập)
│       └── Forms/
│           ├── frmLogin.cs / .Designer.cs        ← Form đăng nhập
│           ├── frmMain.cs / .Designer.cs         ← MDI Container chính
│           ├── frmHome.cs / .Designer.cs         ← Trang chủ (thống kê nhanh)
│           ├── frmUsers.cs / .Designer.cs        ← Quản lý tài khoản (Admin)
│           ├── frmUserEdit.cs / .Designer.cs     ← Thêm/sửa tài khoản
│           ├── frmCustomers.cs / .Designer.cs    ← Quản lý khách hàng
│           ├── frmCustomerEdit.cs / .Designer.cs ← Thêm/sửa khách hàng
│           ├── frmChangePassword.cs / .Designer.cs ← Đổi mật khẩu
│           ├── frmProjects.cs / .Designer.cs     ← Quản lý dự án (GD3)
│           ├── frmProjectEdit.cs / .Designer.cs  ← Thêm/sửa dự án (GD3)
│           └── frmProjectMembers.cs / .Designer.cs ← Thành viên dự án (GD3)
```

---

## 11. Công nghệ sử dụng

| Công nghệ | Phiên bản | Mục đích |
|-----------|-----------|----------|
| C# | 12 | Ngôn ngữ lập trình chính |
| .NET | 8.0 LTS | Runtime |
| Windows Forms | .NET 8 | UI Desktop |
| Entity Framework Core | 8.0 | ORM – ánh xạ DB |
| SQL Server Express | 2022 | Hệ quản trị cơ sở dữ liệu |
| BCrypt.Net-Next | 4.0.3 | Hash mật khẩu (WorkFactor 12) |
| Microsoft.Extensions.DependencyInjection | 8.0 | Dependency Injection Container |
| Microsoft.Extensions.Configuration | 8.0 | Đọc appsettings.json |

---

## 📄 Tài liệu liên quan

- [Kiến trúc hệ thống](TaskFlowManagement/docs/architecture.md)
- [ERD – Thiết kế cơ sở dữ liệu](TaskFlowManagement/docs/erd.md)
- [Use Case & Phân quyền](TaskFlowManagement/docs/usecase.md)
- [CHANGELOG](TaskFlowManagement/CHANGELOG.md)

---

*Đồ án Lập trình Quản lý – Nhóm N5 – Thứ 2*
