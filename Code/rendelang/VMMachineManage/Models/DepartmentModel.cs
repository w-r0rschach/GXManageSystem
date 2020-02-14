using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VMMachineManage.Models
{
    public class Common_DepartmentModel
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        [Key]
        [Display(Name ="部门编号")]
        public int DepId { get; set; }
        /// <summary>
        /// 上级部门编号
        /// </summary>
       [Display(Name = "上级部门编号")]
        public int ParentNumber { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [StringLength(20)]
        [Display(Name = "部门名称")]
        public string DepName { get; set; }
    }
}
