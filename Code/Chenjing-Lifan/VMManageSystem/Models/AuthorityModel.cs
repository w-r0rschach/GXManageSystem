using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VMManageSystem.Models
{
    public class AuthorityModel
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Display(Name = "主键")]
        public int AuthorityId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [Display(Name = "权限名称")]
        [StringLength(10, MinimumLength = 2)]
        public string AuthorityName { get; set; }
    }
}
