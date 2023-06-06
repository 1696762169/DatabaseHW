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
		[HttpGet]
		public IActionResult GetHouseCondition()
		{
			return PartialView(nameof(SaveHouseCondition), m_ConditionRepository.GetHouseCondition(Account.ONLY_ONE));
		}

		[HttpGet]
		public IActionResult GetJobConditionJson()
		{
			JobCondition temp = m_ConditionRepository.GetJobCondition(Account.ONLY_ONE);
			temp.Account = null;
			return Json(temp);
		}
		[HttpGet]
		public IActionResult GetHouseConditionJson()
		{
			HouseCondition temp = m_ConditionRepository.GetHouseCondition(Account.ONLY_ONE);
			temp.Account = null;
			return Json(temp);
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
		public IActionResult SaveHouseCondition([FromForm] HouseCondition condition)
		{
			try
			{
				m_ConditionRepository.UpdateHouseCondition(condition);
			}
			catch (DbUpdateException)
			{
			}
			return PartialView(condition);
		}
	}
}
