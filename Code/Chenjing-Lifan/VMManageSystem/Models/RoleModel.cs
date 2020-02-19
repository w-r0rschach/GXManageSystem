using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VMManageSystem.Models
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int RoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Display(Name = "角色名称")]
        [StringLength(10, MinimumLength = 2)]
        public string RoleName { get; set; }
    }
}
