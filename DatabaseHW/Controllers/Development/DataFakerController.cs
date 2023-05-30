using DatabaseHW.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using DatabaseHW.ViewModels.Development;

namespace DatabaseHW.Controllers.Development
{
    /// <summary>
    /// 测试数据管理控制器
    /// </summary>
    public class DataFakerController : Controller
    {
        private readonly IDataGenerator m_Generator;

        public DataFakerController(IDataGenerator generator)
        {
            m_Generator = generator;
        }

        [HttpGet]
        public IActionResult DataFaker()
        {
            return View(new DataFakerViewModel());
        }

        [HttpPost]
        public IActionResult GenerateData(DataFakerViewModel model)
        {
            TempData["message"] = $"创建了 {model.GenerateCount} 条 {model.SelectedType} 数据";
            return View(nameof(DataFaker), model);
        }

        [HttpPost]
        public IActionResult GetAllData(DataFakerViewModel model)
        {
            TempData["message"] = $"获取了 {model.GetCount} 条 {model.SelectedType} 数据";
            return View(nameof(DataFaker), model);
        }

        [HttpPost]
        public IActionResult RemoveAllData(DataFakerViewModel model)
        {
            TempData["message"] = $"删除了 {model.SelectedType} 数据";
            return View(nameof(DataFaker), model);
        }
    }
}
