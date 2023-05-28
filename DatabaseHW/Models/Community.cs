namespace DatabaseHW.Models
{
    /// <summary>
    /// 小区数据类
    /// </summary>
    public class Community
    {
        public int communityId;      // 小区ID（主键，自增）
        public string name;          // 小区名称
        public string address;       // 小区地址
        public float longitude;     // 经度
        public float latitude;      // 纬度
    }
}
