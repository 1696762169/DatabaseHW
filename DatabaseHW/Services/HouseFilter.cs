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
            // 首先查找小区中包含的出租房 排除大部分出租房
            IEnumerable<House> temp = m_Context.Houses.Where(house => house.CommunityId == community.CommunityId);
            if (condition.PriceMax > 0)   // 按最高月租价格筛选
                temp = temp.Where(house => house.Price <= condition.PriceMax);
            if (condition.PriceMin > 0)    // 按最低月租价格筛选
                temp = temp.Where(house => house.Price >= condition.PriceMin);

            if (condition.AreaMax > 0)    // 按最大面积筛选
                temp = temp.Where(house => house.Area <= condition.AreaMax);
            if (condition.AreaMin > 0) // 按最小面积筛选
                temp = temp.Where(house => house.Area >= condition.AreaMin);

            if (condition.TermMax != short.MaxValue)   // 按最长租期筛选
                temp = temp.Where(house => house.TermMin <= condition.TermMax);
            if (condition.TermMin != 0) // 按最短租期筛选
                temp = temp.Where(house => house.TermMax >= condition.TermMin);

            if (condition.Entire != byte.MaxValue)  // 按是否整租筛选
                temp = temp.Where(house => house.Entire == condition.Entire);
            if (condition.Share != byte.MaxValue)   // 按是否合租筛选
                temp = temp.Where(house => house.Share == condition.Share);

            return temp.ToList();
        }
    }
}
