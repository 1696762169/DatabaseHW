using DatabaseHW.Models;
using DatabaseHW.Services.Interface;

namespace DatabaseHW.Services
{
    /// <summary>
    /// 一级数据筛选器
    /// </summary>
    public class PrimaryFilter : IWorkplaceFilter, ICommunityFilter
    {
        List<Workplace> IWorkplaceFilter.FilterByDistance(float longitude, float latitude, float distance)
        {
            throw new NotImplementedException();
        }

        List<Community> ICommunityFilter.FilterByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }

        List<Community> ICommunityFilter.Filter(string keyword, float longitude, float latitude, float distance)
        {
            throw new NotImplementedException();
        }

        List<Community> ICommunityFilter.FilterByDistance(float longitude, float latitude, float distance)
        {
            throw new NotImplementedException();
        }

        List<Workplace> IWorkplaceFilter.FilterByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }

        List<Workplace> IWorkplaceFilter.Filter(string keyword, float longitude, float latitude, float distance)
        {
            throw new NotImplementedException();
        }
    }
}
