# 🏗 Architecture Diagram – TaskFlow Management

## Clean Architecture Overview

```
┌──────────────────────────────────────────────────────────────────┐
│                    PRESENTATION LAYER                             │
│              TaskFlowManagement.WinForms                          │
│                                                                   │
│   frmLogin ──► frmMain ──► (frmProject, frmTask, ... G2+)        │
│       │             │                                             │
│       ▼             ▼                                             │
│   IAuthService   IProjectService  ITaskService  IUserService     │
└─────────────────────────┬────────────────────────────────────────┘
                          │  (Interface boundary – chỉ biết Interface)
┌─────────────────────────▼────────────────────────────────────────┐
│                   BUSINESS LOGIC LAYER                            │
│              TaskFlowManagement.Application                       │
│                                                                   │
│   Services/          Interfaces/          DTOs/                   │
│   ├─ AuthService     ├─ IAuthService      ├─ LoginResult         │
│   ├─ UserService     ├─ IUserService      └─ UserSessionDto      │
│   ├─ ProjectService  ├─ IProjectService                          │
│   └─ TaskService     ├─ ITaskService                             │
│                      │                                            │
│   Entities/          └─ IRepository<T>                           │
│   ├─ User, Role          IUserRepository                         │
│   ├─ Project             IProjectRepository                      │
│   ├─ TaskItem            ITaskRepository                         │
│   └─ ... (15 entities)   ICustomerRepository                     │
└─────────────────────────┬────────────────────────────────────────┘
                          │  (Interface boundary)
┌─────────────────────────▼────────────────────────────────────────┐
│                    DATA ACCESS LAYER                               │
│              TaskFlowManagement.Infrastructure                     │
│                                                                   │
│   Repositories/                Data/                              │
│   ├─ UserRepository            ├─ AppDbContext (EF Core)         │
│   ├─ ProjectRepository         ├─ DbSeeder (seed data)           │
│   ├─ TaskRepository            └─ Migrations/                    │
│   └─ CustomerRepository                                           │
│                                                                   │
│                         ▼                                         │
│                   SQL Server Express                              │
│                   (TaskFlowManagementDb)                          │
└──────────────────────────────────────────────────────────────────┘
```

## Dependency Rule

```
WinForms  ──►  Application (Interfaces only)
Infrastructure  ──►  Application (Entities + Interfaces)
Application  ──►  (không phụ thuộc vào ai)
```

> Tầng trong không biết tầng ngoài tồn tại.  
> Tầng ngoài phụ thuộc vào Interface, không phụ thuộc vào Implementation.

## Request Flow – Ví dụ đăng nhập

```
frmLogin.btnLogin_Click()
    │
    ▼
IAuthService.LoginAsync(username, password)
    │
    ├─► Validate input (guard clauses)
    ├─► IUserRepository.GetByUsernameAsync()  ──► SQL: SELECT * FROM Users WHERE Username=?
    ├─► BCrypt.Verify(password, hash)
    ├─► IUserRepository.GetWithRolesAsync()   ──► SQL: SELECT + JOIN UserRoles + Roles
    ├─► IUserRepository.UpdateLastLoginAsync() ──► SQL: UPDATE Users SET LastLogin=NOW
    └─► return LoginResult.Ok(UserSessionDto)
    │
    ▼
AppSession.Login(dto)  ←  lưu vào static session
    │
    ▼
frmMain hiện ra với đúng menu theo role
```
