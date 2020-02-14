using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VMMachineManage.Models
{
    /// <summary>
    /// 虚拟机申请
    /// </summary>
    public class MachApplyAndReturnModel
    {
        #region "虚拟机信息"
        /// <summary>
        /// 操作系统
        /// </summary>
        [Display(Name = "操作系统")]
        [NotMapped]
        public int MachineSystem { get; set; }

        /// <summary>
        /// 硬盘大小
        /// </summary>
        [NotMapped]
        public double MachineDiskCount { get; set; }
        /// <summary>
        /// 内存大小
        /// </summary>
        [NotMapped]
        public double MachineMemory { get; set; }
        /// <summary>
        /// 虚拟机ID
        /// </summary>
        [NotMapped]
        public int MachineID { get; set; } 
        #endregion
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int ApplyAndReturnId { get; set; }
        /// <summary>
        /// 0：申请，1：归还 
        /// </summary>
        [Range(0, 2)]
        [Display(Name = "操作类型")]
        public int OprationType { get; set; }
        /// <summary>
        /// 申请人员ID
        /// </summary>
        public int ApplyUserID { get; set; }
        /// <summary>
        /// 审批人员ID
        /// </summary>
        public int ExamineUserID { get; set; }
        /// <summary>
        /// 审批人
        /// </summary>
        [NotMapped]
        [Display(Name = "审批人")]
        public string ExamineUserName { get; set; }
        /// <summary>
        /// 虚拟机ID
        /// </summary>
        public int MachineInfoID { get; set; }
        /// <summary>
        /// 审批结果	0：同意，1：拒绝
        /// </summary>
        [Range(0, 2)]
        public int ExamineResult { get; set; }
        /// <summary>
        /// 申请数量
        /// </summary>
        [Display(Name = "申请数量")]
        [NotMapped]
        public int Number { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        [Display(Name = "申请时间")]
        public DateTime ApplyTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 申请天数
        /// </summary>
        [Range(0, 16)]
        [Display(Name = "使用天数")]
        [NotMapped]
        public int DaysNumber { get; set; }
        /// <summary>
        /// 归还时间
        /// </summary>
        [Display(Name = "归还时间")]
        public DateTime ResultTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "申请原因")]
        public string Remark { get; set; }

    }
}
