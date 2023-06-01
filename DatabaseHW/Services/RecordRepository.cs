using DatabaseHW.Models;

namespace DatabaseHW.Services
{
    /// <summary>
    /// 历史记录操作类
    /// </summary>
    public class RecordRepository : Interface.IRecordRepository
    {
        public void AddRecord(Record record, int accountId)
        {
            throw new NotImplementedException();
        }

        public void RemoveRecord(Record record)
        {
            throw new NotImplementedException();
        }

        public void RemoveAllRecord(int accountId)
        {
            throw new NotImplementedException();
        }

        public List<Record> GetRecord(int accountId)
        {
            throw new NotImplementedException();
        }
    }
}
