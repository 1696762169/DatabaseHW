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
    }
}