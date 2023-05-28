public class Community
{
    public int CommunityId { get; set; } // 表示社区编号
    public string CommunityName { get; set; } // 表示社区名称
    public string Address { get; set; } // 表示社区地址
    public int TotalUnits { get; set; } // 表示社区单元总数
    public double MonthlyFee { get; set; } // 表示社区月费
    public List<Resident> Residents { get; set; } // 表示社区中的居民列表

    /// <summary>
    /// Community 类的构造函数。
    /// </summary>
    /// <param name="id">社区的 ID。</param>
    /// <param name="name">社区的名称。</param>
    /// <param name="address">社区的地址。</param>
    /// <param name="units">社区的单元总数。</param>
    /// <param name="fee">社区的月费。</param>
    /// <param name="residents">社区居民列表。</param>
    public Community(int id, string name, string address, int units, double fee, List<Resident> residents)
    {
        CommunityId = id;
        CommunityName = name;
        Address = address;
        TotalUnits = units;
        MonthlyFee = fee;
        Residents = residents;
    }
}
