// GlobalUsings.cs – áp dụng cho toàn bộ Application project
//
// ⚠ LƯU Ý QUAN TRỌNG:
//   "global using Alias = Type" KHÔNG hỗ trợ type-arguments (e.g. Alias<T>)
//   → KHÔNG dùng kiểu đó. Chỉ import namespace bình thường như bên dưới.
//
// Nhờ dòng này, mọi file trong project đều dùng được Task<T>
// dù namespace của file là Services.Tasks (dễ gây nhầm với C# Task).
global using System.Threading.Tasks;
