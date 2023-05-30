using System.Drawing;
using System.Reflection;
using Bogus;
using DatabaseHW.Data;
using DatabaseHW.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseHW.Services.Development
{
    /// <summary>
    /// 数据创建器类（仅供测试用）
    /// </summary>
    public class DataFaker : Interface.IDataGenerator
    {
        // 创建一个数据构造器
        private readonly Faker m_Faker = new("zh_CN");
        // 数据上下文
        private readonly DataContext m_Context;

        public DataFaker(DataContext context)
        {
            m_Context = context;
        }

        public void GenerateData<T>(int count) where T : class, new()
        {
            if (typeof(T) == typeof(Workplace))
                m_Context.Workplaces.AddRange(m_Faker.Make(count, FakeWorkPlace));
            else if (typeof(T) == typeof(Community))
                m_Context.Communities.AddRange(m_Faker.Make(count, FakeCommunity));
            else if (typeof(T) == typeof(House))
                m_Context.Houses.AddRange(m_Faker.Make(count, FakeHouse));
            else if (typeof(T) == typeof(Job))
                m_Context.Jobs.AddRange(m_Faker.Make(count, FakeJob));
            else if (typeof(T) == typeof(Company))
                m_Context.Companies.AddRange(m_Faker.Make(count, FakeCompany));
            m_Context.SaveChanges();
            Console.WriteLine($"创建了 {count} 条 {typeof(T).Name} 数据");
        }

        public List<T> GetAllData<T>(int count = -1) where T : class
        {
            Console.WriteLine($"获取了 {count} 条 {typeof(T).Name} 数据");
            return count < 0 ? m_Context.Set<T>().ToList() : m_Context.Set<T>().Take(count).ToList();
        }

        public void RemoveAllData<T>() where T : class
        {
            m_Context.Set<T>().RemoveRange(m_Context.Set<T>());
            m_Context.SaveChanges();
            Console.WriteLine($"删除了 {typeof(T).Name} 数据");
        }

        private Workplace FakeWorkPlace()
        {
            throw new NotImplementedException();
        }
        private Community FakeCommunity()
        {
            throw new NotImplementedException();
        }
        private House FakeHouse()
        {
            throw new NotImplementedException();
        }
        private Job FakeJob()
        {
            throw new NotImplementedException();
        }
        private Company FakeCompany()
        {
            throw new NotImplementedException();
        }
    }
}
