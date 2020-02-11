using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VMManageSystem.Models
{
    /// <summary>
    /// 审批单
    /// </summary>
    public class ApprovalModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Display(Name = "主键")]
        public int ApplyAndReturnId { get; set; }

        /// <summary>
        /// 操作类型 0：申请，1：归还 
        /// </summary>
        [Range(0, 2)]
        [Display(Name = "操作类型")]
        public byte OprationType { get; set; }

        /// <summary>
        /// 申请人员Id（员工Id） 外键
        /// </summary>
        [Display(Name = "申请人员")]
        public int ApplyUserID { get; set; }

        /// <summary>
        /// 审批人员Id（员工Id） 外键
        /// </summary>
        [Display(Name = "审批人员")]
        public int ExamineUserID { get; set; }

        /// <summary>
        /// 虚拟机Id 外键
        /// </summary>
        [Display(Name = "虚拟机Id")]
        public int MachineInfoID { get; set; }

        /// <summary>
        /// 申请结果 0：同意，1：拒绝
        /// </summary>
        [Range(0, 2)]
        [Display(Name = "申请结果")]
        public byte ExamineResult { get; set; }

        /// <summary>
        /// 申请时间（格式：yyyy-mm-dd HH:MM:SS）
        /// </summary>
        [Display(Name = "申请时间")]
        [DataType(DataType.DateTime)]
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 归还时间（格式：yyyy-mm-dd HH:MM:SS）
        /// </summary>
        [Display(Name = "归还时间")]
        [DataType(DataType.DateTime)]
        public DateTime ResultTime { get; set; }

        /// <summary>
        /// 员工
        /// </summary>
        public virtual PersonnelModel Personnel { get; set; }

        /// <summary>
        /// 虚拟机
        /// </summary>
        public virtual VirtualMachine VirtualMachine { get; set; }
    }
}
