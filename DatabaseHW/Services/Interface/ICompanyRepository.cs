using DatabaseHW.Models;

namespace DatabaseHW.Services.Interface
{
	/// <summary>
	/// 公司查询服务接口
	/// </summary>
	public interface ICompanyRepository
	{
		/// <summary>
		/// 根据公司ID获取公司信息
		/// </summary>
		public Company GetCompany(int companyId);
	}
}
