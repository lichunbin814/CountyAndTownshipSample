using lib.CountyAndTownship.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib.CountyAndTownship.Service.Interface
{
    public interface ICountyAndTownshipService
    {
        ///// <summary>
        ///// 縣市的DB
        ///// </summary>
        //DbSet<County> CountyRe { get; set; }

        ///// <summary>
        ///// 鄉鎮市區的DB
        ///// </summary>
        //DbSet<Township> TownshipRe { get; set; }

        /// <summary>
        /// 取得完整的地址
        /// </summary>
        /// <param name="countyID">縣市ID</param>
        /// <param name="areaID">鄉鎮市區ID</param>
        /// <param name="address">地址</param>
        /// <returns>完整的地址</returns>
        string GetFullAddress(int countyID, int areaID, string address);

        /// <summary>
        /// 取得鄉鎮市區的下拉選單資料
        /// </summary>
        /// <returns></returns>
        CountyAndTownshipModel Get();

        /// <summary>
        /// 取得鄉鎮市區的下拉選單資料
        /// </summary>
        /// <param name="model"></param>
        void Bind(ICountyAndTownshipModel model);

        /// <summary>
        /// 取得鄉鎮市區的下拉選單資料
        /// </summary>
        /// <param name="model"></param>
        void Bind(CountyAndTownshipModel model);
    }
}
