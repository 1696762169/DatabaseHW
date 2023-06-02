using DatabaseHW.Models;
using DatabaseHW.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DatabaseHW.Controllers
{
    public class HomeController : Controller
    {
        // 定义一个 ILogger<HomeController> 类型的私有成员变量，用于记录日志
        private readonly ILogger<HomeController> m_Logger;

        // 通过依赖注入，自动提供 ILogger<HomeController> 实例
        public HomeController(ILogger<HomeController> logger)
        {
            m_Logger = logger;
        }

        // Index 操作方法，处理对应的 Index 请求
        [HttpGet]
        public IActionResult Index()
        {
            return View(new PrimarySearchViewModel());  // 返回 Index 视图
        }

        // Error 操作方法，处理对应的 Error 请求
        // 使用 ResponseCache 特性设置缓存策略，禁止缓存此响应
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // 返回 Error 视图，并传递一个 ErrorViewModel 实例
            // 设置 ErrorViewModel 的 RequestId 属性为当前请求的 TraceIdentifier（跟踪标识符）
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}