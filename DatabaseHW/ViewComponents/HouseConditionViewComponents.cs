using DatabaseHW.Models;
using DatabaseHW.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseHW.ViewComponents
{
	public class HouseConditionViewComponents : ViewComponent
	{
		private readonly IConditionRepository m_ConditionRepository;

		public HouseConditionViewComponents(IConditionRepository conditionRepository)
		{
			m_ConditionRepository = conditionRepository;
		}

		public IViewComponentResult Invoke()
		{
			return View(m_ConditionRepository.GetHouseCondition(Account.ONLY_ONE));
		}
	}
}
