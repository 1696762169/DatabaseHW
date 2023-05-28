namespace DatabaseHW.Models
{
    /// <summary>
    /// 公司数据类
    /// </summary>
    public class Company
    {
        public int companyId;           // 公司ID（主键，自增）
        public string name;             // 公司名称
        public byte scale;              // 公司人员规模（枚举值）
        public byte financing;          // 公司融资情况（枚举值）
        public string introduction;     // 公司介绍
    }
}
