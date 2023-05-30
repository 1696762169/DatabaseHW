using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DatabaseHW.Models
{
    /// <summary>
    /// 历史记录数据类
    /// </summary>
    public class Record
    {
        public int RecordId { get; set; }            // 记录ID（主键，自增）
        [MaxLength(200)]
        public string Key { get; set; } = null!;     // 查询关键字
        [Precision(10, 6)]
        public float Longitude { get; set; }         // 经度
        [Precision(10, 6)]
        public float Latitude { get; set; }          // 纬度
        public DateTime ApplyTime { get; set; }      // 生成时间
        public int AccountId { get; set; }           // 所属用户，参照Account表的accountId
        public Account Account { get; set; } = null!;// 导航属性，指向所属用户
        public byte Range { get; set; }              // 查询位置附近最大距离，单位为千米
    }

}
