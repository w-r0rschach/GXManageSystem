using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VMMachineManage.Models
{
    public class ApplyDataModel
    {
        /// <summary>
        /// 操作系统
        /// </summary>
        [Display(Name = "操作系统")]
        public int MachineSystem { get; set; }

        /// <summary>
        /// 硬盘大小
        /// </summary>
        [Display(Name = "硬盘大小")]
        [Range(0, 1024)]
        public double MachineDiskCount { get; set; }
        /// <summary>
        /// 内存大小
        /// </summary>
        [Display(Name = "内存大小")]
        [Range(0, 1024)]
        public double MachineMemory { get; set; }
        /// <summary>
        /// 审批人
        /// </summary> 
        [Display(Name = "审批人")]
        public string ExamineUserName { get; set; }
        /// <summary>
        /// 申请数量
        /// </summary>
        [Display(Name = "申请数量")] 
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
        public int DaysNumber { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "申请原因")]
        public string Remark { get; set; }
        /// <summary>
        /// 0：申请，1：归还 
        /// </summary>
        [Range(0, 2)]
        [Display(Name = "操作类型")]
        public int OprationType { get; set; }
    }
}
