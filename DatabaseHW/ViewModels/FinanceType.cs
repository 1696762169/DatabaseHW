namespace DatabaseHW.ViewModels
{
    /// <summary>
    /// 公司融资情况枚举
    /// </summary>
    public enum FinanceType
    {
        NotFinancing = 0,               // 公司未获得融资
        AngelRound = 1,                 // 公司处于天使轮融资阶段
        ARound = 2,                     // 公司处于A轮融资阶段
        BRound = 3,                     // 公司处于B轮融资阶段
        CRound = 4,                     // 公司处于C轮融资阶段
        DRoundAndAbove = 5,             // 公司处于D轮及以上融资阶段
        ListedCompany = 6               // 上市公司
    }
}
