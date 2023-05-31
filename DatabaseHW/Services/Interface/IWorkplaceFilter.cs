using DatabaseHW.Models;

namespace DatabaseHW.Services.Interface
{
    /// <summary>
    /// 工作地点筛选器接口
    /// </summary>
    public interface IWorkplaceFilter
    {
        /// <summary>
        /// 根据地理位置筛选工作地点
        /// </summary>
        public List<Workplace> FilterByDistance(float longitude, float latitude, float distance);
        /// <summary>
        /// 根据查询关键字筛选工作地点
        /// </summary>
        public List<Workplace> FilterByKeyword(string keyword);
        /// <summary>
        /// 同时根据地理位置与查询关键字筛选工作地点
        /// </summary>
        public List<Workplace> Filter(string keyword, float longitude, float latitude, float distance);
    }
}
