using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using DatabaseHW.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DatabaseHW.Models
{
    /// <summary>
    /// 工作岗位数据类
    /// </summary>
    [Index(nameof(WorkplaceId))]
    public class Job
    {
        public int JobId { get; set; }              // 岗位ID（主键，自增）
        [MaxLength(100)]
        public string Name { get; set; } = null!;   // 岗位名称
        public short SalaryMax { get; set; }        // 实习日薪上限，单位为元，不可小于salaryMin
        public short SalaryMin { get; set; }        // 实习日薪下限，单位为元，不可大于salaryMax
        public byte PeriodMax { get; set; }         // 实习时长上限，单位为月，不可小于periodMin
        public byte PeriodMin { get; set; }         // 实习时长下限，单位为月，不可大于periodMax
        public byte FreMax { get; set; }            // 出勤频率上限，单位日/每周，不可小于freMin
        public byte FreMin { get; set; }            // 出勤频率下限，单位日/每周，不可大于freMax
        [EnumDataType(typeof(JobType))]
        public short Type { get; set; }             // 岗位分类（枚举值）
        [EnumDataType(typeof(AcademicType))]
        public byte Academic { get; set; }          // 学历最低要求（枚举值）
        [MaxLength(2000)]
        public string Description { get; set; } = null!; // 岗位描述
        [Url]
        [MaxLength(200)]
        public string? DetailUrl { get; set; }      // 外部信息页面URL，可空
        public int WorkplaceId { get; set; }        // 所属工作地点，参照Workplace表的workplaceId
        public Workplace Workplace { get; set; } = null!; // 所属工作地点
        public int CompanyId { get; set; }          // 所属公司，参照Company表的companyId
        public Company Company { get; set; } = null!; // 所属公司
    }

}
