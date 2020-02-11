using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VMManageSystem.Data
{
    /// <summary>
    /// 操作系统枚举
    /// </summary>
    public enum OperatingSystemEnum : byte
    {
        /// <summary>
        /// Windows操作系统
        /// </summary>
        [Display(Name= "Windows")]
        Windows = 0,
        /// <summary>
        /// Linux操作系统
        /// </summary>
        [Display(Name = "Linux")]
        Linux = 1,
    }
}
