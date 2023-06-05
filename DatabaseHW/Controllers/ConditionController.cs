using DatabaseHW.Models;
using DatabaseHW.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DatabaseHW.Controllers
{
	public class ConditionController : Controller
	{
		private readonly IConditionRepository m_ConditionRepository;

		public ConditionController(IConditionRepository conditionRepository)
		{
			m_ConditionRepository = conditionRepository;
		}

		[HttpGet]
		public IActionResult GetJobCondition()
		{
			return PartialView(nameof(SaveJobCondition), m_ConditionRepository.GetJobCondition(Account.ONLY_ONE));
		}
		[HttpPost]
		public IActionResult SaveJobCondition([FromForm] JobCondition condition)
		{
			try
			{
				m_ConditionRepository.UpdateJobCondition(condition);
			}
			catch (DbUpdateException)
			{
			}
			return PartialView(condition);
		}

		[HttpPost]
		public IActionResult SaveHouseCondition(HouseCondition condition)
		{
			m_ConditionRepository.UpdateHouseCondition(condition);
			return Accepted();
		}
	}
}
