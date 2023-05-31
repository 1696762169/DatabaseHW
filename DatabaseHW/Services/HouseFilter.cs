using DatabaseHW.Models;

namespace DatabaseHW.Services
{
    /// <summary>
    /// 出租房筛选器
    /// </summary>
    public class HouseFilter :Interface.IHouseFilter
    {
        public List<House> Filter(IEnumerable<Community> communities, HouseCondition condition)
        {
            throw new NotImplementedException();
        }
    }
}
