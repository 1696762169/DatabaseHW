using DatabaseHW.Data;
using DatabaseHW.Models;

namespace DatabaseHW.Services
{
    /// <summary>
    /// 岗位筛选器
    /// </summary>
    public class JobFilter : Interface.IJobFilter
    {
        private readonly DataContext m_Context;
        public JobFilter(DataContext context)
        {
            m_Context = context;
        }

        public List<Job> Filter(int workplaceId, JobCondition condition)
        {
            // 首先查找工作地点中包含的岗位 排除大部分岗位
            IEnumerable<Job> temp =  m_Context.Jobs.Where(job => job.WorkplaceId == workplaceId);
            if (condition.Type != 0)    // 按岗位类型筛选
                temp = temp.Where(job => job.Type == condition.Type);
            if (condition.Academic != 0)    // 按学历要求筛选
                temp = temp.Where(job => job.Academic == condition.Academic);

            if (condition.SalaryMax != short.MaxValue)    // 按最高薪资筛选
                temp = temp.Where(job => job.SalaryMin <= condition.SalaryMax);
            if (condition.SalaryMin != 0)   // 按最低薪资筛选
                temp = temp.Where(job => job.SalaryMax >= condition.SalaryMin);

            if (condition.PeriodMax != byte.MaxValue)   // 按最长实习时间筛选
                temp = temp.Where(job => job.PeriodMin <= condition.PeriodMax);
            if (condition.PeriodMin != 0)   // 按最短实习时间筛选
                temp = temp.Where(job => job.PeriodMax >= condition.PeriodMin);

            if (condition.FreMax != byte.MaxValue)  // 按最高出勤频率筛选
                temp = temp.Where(job => job.FreMin <= condition.FreMax);
            if (condition.FreMin != 0)  // 按最低出勤频率筛选
                temp = temp.Where(job => job.FreMax >= condition.FreMin);
            return temp.ToList();
        }
    }
}
