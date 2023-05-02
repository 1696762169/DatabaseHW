namespace DatabaseHW
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)  // 创建Web应用程序构建器
                .UseStartup<Startup>()  // 应用初始配置
                .Build()    // 构建Web应用程序
                .Run(); // 启动Web应用程序
        }
    }
}