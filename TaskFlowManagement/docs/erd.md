# 🗃 Entity Relationship Diagram (ERD)

## Sơ đồ quan hệ bảng

```
┌─────────────┐         ┌─────────────┐         ┌─────────────┐
│    Users    │◄────────│  UserRoles  │────────►│    Roles    │
│─────────────│  N    N │─────────────│ N     1 │─────────────│
│ Id (PK)     │         │ UserId (FK) │         │ Id (PK)     │
│ Username    │         │ RoleId (FK) │         │ Name        │
│ FullName    │         └─────────────┘         │ Description │
│ Email       │                                  └─────────────┘
│ PasswordHash│
│ IsActive    │◄──────────────────────────────────────────────────┐
│ LastLogin   │                                                    │
└──────┬──────┘                                                    │
       │ 1                                                         │
       │ owns N                                                    │
       ▼                                                           │
┌─────────────┐  N    1  ┌─────────────┐                         │
│  Projects   │─────────►│  Customers  │                         │
│─────────────│          │─────────────│                         │
│ Id (PK)     │          │ Id (PK)     │                         │
│ Name        │          │ CompanyName │                         │
│ OwnerId(FK) │◄──────┐  │ ContactName │                         │
│ CustomerId  │       │  │ Email       │                         │
│ Status      │       │  └─────────────┘                         │
│ Budget      │       │                                           │
│ StartDate   │       │  ┌──────────────┐                        │
└──────┬──────┘       │  │ProjectMembers│                        │
       │ 1            └──│──────────────│                        │
       │ has N           │ Id (PK)      │                        │
       ▼                 │ ProjectId(FK)│                        │
┌─────────────┐          │ UserId(FK)──►┘                        │
│  TaskItems  │          │ ProjectRole  │                        │
│─────────────│          │ JoinedAt     │                        │
│ Id (PK)     │          └──────────────┘                        │
│ Title       │                                                   │
│ ProjectId   │◄──────────────────────────────────────────────┐  │
│ AssignedToId│──────────────────────────────────────────────►┘  │
│ CreatedById │─────────────────────────────────────────────────►┘
│ StatusId    │──►┌──────────┐
│ PriorityId  │──►│Priorities│ (Lookup)
│ CategoryId  │──►├──────────┤
│ ParentTaskId│──►│ Statuses │ (Lookup)
│ DueDate     │   ├──────────┤
│ Progress%   │   │Categories│ (Lookup)
└──────┬──────┘   └──────────┘
       │ 1
       ├──── has N ──► Comments
       ├──── has N ──► Attachments
       └──── has N ──► TaskTags ──► Tags

┌──────────────┐
│   Expenses   │
│──────────────│
│ Id (PK)      │
│ ProjectId(FK)│──► Projects
│ CreatedById  │──► Users
│ Amount       │
│ ExpenseType  │
└──────────────┘
```

## Bảng tóm tắt

| Bảng | Loại | Mô tả |
|------|------|-------|
| Users | Core | Tài khoản người dùng |
| Roles | Lookup | Admin / Manager / Developer |
| UserRoles | Junction | Phân quyền User ↔ Role |
| Customers | Core | Khách hàng đặt dự án |
| Projects | Core | Dự án phần mềm |
| ProjectMembers | Junction | Thành viên tham gia dự án |
| TaskItems | Core | Công việc (hỗ trợ sub-task) |
| Comments | Child | Bình luận trong task |
| Attachments | Child | File đính kèm task |
| Expenses | Child | Chi phí dự án |
| Priorities | Lookup | Low/Medium/High/Critical |
| Statuses | Lookup | Todo/InProgress/InReview/Done |
| Categories | Lookup | Bug/Feature/Improvement/... |
| Tags | Lookup | Nhãn tự do |
| TaskTags | Junction | Task ↔ Tag |

## Indexes quan trọng

| Index | Lý do |
|-------|-------|
| `IX_TaskItems_ProjectId_StatusId` | Composite – Kanban query |
| `IX_TaskItems_AssignedToId` | My Tasks view |
| `IX_TaskItems_DueDate` | Overdue / DueSoon |
| `IX_Projects_OwnerId` | GetByOwner |
| `IX_ProjectMembers_UserId` | GetByMember |
