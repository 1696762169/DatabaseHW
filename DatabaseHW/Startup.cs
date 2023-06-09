﻿using DatabaseHW.Data;
using DatabaseHW.Services;
using DatabaseHW.Services.Interface;
using DatabaseHW.Services.Development;
using Microsoft.EntityFrameworkCore;

namespace DatabaseHW
{
    public class Startup
    {
        private readonly IConfiguration m_Configuration;
        public Startup(IConfiguration configuration)
        {
            m_Configuration = configuration;
        }

        // 向应用中添加服务
        public void ConfigureServices(IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddControllersWithViews();
            // 添加数据库上下文服务 指定数据库连接参数
            services.AddDbContext<DataContext>(options =>
            {
                // 从配置文件中读取数据库连接字符串
                string? connectionString = m_Configuration.GetConnectionString("DefaultConnection");
                if (connectionString != null)
                    options.UseSqlServer(connectionString);
                else
                    throw new InvalidOperationException("未找到数据库连接字符串");
            });

            // 添加数据筛选器服务
            services.AddScoped<IWorkplaceFilter, PrimaryFilter>();
            services.AddScoped<ICommunityFilter, PrimaryFilter>();
            services.AddScoped<IJobFilter, JobFilter>();
            services.AddScoped<IHouseFilter, HouseFilter>();

            // 添加用户数据操作服务
            services.AddScoped<IRecordRepository, RecordRepository>();
            services.AddScoped<IConditionRepository, ConditionRepository>();

            // 添加其它数据服务
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            // 添加测试数据服务
            if (env.IsDevelopment())
            {
                services.AddScoped<IDataGenerator, DataFaker>();
            }
        }

        // 配置请求处理管道
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 配置HTTP请求管道
            if (!env.IsDevelopment())   // 如果不是开发环境
            {
                // 使用自定义错误处理中间件，当发生异常时重定向到 /Home/Error 页面
                app.UseExceptionHandler("/Home/Error");
                // 使用HSTS（HTTP Strict Transport Security），强制客户端使用HTTPS
                app.UseHsts();
            }
            else
            {
	            app.UseDeveloperExceptionPage();
            }

            // 将HTTP请求重定向到HTTPS
            app.UseHttpsRedirection();
            // 使用静态文件中间件，用于处理静态文件请求（如CSS、JS、图片等）
            app.UseStaticFiles();
            // 使用路由中间件
            app.UseRouting();

            //app.Use(async (context, next) =>
            //{
	           // try
	           // {
		          //  await next();
	           // }
	           // catch (Exception e)
	           // {
            //        string message = $"出错信息：{e.Message}\n出错资源：{e.Source}\n堆栈信息：{e.StackTrace}";
            //        context.Response.Redirect($"Home/Error?message={5647645}");
	           // }
            //});

			// 配置控制器路由映射
			//app.UseEndpoints(env.IsDevelopment() ? DataFakerRoute : ConfigureRoute);
			app.UseEndpoints(ConfigureRoute);
        }

        // 配置路由
        protected void ConfigureRoute(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "default", // 路由名称
                pattern: "{controller=Home}/{action=Index}/{id?}"); // 路由模板
        }

        protected void DataFakerRoute(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "dataFaker", // 路由名称
                pattern: "{controller=DataFaker}/{action=DataFaker}/{id?}"); // 路由模板    
        }
    }
}
