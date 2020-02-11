using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VMManageSystem.Data
{
    /// <summary>
    /// 虚拟机状态枚举
    /// </summary>
    public enum MachineStatueEnum:byte
    {
        /// <summary>
        /// 未使用
        /// </summary>
        [Display(Name = "未使用")]
        None = 0,
        /// <summary>
        /// 正在申请
        /// </summary>
        [Display(Name = "正在申请")]
        Applying = 1,
        /// <summary>
        /// 正在被使用
        /// </summary>
        [Display(Name = "正在被使用")]
        BeingUsed = 2
    }
}
