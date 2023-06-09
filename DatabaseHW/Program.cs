//#define DEBUG_DB_CONTEXT
using DatabaseHW.Data;
using Microsoft.EntityFrameworkCore;

namespace DatabaseHW
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            Startup startup = new(builder.Configuration);

#if DEBUG_DB_CONTEXT
            DbContextOptionsBuilder<DataContext> optionsBuilder = new();
            optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            DataContext dataContext = new(optionsBuilder.Options);
            dataContext.Test();
#endif

            // 添加服务
            startup.ConfigureServices(builder.Services, builder.Environment);
            // 创建应用
            WebApplication app = builder.Build();
            // 配置请求处理管道
            startup.Configure(app, builder.Environment);
            // 运行应用
            app.Run();
        }
    }
}