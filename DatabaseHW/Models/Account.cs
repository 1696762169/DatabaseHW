namespace DatabaseHW.Models
{
    /// <summary>
    /// 用户信息数据表
    /// </summary>
    public class Account
    {
        public int accountId;           // 用户ID（主键，自增）
        public string username;         // 用户名，唯一
        public string password;         // 密码
        public int houseConId;          // 租房筛选条件，参照HouseCondition表的houseConId
        public int jobConId;            // 岗位筛选条件，参照JobCondition表的jobConId
    }

}
