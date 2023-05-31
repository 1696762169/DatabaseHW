using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DatabaseHW.Data;
using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
using SimpleJSON;

namespace DatabaseHWTests
{
    public class Startup
    {
        // 准备数据库对象
        protected readonly DataContext context;
        // 定义连接字符串
        private const string CONNECTION_STRING =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DatabaseHW;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        
        public Startup()
        {
            // 创建 DbContextOptionsBuilder 对象，并使用连接字符串配置选项
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(CONNECTION_STRING);

            // 使用配置选项创建 DataContext 对象
            context = new DataContext(optionsBuilder.Options);
        }
    }
}
