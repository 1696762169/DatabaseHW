namespace DatabaseHW.ViewModels
{
    /// <summary>
    /// 公司人员规模枚举
    /// </summary>
    public enum ScaleType
    {
        Small = 0,          // 初创/小型公司，100人以下
        Middle = 1,         // 中等规模公司，100~999人
        Large = 2,          // 大型公司，1000~9999人
        Huge = 3            // 行业巨头，10000人以上
    }
}
