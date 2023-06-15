using DatabaseHW.Models;
using DatabaseHW.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
				if (condition.SalaryMin > condition.SalaryMax)
				{
					condition.SalaryMin = condition.SalaryMax;
					return PartialView(condition);
				}
				if (condition.PeriodMin > condition.PeriodMax)
				{
					condition.PeriodMin = condition.PeriodMax;
					return PartialView(condition);
				}
				if (condition.FreMin > condition.FreMax)
				{
					condition.FreMin = condition.FreMax;
					return PartialView(condition);
				}
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
				if (condition.PriceMin > condition.PriceMax)
				{
					condition.PriceMin = condition.PriceMax;
					return PartialView(condition);
				}
				if (condition.AreaMin > condition.AreaMax)
				{
					condition.AreaMin = condition.AreaMax;
					return PartialView(condition);
				}
				if (condition.TermMin > condition.TermMax)
				{
					condition.TermMin = condition.TermMax;
					return PartialView(condition);
				}
				m_ConditionRepository.UpdateHouseCondition(condition);
			}
			catch (DbUpdateException)
			{
			}
			return PartialView(condition);
		}
	}
}
