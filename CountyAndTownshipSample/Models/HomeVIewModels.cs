using lib.CountyAndTownship.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CountyAndTownshipSample.Models
{
    public class HomeViewModels : ICountyAndTownshipModel
    {
        /// <summary>
        /// 縣市ID
        /// </summary>
        [Display(Name = "縣市")]
        public int? CountyId { get; set; }

        /// <summary>
        /// 鄉鎮市區ID
        /// </summary>
        [Display(Name = "鄉鎮市區")]
        public int? AreaId { get; set; }

        public string Address { get; set; }

        public CountyAndTownshipModel CountyAndTownship { get; set; }
    }
}