using DatabaseHW.Services.Interface;
using DatabaseHW.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseHW.Controllers
{
    /// <summary>
    /// 一级结果搜索结果控制器
    /// </summary>
    public class PrimaryController : Controller
    {
        private readonly IWorkplaceFilter m_WorkplaceFilter;
        private readonly ICommunityFilter m_CommunityFilter;

        public PrimaryController(IWorkplaceFilter workplaceFilter, ICommunityFilter communityFilter)
        {
            m_WorkplaceFilter = workplaceFilter;
            m_CommunityFilter = communityFilter;
        }

        [HttpPost]
        public IActionResult Search(PrimarySearchViewModel model)
        {
            // 验证数据是否正确
            if (string.IsNullOrEmpty(model.Key) && (model.Longitude >= float.MaxValue || model.Latitude >= float.MaxValue || model.Range <= 0))
                return Redirect("/");
            Console.WriteLine($"{model.Type} {model.Key} {model.Longitude} {model.Latitude} {model.Range}");
	        return model.Type switch
            {
                nameof(Workplace) => RedirectToAction(nameof(Workplace), model),
                nameof(Community) => RedirectToAction(nameof(Community), model),
                _                 => Redirect("/")
			};
        }

        [HttpGet]
        public IActionResult Workplace(PrimarySearchViewModel model)
        {
            return View();
        }
        [HttpGet]
        public IActionResult Community(PrimarySearchViewModel model)
        {
            return View();
        }
    }
}
