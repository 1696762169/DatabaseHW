namespace DatabaseHW.Services.Interface
{
    /// <summary>
    /// 数据批量创建/删除接口（仅供测试用）
    /// </summary>
    public interface IDataGenerator
    {
        /// <summary>
        /// 批量添加随机数据
        /// </summary>
        public void GenerateData<T>(int count) where T : class, new();
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="count">-1表示查看所有数据</param>
        public List<T> GetAllData<T>(int count = -1) where T : class;
        /// <summary>
        /// 删除所有数据
        /// </summary>
        public void RemoveAllData<T>() where T : class;
    }
}
