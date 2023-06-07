using DatabaseHW.Data;
using DatabaseHW.Models;

namespace DatabaseHW.Services
{
	/// <summary>
	/// 公司查询服务
	/// </summary>
	public class CompanyRepository : Interface.ICompanyRepository
	{
		private readonly DataContext m_Context;

		public CompanyRepository(Data.DataContext context)
		{
			m_Context = context;
		}
		public Company GetCompany(int companyId)
		{
			return m_Context.Companies.Find(companyId)
			       ?? throw new ArgumentException($"ID为 {companyId} 的公司不存在");
		}
	}
}
