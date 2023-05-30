using System.ComponentModel.DataAnnotations;
using DatabaseHW.ViewModels;

namespace DatabaseHW.Models
{
    /// <summary>
    /// 公司数据类
    /// </summary>
    public class Company
    {
        public int CompanyId { get; set; }          // 公司ID（主键，自增）
        [MaxLength(100)]
        public string Name { get; set; } = null!; // 公司名称
        [EnumDataType(typeof(ScaleType))]
        public byte Scale { get; set; }             // 公司人员规模（枚举值）
        [EnumDataType(typeof(FinanceType))]
        public byte Financing { get; set; }         // 公司融资情况（枚举值）
        [MaxLength(1000)]
        public string Introduction { get; set; } = null!;    // 公司介绍
        public List<Job> Jobs { get; set; } = new();  // 公司发布的岗位
    }
}
