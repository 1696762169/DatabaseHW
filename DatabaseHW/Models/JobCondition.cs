using System.ComponentModel.DataAnnotations;
using DatabaseHW.ViewModels;

namespace DatabaseHW.Models
{
    /// <summary>
    /// 岗位筛选条件数据表
    /// </summary>
    public class JobCondition
    {
        [Key]
        public int JobConId { get; set; }            // 筛选条件ID（主键，自增）
        public Account Account { get; set; } = null!; // 导航属性，指向所属账户
        public short SalaryMax { get; set; }         // 实习日薪上限，单位为元，不可小于salaryMin
        public short SalaryMin { get; set; }         // 实习日薪下限，单位为元，不可大于salaryMax
        public byte PeriodMax { get; set; }          // 实习时长上限，单位为月，不可小于periodMin
        public byte PeriodMin { get; set; }          // 实习时长下限，单位为月，不可大于periodMax
        public byte FreMax { get; set; }             // 出勤频率上限，单位日/每周，不可小于freMin
        public byte FreMin { get; set; }             // 出勤频率下限，单位日/每周，不可大于freMax
        [EnumDataType(typeof(JobType))]
        public short Type { get; set; }              // 岗位分类（枚举值）
        [EnumDataType(typeof(AcademicType))]
        public byte Academic { get; set; }           // 学历最低要求（枚举值）
    }

}
