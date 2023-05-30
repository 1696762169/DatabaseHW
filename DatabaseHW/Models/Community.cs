using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseHW.Models
{
    /// <summary>
    /// 小区数据类
    /// </summary>
    [Index(nameof(Longitude))]
    [Index(nameof(Latitude))]
    public class Community
    {
        public int CommunityId { get; set; }    // 小区ID（主键，自增）
        [MaxLength(50)]
        public string Name { get; set; } = null!;        // 小区名称
        [MaxLength(200)]
        public string Address { get; set; } = null!;     // 小区地址

        [Precision(10, 6)]
        public float Longitude { get; set; }    // 经度
        [Precision(10, 6)]
        public float Latitude { get; set; }     // 纬度

        public List<House> Houses { get; set; } = new(); // 导航属性，指向小区内的出租房
    }
}
