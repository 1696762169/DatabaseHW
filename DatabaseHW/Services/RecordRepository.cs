using DatabaseHW.Data;
using DatabaseHW.Models;

namespace DatabaseHW.Services
{
    /// <summary>
    /// 历史记录操作类
    /// </summary>
    public class RecordRepository : Interface.IRecordRepository
    {
        private readonly DataContext m_Context;
        public RecordRepository(DataContext context)
        {
            m_Context = context;
        }

        public void AddRecord(Record record, int accountId)
        {
            Account account = m_Context.Accounts.Find(accountId)
                              ?? throw new ArgumentException($"ID为 {accountId} 的用户不存在");
            // 检查历史记录是否过多
            var records = m_Context.Records.Where(r => r.AccountId == accountId);
            if (records.Count() >= 10)
	            m_Context.Records.Remove(records.First());

            // 添加历史记录
            record.AccountId = accountId;
            m_Context.Records.Add(record);
            m_Context.SaveChanges();
        }

        public void RemoveRecord(Record record)
        {
            m_Context.Records.Remove(record);
            m_Context.SaveChanges();
        }

        public void RemoveAllRecord(int accountId)
        {
            Account account = m_Context.Accounts.Find(accountId)
                              ?? throw new ArgumentException($"ID为 {accountId} 的用户不存在");
            m_Context.Records.RemoveRange(m_Context.Records.Where(r => r.AccountId == accountId));
            m_Context.SaveChanges();
        }

        public List<Record> GetAllRecord(int accountId)
        {
            Account account = m_Context.Accounts.Find(accountId)
                              ?? throw new ArgumentException($"ID为 {accountId} 的用户不存在");
            return m_Context.Records.Where(r => r.AccountId == accountId).ToList();
        }
    }
}
