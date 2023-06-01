using DatabaseHW.Models;

namespace DatabaseHW.Services.Interface
{
    /// <summary>
    /// 筛选条件操作接口
    /// </summary>
    public interface IConditionRepository
    {
        /// <summary>
        /// 获取某个账号的出租房筛选信息
        /// </summary>
        public HouseCondition GetHouseCondition(int accountId);
        /// <summary>
        /// 更新一条出租房筛选信息
        /// </summary>
        public void UpdateHouseCondition(HouseCondition condition);

        /// <summary>
        /// 获取某个账号的岗位筛选信息
        /// </summary>
        public JobCondition GetJobCondition(int accountId);
        /// <summary>
        /// 更新一条岗位筛选信息
        /// </summary>
        public void UpdateJobCondition(JobCondition condition);
    }
}
