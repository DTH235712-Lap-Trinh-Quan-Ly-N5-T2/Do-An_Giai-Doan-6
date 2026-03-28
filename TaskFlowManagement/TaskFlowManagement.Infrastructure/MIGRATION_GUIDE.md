# Hướng dẫn Migration

## Lần đầu tiên (hoặc sau khi thay đổi entities)

```bash
# 1. Xóa database cũ (nếu có)
# SQL Server Management Studio: DROP DATABASE TaskFlowManagementDb

# 2. Tạo migration mới
cd TaskFlowManagement.Infrastructure
dotnet ef migrations add FullSchema_v2 --startup-project ../TaskFlowManagement.WinForms

# 3. Apply migration
dotnet ef database update --startup-project ../TaskFlowManagement.WinForms
```

## Tài khoản mặc định sau seed

| Username  | Password    | Role      |
|-----------|-------------|-----------|
| admin     | Admin@123   | Admin     |
| manager1  | Manager@123 | Manager   |
| manager2  | Manager@123 | Manager   |
| dev1      | Dev@123     | Developer |
| dev2      | Dev@123     | Developer |

## Lưu ý

- App đã cấu hình `context.Database.Migrate()` trong Program.cs
- Seed data chạy tự động nếu DB trống
- Password dùng SHA-256 + Salt: `TaskFlow_Salt_2024`
