using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseHW.Models
{
    /// <summary>
    /// 出租房数据类
    /// </summary>
    [Index(nameof(CommunityId))]
    public class House
    {
        public int HouseId { get; set; }             // 出租房ID（主键，自增）
        [MaxLength(100)]
        public string Title { get; set; } = null!;   // 租房标题
        [Precision(8, 2)]
        public float Price { get; set; }             // 月租价格，单位为元
        [Precision(6, 2)]
        public float Area { get; set; }              // 房屋面积，单位为平方米
        public short TermMax { get; set; }           // 租期上限，单位为月，不可小于termMin
        public short TermMin { get; set; }           // 租期下限，单位为月，不可大于termMax
        public byte Entire { get; set; }             // 是否可整租，不可与share同为0
        public byte Share { get; set; }              // 是否可合租，不可与entire同为0
        [Url]
        [MaxLength(200)]
        public string? DetailUrl { get; set; }       // 外部信息页面URL，可空
        public int CommunityId { get; set; }         // 所属小区，参照Community表的communityId
        public Community Community { get; set; } = null!; // 导航属性，指向所属小区
    }

}
