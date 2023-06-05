using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DatabaseHW.Models
{
    /// <summary>
    /// 用户信息数据表
    /// </summary>
    [Index(nameof(Username), IsUnique = true)]
    public class Account
    {
        public int AccountId { get; set; }           // 用户ID（主键，自增）
        [MaxLength(50)]
        public string Username { get; set; } = null!; // 用户名，唯一
        [MaxLength(50)]
        public string Password { get; set; } = null!; // 密码
        public int HouseConId { get; set; }          // 租房筛选条件，参照HouseCondition表的houseConId
        public HouseCondition HouseCon { get; set; } = null!; // 租房筛选条件
        public int JobConId { get; set; }            // 岗位筛选条件，参照JobCondition表的jobConId
        public JobCondition JobCon { get; set; } = null!; // 岗位筛选条件
        public List<Record> Records { get; set; } = new(); // 导航属性，指向历史记录

        public const int ONLY_ONE = 1;
    }

}
