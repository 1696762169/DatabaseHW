using DatabaseHW.Data;
using DatabaseHW.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseHW.Services
{
    /// <summary>
    /// 筛选条件操作类
    /// </summary>
    public class ConditionRepository : Interface.IConditionRepository
    {
        private readonly DataContext m_Context;
        public ConditionRepository(DataContext context)
        {
            m_Context = context;
        }

        public HouseCondition GetHouseCondition(int accountId)
        {
            Account account = m_Context.Accounts.Find()
                              ?? throw new ArgumentException($"ID为 {accountId} 的用户不存在");
            return m_Context.HouseConditions.Find(account.HouseConId)
                   ?? throw new ArgumentException($"ID为 {accountId} 的用户没有出租房筛选条件");
        }
        public void UpdateHouseCondition(HouseCondition condition)
        {
            m_Context.HouseConditions.Update(condition);
            m_Context.SaveChanges();
        }

        public JobCondition GetJobCondition(int accountId)
        {
            Account account = m_Context.Accounts.Find()
                              ?? throw new ArgumentException($"ID为 {accountId} 的用户不存在");
            return m_Context.JobConditions.Find(account.JobConId)
                   ?? throw new ArgumentException($"ID为 {accountId} 的用户没有岗位筛选条件");
        }
        public void UpdateJobCondition(JobCondition condition)
        {
            m_Context.JobConditions.Update(condition);
            m_Context.SaveChanges();
        }
    }
}
