# 👤 Use Case Diagram – TaskFlow Management

## Tổng quan phân quyền

```
                    ┌─────────────────────────────────────────┐
                    │           TaskFlow Management           │
                    │                                         │
  ┌──────────┐      │  ┌─────────────────────────────────┐   │
  │          │      │  │  <<include>> Đăng nhập           │   │
  │  Admin   │─────►│  │  <<include>> Đổi mật khẩu       │   │
  │          │      │  │  <<include>> Đăng xuất           │   │
  └────┬─────┘      │  └─────────────────────────────────┘   │
       │            │                                         │
       │ extends    │  ┌─────────────────────────────────┐   │
       ▼            │  │  👥 Quản lý Người dùng           │   │
  ┌──────────┐      │  │  • Xem danh sách                │   │
  │          │─────►│  │  • Tạo tài khoản                │   │
  │ Manager  │      │  │  • Vô hiệu hóa tài khoản        │   │
  │          │      │  └─── Admin only ────────────────── ┘   │
  └────┬─────┘      │                                         │
       │            │  ┌─────────────────────────────────┐   │
       │ extends    │  │  🏢 Quản lý Khách hàng           │   │
       ▼            │  │  • Xem / Tìm kiếm                │   │
  ┌──────────┐      │  │  • Thêm / Sửa / Xóa             │   │
  │          │─────►│  └─── Manager + Admin ──────────── ┘   │
  │Developer │      │                                         │
  │          │      │  ┌─────────────────────────────────┐   │
  └──────────┘      │  │  📁 Quản lý Dự án               │   │
                    │  │  • Xem danh sách                │   │
                    │  │  • Tạo / Sửa / Xóa              │   │
                    │  │  • Thêm / Xóa thành viên        │   │
                    │  └─── Manager + Admin ──────────── ┘   │
                    │                                         │
                    │  ┌─────────────────────────────────┐   │
                    │  │  ✅ Quản lý Công việc            │   │
                    │  │  • Xem tất cả task (Manager)    │   │
                    │  │  • Kanban Board (Manager)        │   │
                    │  │  • "Công việc của tôi" (All)    │   │
                    │  │  • Cập nhật tiến độ (Assignee)  │   │
                    │  └─────────────────────────────────┘   │
                    │                                         │
                    │  ┌─────────────────────────────────┐   │
                    │  │  📊 Báo cáo                      │   │
                    │  │  • Dashboard tổng quan           │   │
                    │  │  • Báo cáo tiến độ               │   │
                    │  │  • Báo cáo ngân sách             │   │
                    │  └─── Manager + Admin ──────────── ┘   │
                    └─────────────────────────────────────────┘
```

## Ma trận quyền chi tiết

| Chức năng | Developer | Manager | Admin |
|-----------|:---------:|:-------:|:-----:|
| Đăng nhập / Đăng xuất | ✅ | ✅ | ✅ |
| Đổi mật khẩu | ✅ | ✅ | ✅ |
| Xem "Công việc của tôi" | ✅ | ✅ | ✅ |
| Cập nhật tiến độ task | ✅ | ✅ | ✅ |
| Xem tất cả task dự án | ❌ | ✅ | ✅ |
| Kanban Board | ❌ | ✅ | ✅ |
| Quản lý dự án | ❌ | ✅ | ✅ |
| Quản lý khách hàng | ❌ | ✅ | ✅ |
| Xem báo cáo | ❌ | ✅ | ✅ |
| Quản lý tài khoản | ❌ | ❌ | ✅ |
| Quản lý nhân viên | ❌ | ✅ | ✅ |
