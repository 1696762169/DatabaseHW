using DatabaseHW.Data;
using DatabaseHW.Models;
using DatabaseHW.Models.Interface;
using DatabaseHW.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace DatabaseHW.Services
{
    /// <summary>
    /// 一级数据筛选器
    /// </summary>
    public class PrimaryFilter : IWorkplaceFilter, ICommunityFilter
    {
        private readonly DataContext m_Context;
        public PrimaryFilter(DataContext context)
        {
            m_Context = context;
        }

        #region “公共”接口
        List<Workplace> IWorkplaceFilter.FilterByDistance(float longitude, float latitude, float distance) => 
            Filter(m_Context.Workplaces, workplace => ByDistance(longitude, latitude, distance, workplace));
        List<Workplace> IWorkplaceFilter.FilterByKeyword(string keyword) =>
            Filter(m_Context.Workplaces, workplace => ByKeyword(keyword, workplace));
        List<Workplace> IWorkplaceFilter.Filter(string keyword, float longitude, float latitude, float distance) =>
            Filter(m_Context.Workplaces, workplace => ByKeyword(keyword, workplace) && ByDistance(longitude, latitude, distance, workplace));

        List<Community> ICommunityFilter.FilterByDistance(float longitude, float latitude, float distance) =>
            Filter(m_Context.Communities, community => ByDistance(longitude, latitude, distance, community));
        List<Community> ICommunityFilter.FilterByKeyword(string keyword) =>
            Filter(m_Context.Communities, community => ByKeyword(keyword, community));
        List<Community> ICommunityFilter.Filter(string keyword, float longitude, float latitude, float distance) =>
            Filter(m_Context.Communities, community => ByKeyword(keyword, community) && ByDistance(longitude, latitude, distance, community));
        #endregion

        // 筛选筛选数据并返回
        private List<T> Filter<T>(DbSet<T> set, Func<T, bool> filter) where T : class, ILocation => set.Where(filter).ToList();

        // 按关键字筛选
        private bool ByKeyword(string keyword, ILocation location)
        {
            return location.Name.Contains(keyword) || location.Address.Contains(keyword);
        }

        // 按距离筛选
        private bool ByDistance(float longitude, float latitude, float distance, ILocation location)
        {
            return Math.Sqrt(Math.Pow(longitude - location.Longitude, 2) + Math.Pow(latitude - location.Latitude, 2)) <= distance;
        }
    }
}
