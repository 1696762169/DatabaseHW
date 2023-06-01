using DatabaseHW.Models;

namespace DatabaseHW.Services
{
    /// <summary>
    /// 筛选条件操作类
    /// </summary>
    public class ConditionRepository : Interface.IConditionRepository
    {
        public HouseCondition GetHouseCondition(int accountId)
        {
            throw new NotImplementedException();
        }
        public void UpdateHouseCondition(HouseCondition condition)
        {
            throw new NotImplementedException();
        }

        public JobCondition GetJobCondition(int accountId)
        {
            throw new NotImplementedException();
        }
        public void UpdateJobCondition(JobCondition condition)
        {
            throw new NotImplementedException();
        }
    }
}
