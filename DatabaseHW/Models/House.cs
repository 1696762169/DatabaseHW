namespace DatabaseHW.Models
{
    /// <summary>
    /// 出租房数据表
    /// </summary>
    public class House
    {
        public int houseId;             // 出租房ID（主键，自增）
        public string title;            // 租房标题
        public float price;             // 月租价格，单位为元
        public float area;              // 房屋面积，单位为平方米
        public short termMax;           // 租期上限，单位为月，不可小于termMin
        public short termMin;           // 租期下限，单位为月，不可大于termMax
        public byte entire;             // 是否可整租，不可与share同为0
        public byte share;              // 是否可合租，不可与entire同为0
        public string? detailUrl;       // 外部信息页面URL，可空
        public int communityId;         // 所属小区，参照Community表的communityId
    }

}
