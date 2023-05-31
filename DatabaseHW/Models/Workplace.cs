using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DatabaseHW.Models
{
    /// <summary>
    /// 工作地点数据类
    /// </summary>
    [Index(nameof(Longitude))]
    [Index(nameof(Latitude))]
    public class Workplace : Interface.ILocation
    {
        public int WorkplaceId { get; set; }  // 工作地点ID（主键，自增）
        [MaxLength(50)]
        public string Name { get; set; } = null!; // 工作地点名称
        [MaxLength(200)]
        public string Address { get; set; } = null!;    // 工作地点地址
        
        [Precision(10, 6)]
        public float Longitude { get; set; }  // 经度
        [Precision(10, 6)]
        public float Latitude { get; set; }   // 纬度

        public List<Job> Jobs { get; set; } = new(); // 工作地点包含的岗位
    }
}
