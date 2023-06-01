using DatabaseHW.Models;

namespace DatabaseHW.Services.Interface
{
    /// <summary>
    /// 历史记录操作接口
    /// </summary>
    public interface IRecordRepository
    {
        /// <summary>
        /// 添加历史记录（超过记录上限会移除旧的记录）
        /// </summary>
        public void AddRecord(Record record, int accountId);

        /// <summary>
        /// 移除一条历史记录
        /// </summary>
        public void RemoveRecord(Record record);
        /// <summary>
        /// 清空某个用户的所有历史记录
        /// </summary>
        public void RemoveAllRecord(int accountId);

        /// <summary>
        /// 获取一个用户的所有历史记录
        /// </summary>
        public List<Record> GetRecord(int accountId);
    }
}
