using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GX.ManageSystem.Portal.Models.UserModel
{
    public class Common_DepartmentModel
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        [Key]
        public int DepId { get; set; }
        /// <summary>
        /// 上级部门编号
        /// </summary>
        public int ParentNumber { get; set; }
       /// <summary>
       /// 部门名称
       /// </summary>
        [StringLength(20)]
        public string DepName { get; set; }
    }
}
