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
		public IActionResult GetJobList([FromBody] int id, [FromBody] JobCondition condition)
		{
			return Json(m_JobFilter.Filter(id, condition));
		}
		// 查询房屋
		public IActionResult GetHouseList([FromBody] int id, [FromBody] HouseCondition condition)
		{
			return Json(m_HouseFilter.Filter(id, condition));
		}
	}
}
