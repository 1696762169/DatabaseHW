namespace DatabaseHW.Models
{
    /// <summary>
    /// 租房筛选条件数据表
    /// </summary>
    public class HouseCondition
    {
        public int houseConId;          // 筛选条件ID（主键，自增）
        public float priceMax;          // 月租价格最大值，单位为元，不可小于priceMin
        public float priceMin;          // 月租价格最小值，单位为元，不可大于priceMax
        public short termMax;           // 租期上限，单位为月，不可小于termMin
        public short termMin;           // 租期下限，单位为月，不可大于termMax
        public float areaMax;           // 面积最大值，单位为平方米，不可小于areaMin
        public float areaMin;           // 面积最小值，单位为平方米，不可大于areaMax
        public byte entire;             // 是否接受整租，不可与share同为0
        public byte share;              // 是否接受合租，不可与entire同为0
    }

}
