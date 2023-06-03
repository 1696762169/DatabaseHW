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
            Filter(m_Context.Workplaces, workplace => ByDistance(CalculateLocationRange(longitude, latitude, distance), workplace));
        List<Workplace> IWorkplaceFilter.FilterByKeyword(string keyword) =>
            Filter(m_Context.Workplaces, workplace => ByKeyword(keyword, workplace));
        List<Workplace> IWorkplaceFilter.Filter(string keyword, float longitude, float latitude, float distance) =>
            Filter(m_Context.Workplaces, workplace => ByKeyword(keyword, workplace) && ByDistance(CalculateLocationRange(longitude, latitude, distance), workplace));

        List<Community> ICommunityFilter.FilterByDistance(float longitude, float latitude, float distance) =>
            Filter(m_Context.Communities, community => ByDistance(CalculateLocationRange(longitude, latitude, distance), community));
        List<Community> ICommunityFilter.FilterByKeyword(string keyword) =>
            Filter(m_Context.Communities, community => ByKeyword(keyword, community));
        List<Community> ICommunityFilter.Filter(string keyword, float longitude, float latitude, float distance) =>
            Filter(m_Context.Communities, community => ByKeyword(keyword, community) && ByDistance(CalculateLocationRange(longitude, latitude, distance), community));
        #endregion

        // 筛选筛选数据并返回
        private List<T> Filter<T>(DbSet<T> set, Func<T, bool> filter) where T : class, ILocation => set.Where(filter).ToList();

        // 按关键字筛选
        private bool ByKeyword(string keyword, ILocation location)
        {
            return location.Name.Contains(keyword) || location.Address.Contains(keyword);
        }

        // 按距离筛选
        private bool ByDistance((float minLat, float maxLat, float minLon, float maxLon) range, ILocation location)
        {
            return range.minLat <= location.Latitude && 
                   range.minLon <= location.Longitude && 
                   range.maxLat >= location.Latitude && 
                   range.maxLon >= location.Longitude;
        }

        private static (float minLatitude, float maxLatitude, float minLongitude, float maxLongitude) CalculateLocationRange(float longitude, float latitude, float radiusInKilometers)
        {
	        var (latitudeDiff, longitudeDiff) = ConvertKilometersToLatLng(radiusInKilometers, latitude);

	        float minLatitude = latitude - latitudeDiff;
	        float maxLatitude = latitude + latitudeDiff;
	        float minLongitude = longitude - longitudeDiff;
	        float maxLongitude = longitude + longitudeDiff;

	        return (minLatitude, maxLatitude, minLongitude, maxLongitude);

	        static (float latitudeDiff, float longitudeDiff) ConvertKilometersToLatLng(float kilometers, float latitude)
	        {
		        const float KILOMETERS_PER_DEGREE = 111.32f;
				float latitudeDiff = kilometers / KILOMETERS_PER_DEGREE;
		        float longitudeDiff = kilometers / (KILOMETERS_PER_DEGREE * MathF.Cos(latitude * MathF.PI / 180));

		        return (latitudeDiff, longitudeDiff);
	        }
		}
	}
}
