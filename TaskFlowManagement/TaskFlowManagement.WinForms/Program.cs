using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.Core.Services.Auth;
using TaskFlowManagement.Core.Services.Users;
using TaskFlowManagement.Core.Services.Projects;
using TaskFlowManagement.Core.Services.Tasks;
using TaskFlowManagement.Infrastructure.Data;
using TaskFlowManagement.Infrastructure.Repositories;
using TaskFlowManagement.WinForms.Forms;

namespace TaskFlowManagement.WinForms
{
    /// <summary>
    /// Entry point – cấu hình DI Container và khởi động ứng dụng.
    /// </summary>
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);

            var services = new ServiceCollection();

            // 1. Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            services.AddSingleton<IConfiguration>(config);

            // 2. Database
            services.AddDbContextFactory<AppDbContext>(options =>
                options.UseSqlServer(
                    config.GetConnectionString("DefaultConnection"),
                    sql =>
                    {
                        sql.EnableRetryOnFailure(maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(5),
                            errorNumbersToAdd: null);
                        sql.CommandTimeout(30);
                    })
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            );

            // 3. Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            // 4. Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITaskService, TaskService>();

            // 5. Forms
            services.AddTransient<frmLogin>();
            services.AddTransient<frmMain>();
            services.AddTransient<frmHome>();
            // GD2: Quản lý Người dùng & Khách hàng
            services.AddTransient<frmUsers>();
            services.AddTransient<frmCustomers>();
            services.AddTransient<frmChangePassword>();
            // GD3: Quản lý Dự án
            services.AddTransient<frmProjects>();
            // GD4: Quản lý Công việc
            // FIX: Thêm 3 form GD4 vào DI Container
            // DI tự inject (ITaskService, IProjectService, IUserService) vào constructor
            services.AddTransient<frmTaskList>();
            services.AddTransient<frmMyTasks>();
            // frmTaskEdit KHÔNG đăng ký ở đây vì dùng `new` trực tiếp với tham số taskId runtime

            ServiceProvider = services.BuildServiceProvider();

            // Migrate + Seed
            try
            {
                using var scope = ServiceProvider.CreateScope();
                var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
                using var context = factory.CreateDbContext();
                context.Database.Migrate();
                DbSeeder.SeedAsync(context).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Không thể kết nối database:\n\n{ex.Message}\n\n" +
                    "Kiểm tra:\n" +
                    "  1. SQL Server Express đang chạy\n" +
                    "  2. Tên instance trong appsettings.json đúng không\n" +
                    "  3. Windows Firewall không chặn SQL Server",
                    "Lỗi kết nối Database",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Khởi động: Login → Main
            var loginForm = ServiceProvider.GetRequiredService<frmLogin>();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                var mainForm = ServiceProvider.GetRequiredService<frmMain>();
                Application.Run(mainForm);
            }
        }
    }
}