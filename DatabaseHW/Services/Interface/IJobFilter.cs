using DatabaseHW.Models;

namespace DatabaseHW.Services.Interface
{
    /// <summary>
    /// 岗位筛选接口
    /// </summary>
    public interface IJobFilter
    {
        /// <summary>
        /// 根据岗位筛选条件筛选出岗位
        /// </summary>
        /// <param name="workplace">岗位所属地点</param>
        /// <param name="condition">岗位筛选条件</param>
        public List<Job> Filter(Workplace workplace, JobCondition condition);
    }
}
