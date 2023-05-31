using DatabaseHW.Models;

namespace DatabaseHW.Services.Interface
{
    /// <summary>
    /// 小区筛选器接口
    /// </summary>
    public interface ICommunityFilter
    {
        /// <summary>
        /// 根据地理位置筛选小区
        /// </summary>
        public List<Community> FilterByDistance(float longitude, float latitude, float distance);
        /// <summary>
        /// 根据查询关键字筛选小区
        /// </summary>
        public List<Community> FilterByKeyword(string keyword);
        /// <summary>
        /// 同时根据地理位置与查询关键字筛选小区
        /// </summary>
        public List<Community> Filter(string keyword, float longitude, float latitude, float distance);
    }
}
