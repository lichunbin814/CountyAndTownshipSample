using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace lib.CountyAndTownship.Models
{
    /// <summary>
    /// 存放縣市鄉鎮資料的ViewModel
    /// </summary>
    public interface ICountyAndTownshipModel
    {

        /// <summary>
        /// 存放縣市鄉鎮資料的ViewModel
        /// </summary>
        CountyAndTownshipModel CountyAndTownship { get; set; }
    }

    /// <summary>
    /// 存放縣市鄉鎮資料的ViewModel
    /// </summary>
    public class CountyAndTownshipModel
    {

        /// <summary>
        /// 縣市的下拉選單
        /// </summary>
        public IEnumerable<SelectListItem> CountyListItem { get; set; }

        /// <summary>
        /// 鄉鎮市區的下拉區單
        /// </summary>
        public IEnumerable<SelectListItem> AreaListItem { get; set; }
    }
}
