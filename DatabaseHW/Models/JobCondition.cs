namespace DatabaseHW.Models
{
    /// <summary>
    /// 岗位筛选条件数据表
    /// </summary>
    public class JobCondition
    {
        public int jobConId;            // 筛选条件ID（主键，自增）
        public float salaryMax;         // 实习日薪上限，单位为元，不可小于salaryMin
        public float salaryMin;         // 实习日薪下限，单位为元，不可大于salaryMax
        public byte periodMax;          // 实习时长上限，单位为月，不可小于periodMin
        public byte periodMin;          // 实习时长下限，单位为月，不可大于periodMax
        public byte freMax;             // 出勤频率上限，单位日/每周，不可小于freMin
        public byte freMin;             // 出勤频率下限，单位日/每周，不可大于freMax
        public short type;              // 岗位分类（枚举值）
        public byte academic;           // 学历最低要求（枚举值）
    }

}
