using Microsoft.EntityFrameworkCore;

namespace DatabaseHW.Data
{
    /// <summary>
    /// 将实体类映射到数据库中的表
    /// </summary>
    public class DataContext : DbContext
    {
        // 处理传入的数据库连接参数
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
