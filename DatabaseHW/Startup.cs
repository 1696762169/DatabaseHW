﻿namespace DatabaseHW
{
    public class Startup
    {
        // 向应用中添加服务
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // 配置请求处理管道
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 配置HTTP请求管道
            // 如果不是开发环境
            if (!env.IsDevelopment())
            {
                // 使用自定义错误处理中间件，当发生异常时重定向到 /Home/Error 页面
                app.UseExceptionHandler("/Home/Error");
                // 使用HSTS（HTTP Strict Transport Security），强制客户端使用HTTPS，详情参见：https://aka.ms/aspnetcore-hsts
                app.UseHsts();
            }

            // 将HTTP请求重定向到HTTPS
            app.UseHttpsRedirection();
            // 使用静态文件中间件，用于处理静态文件请求（如CSS、JS、图片等）
            app.UseStaticFiles();
            // 使用路由中间件
            app.UseRouting();

            // 配置控制器路由映射
            app.UseEndpoints(ConfigureRoute);
        }

        // 配置路由
        protected void ConfigureRoute(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                    name: "default", // 路由名称
                    pattern: "{controller=Home}/{action=Index}/{id?}"); // 路由模板
        }
    }
}
