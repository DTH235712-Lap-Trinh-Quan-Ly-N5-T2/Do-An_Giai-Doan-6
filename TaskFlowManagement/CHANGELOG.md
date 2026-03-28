# Changelog – TaskFlow Management

Format: [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)

---

## [v1.0.0] – 2026-03-05 · Giai đoạn 1: Foundation

### ✅ Kiến trúc
- **Clean Architecture 4 tầng**: Application → Infrastructure → WinForms (+ Service Layer)
- **Dependency Injection** với `Microsoft.Extensions.DependencyInjection`
- **Repository Pattern** với generic `IRepository<T>` + 4 repository chuyên biệt
- **Service Layer**: `AuthService`, `UserService`, `ProjectService`, `TaskService`
- **DTO Pattern**: `LoginResult`, `UserSessionDto` – Form không nhận Entity thô

### ✅ Database
- **Entity Framework Core 8** + SQL Server Express 2022
- **15 Entity/Bảng** với Fluent API config đầy đủ
- **Auto-migration + Auto-seed** khi chạy lần đầu
- **Performance indexes**: 11 indexes đơn + 1 composite index `(ProjectId, StatusId)`
- **Soft delete** cho User (`IsActive = false`)

### ✅ Bảo mật
- **BCrypt** password hashing (WorkFactor 12) – thay thế SHA-256
- Salt tự động per-user – cùng password khác hash

### ✅ UI/UX
- Form Login **2 panel**: branding xanh trái + form trắng phải
- **Eye icon** (👁) toggle hiện/ẩn mật khẩu
- Menu bar + Status bar **navy đậm** (#0F1722)
- **Real-time clock** màu xanh lá trên status bar
- Phân quyền menu tự ẩn/hiện theo Role

### ✅ Tài liệu
- `README.md` với hướng dẫn `git clone` + setup đầy đủ
- `docs/architecture.md` – sơ đồ kiến trúc + request flow
- `docs/erd.md` – ERD 15 bảng + indexes
- `docs/usecase.md` – Use Case + ma trận quyền
- `CHANGELOG.md` – lịch sử thay đổi

### 🔧 Kỹ thuật nổi bật
- `IDbContextFactory` – mỗi Repository tạo DbContext riêng
- `AsNoTracking()` mặc định cho tất cả SELECT
- `ExecuteUpdateAsync` / `ExecuteDeleteAsync` – tránh SELECT-then-UPDATE
- `GetPagedAsync` – phân trang + optional filter chain
- Retry policy 3 lần khi SQL Server tạm lỗi
- Fire-and-forget `UpdateLastLoginAsync` (không block UI)

---

## [v2.0.0] – Giai đoạn 2 (Sắp có)

### 📋 Dự kiến
- [ ] Form CRUD Users (quản lý tài khoản)
- [ ] Form CRUD Projects (tạo/sửa/xóa dự án)
- [ ] Form CRUD Tasks (tạo/sửa/xóa task)
- [ ] Form CRUD Customers
- [ ] Đổi mật khẩu (BCrypt sẵn rồi)
- [ ] Quản lý thành viên dự án
