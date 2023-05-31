using System.Collections;
using DatabaseHW.Models;
namespace DatabaseHW.ViewModels.Development
{
    /// <summary>
    /// 数据批量管理使用的视图模型
    /// </summary>
    public class DataFakerViewModel
    {
        public string SelectedType { get; set; } = nameof(Workplace);
        public int GenerateCount { get; set; }
        public int GetCount { get; set; }
        public IList? GetResult { get; set; }

        public static List<string> Types { get; set; } = new()
        {
            nameof(Workplace),
            nameof(Community),
            nameof(House),
            nameof(Job),
            nameof(Company),
        };
    }
}
