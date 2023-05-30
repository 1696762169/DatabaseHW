using System.ComponentModel.DataAnnotations;
using System.Reflection;
using DatabaseHW.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseHW.Data
{
    /// <summary>
    /// 将实体类映射到数据库中的表
    /// </summary>
    public class DataContext : DbContext
    {
        public DbSet<Workplace> Workplaces { get; set; } = null!; // 工作地点数据表
        public DbSet<Community> Communities { get; set; } = null!; // 小区数据表

        public DbSet<Job> Jobs { get; set; } = null!; // 岗位数据表
        public DbSet<Company> Companies { get; set; } = null!; // 公司数据表
        public DbSet<House> Houses { get; set; } = null!; // 房屋数据表

        public DbSet<Account> Accounts { get; set; } = null!; // 用户信息数据表
        public DbSet<HouseCondition> HouseConditions { get; set; } = null!; // 租房筛选条件数据表
        public DbSet<JobCondition> JobConditions { get; set; } = null!; // 岗位筛选条件数据表
        public DbSet<Record> Records { get; set; } = null!; // 历史记录数据表

        // 处理传入的数据库连接参数
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 遍历所有 DbSet 的类型
            foreach (PropertyInfo dbSetProperty in GetType().GetProperties())
            {
                if (!dbSetProperty.PropertyType.IsGenericType ||
                    dbSetProperty.PropertyType.GetGenericTypeDefinition() != typeof(DbSet<>))
                    continue;
                Type entityType = dbSetProperty.PropertyType.GetGenericArguments().First();
                // 遍历所有属性
                foreach (PropertyInfo entityProperty in entityType.GetProperties())
                {
                    SetNonUnicode(entityType, entityProperty, modelBuilder);
                    SetCompareConstraint(entityType, entityProperty, modelBuilder);
                    SetEnumConstraint(entityType, entityProperty, modelBuilder);
                    SetFloat(entityType, entityProperty, modelBuilder);
                }
            }

            // 约束整租合租
            modelBuilder.Entity<House>().ToTable(t =>
            {
                t.HasCheckConstraint("CK_House_ES", $"[{nameof(House.Entire)}] <> 0 OR [{nameof(House.Share)}] <> 0");
            });
            modelBuilder.Entity<HouseCondition>().ToTable(t =>
            {
                t.HasCheckConstraint("CK_HouseCondition_ES", $"[{nameof(HouseCondition.Entire)}] <> 0 OR [{nameof(HouseCondition.Share)}] <> 0");
            });
        }

        // 将字符串属性设置为非Unicode编码
        private void SetNonUnicode(Type entityType, PropertyInfo entityProperty, ModelBuilder modelBuilder)
        {
            if (entityProperty.PropertyType != typeof(string))
                return;
            modelBuilder.Entity(entityType).Property(entityProperty.Name).IsUnicode(false);
        }

        // 设置比较大小约束
        private void SetCompareConstraint(Type entityType, PropertyInfo entityProperty, ModelBuilder modelBuilder)
        {
            if (!entityProperty.Name.EndsWith("Max"))
                return;
            // 获取待比较属性名称
            string compareName = entityProperty.Name[..^3] + "Min";
            // 检查待比较属性是否存在
            if (entityType.GetProperty(compareName) is null)
                return;
            // 设置比较大小约束
            modelBuilder.Entity(entityType).ToTable(t =>
                t.HasCheckConstraint($"CK_{entityType.Name}_{entityProperty.Name}", $"[{entityProperty.Name}] > [{compareName}]"));
        }

        // 设置枚举约束
        private void SetEnumConstraint(Type entityType, PropertyInfo entityProperty, ModelBuilder modelBuilder)
        {
            // 检查是否为枚举类型
            if (entityProperty.GetCustomAttribute<EnumDataTypeAttribute>() is not { } enumDataType)
                return;
            // 设置枚举约束
            if (Enum.GetValuesAsUnderlyingType(enumDataType.EnumType) is not int[] values)
                return;
            modelBuilder.Entity(entityType).ToTable(t =>
                t.HasCheckConstraint($"CK_{entityType.Name}_{entityProperty.Name}", $"[{entityProperty.Name}] IN ({string.Join(',', values)})"));
        }

        // 设置浮点数类型
        private void SetFloat(Type entityType, PropertyInfo entityProperty, ModelBuilder modelBuilder)
        {
            if (entityProperty.PropertyType != typeof(float))
                return;
            if (entityProperty.GetCustomAttribute<PrecisionAttribute>() is { } precision)
                modelBuilder.Entity(entityType).Property(entityProperty.Name).HasColumnType($"decimal({precision.Precision},{precision.Scale})");
            else
                modelBuilder.Entity(entityType).Property(entityProperty.Name).HasColumnType("real");
        }

        public void Test()
        {
            ModelBuilder modelBuilder = new();
            foreach (PropertyInfo dbSetProperty in GetType().GetProperties())
            {
                if (!dbSetProperty.PropertyType.IsGenericType ||
                    dbSetProperty.PropertyType.GetGenericTypeDefinition() != typeof(DbSet<>))
                    continue;
                Type entityType = dbSetProperty.PropertyType.GetGenericArguments().First();
                // 遍历所有属性
                foreach (PropertyInfo entityProperty in entityType.GetProperties())
                {
                    SetNonUnicode(entityType, entityProperty, modelBuilder);
                    SetCompareConstraint(entityType, entityProperty, modelBuilder);
                    SetEnumConstraint(entityType, entityProperty, modelBuilder);
                    SetFloat(entityType, entityProperty, modelBuilder);
                }
            }
        }
    }
}
