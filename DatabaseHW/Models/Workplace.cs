namespace DatabaseHW.Models
{
    /// <summary>
    /// 工作地点数据类
    /// </summary>
    public class Workplace
    {
        public int workplaceId;     // 工作地点ID（主键，自增）
        public string name;         // 工作地点名称
        public string address;      // 工作地点地址
        public float longitude;     // 经度
        public float latitude;      // 纬度
    }
}
