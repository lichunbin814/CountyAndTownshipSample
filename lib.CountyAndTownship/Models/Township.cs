using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib.CountyAndTownship.Models
{
    [Table("Township")]
    public class Township
    {
        /// <summary>
        /// 索引鍵
        /// </summary>
        [Key]
        public int Code { get; set; }

        /// <summary>
        /// 鄉鎮市區名稱
        /// </summary>
        [StringLength(4)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 郵遞區號
        /// </summary>
        [Display(Name = "郵遞區號")]
        [Required]
        public int PostCode { get; set; }

        /// <summary>
        /// 縣市的Code
        /// </summary>
        [Display(Name = "縣市的Code")]
        public int CountyCode { get; set; }
    }
}
