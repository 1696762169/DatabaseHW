using DatabaseHW.Models;
using DatabaseHW.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseHW.Controllers
{
	public class SecondaryController : Controller
	{
		private readonly IJobFilter m_JobFilter;
		private readonly IHouseFilter m_HouseFilter;

		public SecondaryController(IJobFilter jobFilter, IHouseFilter houseFilter)
		{
			m_JobFilter = jobFilter;
			m_HouseFilter = houseFilter;
		}

		// 查询岗位
		[HttpPost]
		public IActionResult GetJobList([FromBody] JobListRequestModel model)
		{
			return Json(m_JobFilter.Filter(model.Id, model.Condition));
		}
		// 查询房屋
		[HttpPost]
		public IActionResult GetHouseList([FromBody] HouseListRequestModel model)
		{
			return Json(m_HouseFilter.Filter(model.Id, model.Condition));
		}

		public class JobListRequestModel
		{
			public int Id { get; set; }
			public JobCondition Condition { get; set; } = null!;
		}
		public class HouseListRequestModel
		{
			public int Id { get; set; }
			public HouseCondition Condition { get; set; } = null!;
		}
	}
}
