using DatabaseHW.Models;
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
            int timer = Environment.TickCount;
            m_Generator.GenerateData(model.SelectedType, model.GenerateCount);
            TempData["message"] = $"创建了 {model.GenerateCount} 条 {model.SelectedType} 数据\n用时 {(Environment.TickCount - timer) / 1000.0f:F2} 秒";
            return View(nameof(DataFaker), model);
        }

        [HttpPost]
        public IActionResult GetAllData(DataFakerViewModel model)
        {
            int timer = Environment.TickCount;
            model.GetCount = Math.Min(model.GetCount, 100);
            model.GetResult = model.SelectedType switch
            {
                nameof(Workplace) => m_Generator.GetData<Workplace>(model.GetCount),
                nameof(Community) => m_Generator.GetData<Community>(model.GetCount),
                nameof(House) => m_Generator.GetData<House>(model.GetCount),
                nameof(Job) => m_Generator.GetData<Job>(model.GetCount),
                nameof(Company) => m_Generator.GetData<Company>(model.GetCount),
                _ => throw new ArgumentException("未知的类型"),
            };
            TempData["message"] = $"获取了 {model.GetResult.Count} 条 {model.SelectedType} 数据\n用时 {(Environment.TickCount - timer) / 1000.0f:F2} 秒";
            return View(nameof(DataFaker), model);
        }

        [HttpPost]
        public IActionResult RemoveAllData(DataFakerViewModel model)
        {
            int timer = Environment.TickCount;
            switch (model.SelectedType)
            {
            case nameof(Workplace):
                m_Generator.RemoveAllData<Workplace>();
                break;
            case nameof(Community):
                m_Generator.RemoveAllData<Community>();
                break;
            case nameof(House):
                m_Generator.RemoveAllData<House>();
                break;
            case nameof(Job):
                m_Generator.RemoveAllData<Job>();
                break;
            case nameof(Company):
                m_Generator.RemoveAllData<Company>();
                break;
            }
            TempData["message"] = $"删除了 {model.SelectedType} 数据\n用时 {(Environment.TickCount - timer) / 1000.0f:F2} 秒";
            return View(nameof(DataFaker), model);
        }
    }
}
