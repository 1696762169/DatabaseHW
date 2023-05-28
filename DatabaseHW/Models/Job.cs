namespace DatabaseHW.Models
{
    /// <summary>
    /// 岗位数据类
    /// </summary>
    public class Job
    {
        public int jobId;               // 岗位ID（主键，自增）
        public string name;             // 岗位名称
        public short salaryMax;         // 实习日薪上限，单位为元，不可小于salaryMin
        public short salaryMin;         // 实习日薪下限，单位为元，不可大于salaryMax
        public byte periodMax;          // 实习时长上限，单位为月，不可小于periodMin
        public byte periodMin;          // 实习时长下限，单位为月，不可大于periodMax
        public byte freMax;             // 出勤频率上限，单位日/每周，不可小于freMin
        public byte freMin;             // 出勤频率下限，单位日/每周，不可大于freMax
        public short type;              // 岗位分类（枚举值）
        public byte academic;           // 学历最低要求（枚举值）
        public string description;      // 岗位描述
        public string? detailUrl;       // 外部信息页面URL，可空
        public int workplaceId;         // 所属工作地点，参照Workplace表的workplaceId
        public int companyId;           // 所属公司，参照Company表的companyId
    }

}
