using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DatabaseHW.Models
{
    /// <summary>
    /// 租房筛选条件数据表
    /// </summary>
    public class HouseCondition
    {
        [Key]
        public int HouseConId { get; set; }          // 筛选条件ID（主键，自增）
        public Account? Account { get; set; } // 导航属性，指向所属账户
        [Precision(8, 2)]
        public float PriceMax { get; set; }          // 月租价格最大值，单位为元，不可小于priceMin
        [Precision(8, 2)]
        public float PriceMin { get; set; }          // 月租价格最小值，单位为元，不可大于priceMax
        public short TermMax { get; set; }           // 租期上限，单位为月，不可小于termMin
        public short TermMin { get; set; }           // 租期下限，单位为月，不可大于termMax
        [Precision(6, 2)]
        public float AreaMax { get; set; }           // 面积最大值，单位为平方米，不可小于areaMin
        [Precision(6, 2)]
        public float AreaMin { get; set; }           // 面积最小值，单位为平方米，不可大于areaMax
        public byte Entire { get; set; }             // 是否接受整租，不可与share同为0
        public byte Share { get; set; }              // 是否接受合租，不可与entire同为0

        /// <summary>
        /// 生成一个空的筛选条件 不进行筛选
        /// </summary>
        public static HouseCondition Empty()
        {
            return new HouseCondition
            {
                PriceMax = -1,
                PriceMin = -1,
                TermMax = short.MaxValue,
                TermMin = 0,
                AreaMax = -1,
                AreaMin = -1,
                Entire = byte.MaxValue,
                Share = byte.MaxValue,
            };
        }
    }
}
