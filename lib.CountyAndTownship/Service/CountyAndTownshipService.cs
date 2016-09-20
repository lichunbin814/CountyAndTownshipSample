using lib.CountyAndTownship.Models;
using lib.CountyAndTownship.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace lib.CountyAndTownship.Service
{
    /// <summary>
    /// 存放「縣市」及「鄉鎮市區」資料的容器
    /// </summary>
    public static class CountyAndTownshipConatiner
    {
        /// <summary>
        /// 縣市的下拉選單
        /// </summary>
        public static IEnumerable<SelectListItem> CountySLI = null;

        /// <summary>
        ///縣市的資料
        /// </summary>
        public static Dictionary<int, string> CountyDi = null;

        /// <summary>
        /// 鄉鎮市區的資料
        /// </summary>
        public static Dictionary<int, string> TownShipDi = null;

        /// <summary>
        /// 鄉鎮市區的下拉選單
        /// </summary>
        public static IEnumerable<SelectListItem> TownShipSLI = null;
    }

    
    /// <summary>
    /// 處理「縣市」及「鄉鎮市區」資料的Service
    /// </summary>
    public class CountyAndTownshipService : ICountyAndTownshipService
    {

        /// <summary>
        /// 縣市Repository
        /// </summary>
        private DbSet<County> countyRe { get; set; }

        /// <summary>
        /// 鄉鎮市區Repository
        /// </summary>
        private DbSet<Township> townshipRe { get; set; }


        public CountyAndTownshipService(DbSet<County> countyRe , DbSet<Township> townshipRe)
        {
            this.countyRe = countyRe;
            this.townshipRe = townshipRe;
        }

        public CountyAndTownshipService(ICountyAndTownshipContext context)
        {
            countyRe = context.County;
            townshipRe = context.Township;
        }

        /// <summary>
        /// 取得完整的地址
        /// </summary>
        /// <param name="countyID">縣市ID</param>
        /// <param name="areaID">鄉鎮市區ID</param>
        /// <param name="address">地址</param>
        /// <returns>完整的地址</returns>
        public string GetFullAddress(int countyID, int areaID, string address)
        {
            //縣市
            string countyName = "";
            if (CountyDi.ContainsKey(countyID))
            {
                countyName = CountyDi[countyID];
            }
            //鄉鎮市區
            string townShipName = "";
            string postCode = "";
            if (TownshipDi.ContainsKey(areaID))
            {
                var townshipArrays = TownshipDi[areaID].Split(',');
                if (townshipArrays.Length == 2)
                {
                    postCode = townshipArrays[0];
                    townShipName = townshipArrays[1];
                }

            }
            //格式:郵遞區號+縣市+鄉鎮市區+路名
            return string.Format("{3}{0}{1}{2}", countyName, townShipName, address, postCode);
        }

        /// <summary>
        /// 縣市的資料
        /// </summary>
        private Dictionary<int, string> CountyDi
        {
            get
            {
                if (CountyAndTownshipConatiner.CountyDi == null || CountyAndTownshipConatiner.CountyDi.Count() == 0)
                {
                    CountyAndTownshipConatiner.CountyDi = GetCountyData();
                }

                return CountyAndTownshipConatiner.CountyDi;
            }
        }

        /// <summary>
        /// 取得設定縣市的資料
        /// </summary>
        private Dictionary<int, string> GetCountyData()
        {
            return countyRe.ToDictionary(_key => _key.Code, _value => _value.Name);
        }

        /// <summary>
        /// 鄉鎮市區的資料
        /// <para>Key:自動成長的主鍵</para>
        /// <para>Value:郵遞區號 + 鄉鎮市區名稱(以逗號區隔) </para>
        /// </summary>
        private Dictionary<int, string> TownshipDi
        {
            get
            {
                if (CountyAndTownshipConatiner.TownShipDi == null || CountyAndTownshipConatiner.TownShipDi.Count() == 0)
                {
                    CountyAndTownshipConatiner.TownShipDi = GetTownshipData();
                }

                return CountyAndTownshipConatiner.TownShipDi;
            }
        }

        /// <summary>
        /// 取得鄉鎮市區的資料
        /// </summary>
        /// <returns>
        /// 鄉鎮市區的字典檔
        /// <para>Key:自動成長的主鍵</para>
        /// <para>Value:郵遞區號 + 鄉鎮市區名稱(以逗號區隔) </para>
        /// </returns>
        private Dictionary<int, string> GetTownshipData()
        {
            return townshipRe.ToDictionary(_key => _key.Code, _value => _value.PostCode + "," + _value.Name);
        }

        /// <summary>
        /// 取得縣市的下拉選單
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetCountySelectListItem()
        {



            if (CountyAndTownshipConatiner.CountySLI == null || CountyAndTownshipConatiner.CountySLI.Count() == 0)
            {

                IEnumerable<SelectListItem> countyList = countyRe.Select(_county => new SelectListItem
                {
                    Text = _county.Name,
                    Value = _county.Code.ToString()
                }).ToList();

                CountyAndTownshipConatiner.CountySLI = countyList;
            }

            return CountyAndTownshipConatiner.CountySLI;
        }

        /// <summary>
        /// 取得鄉鎮市區的下拉選單
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetTownshipSelectListItem()
        {

            if (CountyAndTownshipConatiner.TownShipSLI == null || CountyAndTownshipConatiner.TownShipSLI.Count() == 0)
            {
                //取得資料
                var data = townshipRe.OrderBy(_township => _township.CountyCode)
                   .Select(_township => new {
                       _township.CountyCode,
                       _township.Code,
                       _township.Name,
                       _township.PostCode
                   }).ToList()
                   .Select(_township => new {
                       ValueField = _township.Code,
                       TextField = string.Format("{0} {1}", _township.PostCode, _township.Name),
                       GroupField = CountyDi[_township.CountyCode]
                   });
                //轉為SelectListItem
                var townshipList = new SelectList(
                    items: data,
                    dataValueField: "ValueField",
                    dataTextField: "TextField",
                    dataGroupField: "GroupField",
                    selectedValue: null
                );


                CountyAndTownshipConatiner.TownShipSLI = townshipList;
            }

            return CountyAndTownshipConatiner.TownShipSLI;
        }

        /// <summary>
        /// 取得資料
        /// </summary>
        /// <returns></returns>
        public CountyAndTownshipModel Get()
        {
            return new CountyAndTownshipModel
            {
                CountyListItem = GetCountySelectListItem(),
                AreaListItem = GetTownshipSelectListItem()
            };
        }

        /// <summary>
        /// 取得鄉鎮市區的下拉選單資料
        /// </summary>
        /// <param name="model"></param>
        public void Bind(ICountyAndTownshipModel model)
        {
            model.CountyAndTownship = Get();
        }

        /// <summary>
        /// 取得鄉鎮市區的下拉選單資料
        /// </summary>
        /// <param name="model"></param>
        public void Bind(CountyAndTownshipModel model)
        {
            model = Get();
        }

    }
}
