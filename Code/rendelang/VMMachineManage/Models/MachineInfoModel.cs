using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks; 

namespace VMMachineManage.Models
{
    public class MachineInfoModel
    {/// <summary>
    /// 主键Id
    /// </summary>
        [Key]
        public int MachineID { get; set; }
        /// <summary>
        /// 虚拟机IP
        /// </summary> 
        [Display(Name ="IP")]
        [DataType(DataType.Text)]
        [StringLength(11, MinimumLength = 7)]
        [RegularExpression("^((25[0-5]|2[0-4]\\d|[1]{1}\\d{1}\\d{1}|[1-9]{1}\\d{1}|\\d{1})($|(?!\\.$)\\.)){4}$", ErrorMessage = "IP地址不符合规范（127.0.0.1）")]
        public string MachineIP { get; set; }
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
       [ Display(Name = "内存大小")]
        [Range(0, 1024)]
        public double MachineMemory { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Range(0,3)]
        [Display(Name = "状态")]
        public int MachineState { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [StringLength(20)]
        [Display(Name = "账号")]
        [DataType(DataType.Text)]
        [RegularExpression("[A-Za-z0-9]{1,20}", ErrorMessage = "账号不符合规范（英文字母+数字组合）")]
        public string MachineUser { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [StringLength(20)]
        public string MachinePassword { get; set; }
        /// <summary>
        /// 虚拟机申请信息
        /// </summary>
        public MachApplyAndReturnModel MachApplyAndReturnInfo { get; set; }
        /// <summary>
        /// 员工信息
        /// </summary>
        public PersonnelInfoModel PersonnelInfo { get; set; }

    }
}
