using System.Drawing;
using System.Reflection;
using System.Text;
using Bogus;
using DatabaseHW.Data;
using DatabaseHW.Models;
using DatabaseHW.ViewModels;
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

        // 实体ID中间变量
        private List<int> m_WorkPlaces = new();
        private List<int> m_Communities = new();
        private List<int> m_Companies = new();

        public DataFaker(DataContext context)
        {
            m_Context = context;
        }

        public void GenerateData(string typeName, int count)
        {
            switch (typeName)
            {
            case nameof(Workplace):
                m_Context.Workplaces.AddRange(m_Faker.Make(count, FakeWorkPlace));
                break;
            case nameof(Community):
                m_Context.Communities.AddRange(m_Faker.Make(count, FakeCommunity));
                break;
            case nameof(House):
                m_Communities = m_Context.Communities.Select(c => c.CommunityId).ToList();
                m_Context.Houses.AddRange(m_Faker.Make(count, FakeHouse));
                break;
            case nameof(Job):
                m_WorkPlaces = m_Context.Workplaces.Select(w => w.WorkplaceId).ToList();
                m_Companies = m_Context.Companies.Select(c => c.CompanyId).ToList();
                m_Context.Jobs.AddRange(m_Faker.Make(count, FakeJob));
                break;
            case nameof(Company):
                m_Context.Companies.AddRange(m_Faker.Make(count, FakeCompany));
                break;
            }
            m_Context.SaveChanges();
        }

        public List<T> GetData<T>(int count = -1) where T : class
        {
            return count < 0 || count > m_Context.Set<T>().Count() ? m_Context.Set<T>().ToList() : m_Context.Set<T>().Take(count).ToList();
        }

        public void RemoveAllData<T>() where T : class
        {
            m_Context.Set<T>().RemoveRange(m_Context.Set<T>());
            m_Context.SaveChanges();
        }

        private Workplace FakeWorkPlace()
        { 
            return new Workplace
            {
                WorkplaceId = m_Faker.UniqueIndex,
                Name = m_Faker.Address.StreetAddress(),
                Address = m_Faker.Address.FullAddress(),
                Longitude = (float)m_Faker.Address.Longitude(),
                Latitude = (float)m_Faker.Address.Latitude(),
            };
        }
        private Community FakeCommunity()
        {
            return new Community
            {
                CommunityId = m_Faker.UniqueIndex,
                Name = m_Faker.Address.StreetAddress(),
                Address = m_Faker.Address.FullAddress(),
                Longitude = (float)m_Faker.Address.Longitude(),
                Latitude = (float)m_Faker.Address.Latitude(),
            };
        }
        private House FakeHouse()
        {
            House ret = new()
            {
                HouseId = m_Faker.UniqueIndex,
                Title = m_Faker.Lorem.Sentence(5, 3),
                Price = m_Faker.Random.Float(100.0f, 10000.0f),
                Area = m_Faker.Random.Float(10.0f, 1000.0f),
                DetailUrl = m_Faker.Random.Int(0, 2) == 0 ? null : m_Faker.Internet.Url(),
                CommunityId = m_Faker.PickRandom(m_Communities),
                TermMax = m_Faker.Random.Short(3, 100)
            };
            ret.TermMin = (short)Math.Max(1, ret.TermMax - m_Faker.Random.Short(0, 50));
            int temp = m_Faker.Random.Int(1, 3);
            ret.Entire = (byte)(temp & 1);
            ret.Share = (byte)((temp >> 1) & 1);
            return ret;
        }
        private Job FakeJob()
        {
            Job ret = new()
            {
                JobId = m_Faker.UniqueIndex,
                Name = m_Faker.Name.JobTitle(),
                SalaryMax = m_Faker.Random.Short(100, 500),
                PeriodMax = m_Faker.Random.Byte(3, 24),
                FreMax = m_Faker.Random.Byte(2, 5),
                Type = (short)m_Faker.PickRandom<JobType>(),
                Academic = (byte)m_Faker.PickRandom<AcademicType>(),
                Description = m_Faker.Lorem.Paragraph(),
                DetailUrl = m_Faker.Random.Int(0, 2) == 0 ? null : m_Faker.Internet.Url(),
                WorkplaceId = m_Faker.PickRandom(m_WorkPlaces),
                CompanyId = m_Faker.PickRandom(m_Companies),
            };
            ret.SalaryMin = (short)Math.Max(100, ret.SalaryMax - m_Faker.Random.Short(0, 200));
            ret.PeriodMin = (byte)Math.Max(1, ret.PeriodMax - m_Faker.Random.Byte(0, 12));
            ret.FreMin = (byte)Math.Max(1, ret.FreMax - m_Faker.Random.Byte(0, 3));
            return ret;
        }
        private Company FakeCompany()
        {
            return new Company
            {
                CompanyId = m_Faker.UniqueIndex,
                Name = m_Faker.Company.CompanyName(),
                Scale = (byte)m_Faker.PickRandom<ScaleType>(),
                Financing = (byte)m_Faker.PickRandom<FinanceType>(),
                Introduction = m_Faker.Lorem.Paragraph(),
            };
        }
    }
}
