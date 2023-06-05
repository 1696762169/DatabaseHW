using DatabaseHW.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseHWTests
{
	[TestClass]
	public class InitAccount : Startup
	{
		[TestMethod]
		public void Init()
		{
			// 先添加默认筛选条件
			//context.JobConditions.Add(JobCondition.Empty());
			//context.HouseConditions.Add(HouseCondition.Empty());
			//context.SaveChanges();

			JobCondition jc = context.JobConditions.First();
			HouseCondition hc = context.HouseConditions.First();
			// 再添加账户
			context.Accounts.Add(new Account
			{
				AccountId = Account.ONLY_ONE,
				Username = "JYX",
				Password = "12345678",
				JobConId = jc.JobConId,
				HouseConId = hc.HouseConId,
			});
			//context.SaveChanges();
		}
	}
}
