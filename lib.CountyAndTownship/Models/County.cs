using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib.CountyAndTownship.Models
{
    /// <summary>
    /// 縣市
    /// </summary>
    [Table("County")]
    public class County
    {

        /// <summary>
        /// 代號
        /// </summary>
        [Key]
        public int Code { get; set; }

        /// <summary>
        /// 縣市名稱
        /// </summary>
        [StringLength(3)]
        [Required]
        [Display(Name = "縣市名稱")]
        public string Name { get; set; }

    }
}
