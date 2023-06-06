using Bogus;
using Bogus.DataSets;

namespace DatabaseHW.Services.Development
{
	public static class ChineseNameExtensions
	{
		private static readonly Faker _Faker = new("zh_CN");

		private static readonly string[] _OfficePrefixes = { "世纪", "东方", "中心", "华府", "星光", "金色", "蓝天", "阳光", "绿洲" };
		private static readonly string[] _OfficeSuffixes = { "大厦", "中心", "东座", "南栋", "北楼", "西塔" };

		private static readonly string[] _CommunityPrefixes = { "锦绣", "城市", "丽景", "山水", "宝山", "和谐", "紫金", "幸福" };
		private static readonly string[] _CommunitySuffixes = { "小区", "花苑", "公寓", "置地", "家园" };

		private static readonly string[] _CompanyPrefixes = { "盛世", "众信", "瑞泽", "恒盛", "光华", "鼎盛", "荣耀", "金谷", "卓越", "新领" };
		private static readonly string[] _CompanySuffixes = { "科技", "网络", "集团", "有限公司", "电子商务", "信息技术", "通信", "软件" };

		public static string WorkplaceName(this Name name)
		{
			string prefix = _Faker.PickRandom(_OfficePrefixes);
			string suffix = _Faker.PickRandom(_OfficeSuffixes);

			return $"{prefix}{suffix}";
		}

		public static string CommunityName(this Name name)
		{
			string prefix = _Faker.PickRandom(_CommunityPrefixes);
			string suffix = _Faker.PickRandom(_CommunitySuffixes);

			return $"{prefix}{suffix}";
		}

		public static string CompanyName(this Name name)
		{
			string prefix = _Faker.PickRandom(_CompanyPrefixes);
			string suffix = _Faker.PickRandom(_CompanySuffixes);

			return $"{prefix}{suffix}";
		}
	}
}
