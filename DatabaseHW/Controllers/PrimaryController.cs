using DatabaseHW.Models;
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
        private readonly IRecordRepository m_RecordRepository;

        public PrimaryController(IWorkplaceFilter workplaceFilter, ICommunityFilter communityFilter, IRecordRepository recordRepository)
        {
            m_WorkplaceFilter = workplaceFilter;
            m_CommunityFilter = communityFilter;
            m_RecordRepository = recordRepository;
        }

        [HttpPost]
        public IActionResult Search(PrimarySearchViewModel model)
        {
            // 验证数据是否正确
            if (string.IsNullOrEmpty(model.Key) && (model.Longitude >= float.MaxValue || model.Latitude >= float.MaxValue || model.Range <= 0))
                return Redirect("/");
            
            // 记录历史记录
	        m_RecordRepository.AddRecord(new Record
	        {
                Key = model.Key,
                Longitude = model.Longitude,
                Latitude = model.Latitude,
                ApplyTime = DateTime.Now,
	        }, Account.ONLY_ONE);
            return View(model);
        }

        [HttpGet]
        public IActionResult GetPrimaryList([FromQuery] PrimarySearchViewModel model)
        {
            Console.WriteLine($"{model.Type} {model.Key} {model.Longitude} {model.Latitude} {model.Range}");
            var temp = m_WorkplaceFilter.FilterByDistance(model.Longitude, model.Latitude, model.Range);
            var temp1 = Json(temp);

			return model.Type switch
            {
	            nameof(Workplace) when model.Key == null => 
		            Json(m_WorkplaceFilter.FilterByDistance(model.Longitude, model.Latitude, model.Range)),
	            nameof(Workplace) when model.Longitude >= float.MaxValue || model.Latitude >= float.MaxValue || model.Range <= 0 => 
		            Json(m_WorkplaceFilter.FilterByKeyword(model.Key)),
	            nameof(Workplace) => Json(m_WorkplaceFilter.Filter(model.Key, model.Longitude, model.Latitude, model.Range)),
	            
	            nameof(Community) when model.Key == null => 
		            Json(m_CommunityFilter.FilterByDistance(model.Longitude, model.Latitude, model.Range)),
	            nameof(Community) when model.Longitude >= float.MaxValue || model.Latitude >= float.MaxValue || model.Range <= 0 => 
		            Json(m_CommunityFilter.FilterByKeyword(model.Key)),
	            nameof(Community) => Json(m_CommunityFilter.Filter(model.Key, model.Longitude, model.Latitude, model.Range)),
	            
	            _ => throw new ArgumentException($"一级查询类型有误：{model.Type}")
            };
        }
    }
}
