using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DatabaseHW.Models.Interface
{
    /// <summary>
    /// 地点模型接口
    /// </summary>
    public interface ILocation
    {
        public string Name { get; set; }        // 名称
        public string Address { get; set; }     // 地址
        public float Longitude { get; set; }    // 经度
        public float Latitude { get; set; }     // 纬度
    }
}
