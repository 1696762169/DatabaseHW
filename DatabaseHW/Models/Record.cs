namespace DatabaseHW.Models
{
    /// <summary>
    /// 历史记录数据表
    /// </summary>
    public class Record
    {
        public int recordId;            // 记录ID（主键，自增）
        public string key;              // 查询关键字
        public float longitude;         // 经度
        public float latitude;          // 纬度
        public byte range;              // 查询位置附近最大距离，单位为千米
        public DateTime applyTime;      // 生成时间
        public int accountId;           // 所属用户，参照Account表的accountId
    }

}
