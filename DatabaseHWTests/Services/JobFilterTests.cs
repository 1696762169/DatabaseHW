using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseHW.Data;
using DatabaseHW.Models;
using Bogus;

namespace DatabaseHW.Services.Tests
{
    [TestClass]
    public class JobFilterTests : DatabaseHWTests.Startup
    {
        [TestMethod]
        public void FilterTest()
        {
            // 准备测试数据
            var workplace = new Faker().PickRandom(context.Workplaces.ToList());
            var condition = new JobCondition { Type = 1, Academic = 2 };

            // 创建 JobFilter 实例，并调用 Filter 方法
            JobFilter jobFilter = new(context);
            List<Job> jobs = jobFilter.Filter(workplace, condition);
        }
    }
}