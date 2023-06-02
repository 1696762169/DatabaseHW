using DatabaseHW.Models;

namespace DatabaseHW.ViewModels
{
    /// <summary>
    /// 一级搜索条件视图模型
    /// </summary>
    public class PrimarySearchViewModel
    {
        // 查询类型
        public string Type { get; set; } = nameof(Workplace);

        public string? Key { get; set; }    // 关键词

        public float Longitude { get; set; } = float.MaxValue;    // 经度
        public float Latitude { get; set; } = float.MaxValue;     // 纬度
		public float Range { get; set; } = 1;    // 筛选范围
	}
}
