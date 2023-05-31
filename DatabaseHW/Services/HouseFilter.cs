using DatabaseHW.Data;
using DatabaseHW.Models;

namespace DatabaseHW.Services
{
    /// <summary>
    /// 出租房筛选器
    /// </summary>
    public class HouseFilter :Interface.IHouseFilter
    {
        private readonly DataContext m_Context;
        public HouseFilter(DataContext context)
        {
            m_Context = context;
        }

        public List<House> Filter(Community community, HouseCondition condition)
        {
            throw new NotImplementedException();
        }
    }
}
