﻿using DatabaseHW.Models;

namespace DatabaseHW.Services.Interface
{
    /// <summary>
    /// 出租房筛选接口
    /// </summary>
    public interface IHouseFilter
    {
        /// <summary>
        /// 根据出租房筛选条件筛选出房屋
        /// </summary>
        /// <param name="community">房屋所属小区</param>
        /// <param name="condition">房屋筛选条件</param>
        public List<House> Filter(Community community, HouseCondition condition);
    }
}
