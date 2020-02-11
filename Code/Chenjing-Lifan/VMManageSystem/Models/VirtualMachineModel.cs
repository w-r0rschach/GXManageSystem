using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VMManageSystem.Data;

namespace VMManageSystem.Models
{
    /// <summary>
    /// 虚拟机模型
    /// </summary>
    public class VirtualMachine
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [Display(Name = "主键Id")]
        public int MachineId { get; set; }

        /// <summary>
        /// 机器IP地址
        /// </summary>
        [Display(Name = "IP地址")]
        [DataType(DataType.Text)]
        [StringLength(11, MinimumLength = 7)]
        [RegularExpression("^((25[0-5]|2[0-4]\\d|[1]{1}\\d{1}\\d{1}|[1-9]{1}\\d{1}|\\d{1})($|(?!\\.$)\\.)){4}$", ErrorMessage = "IP地址不符合规范（127.0.0.1）")]
        public string MachineIP { get; set; }

        /// <summary>
        /// 机器系统（0:windows，1:Linux）
        /// </summary>
        [Display(Name = "操作系统")]
        public OperatingSystemEnum MachineSystem { get; set; }

        /// <summary>
        /// 机器硬盘大小（单位G）
        /// </summary>
        [Display(Name = "硬盘容量")]
        [Range(0, 1024)]
        public double MachineDiskCount { get; set; }

        /// <summary>
        /// 机器内存大小（单位G）
        [Range(0, 1024)]
        /// </summary>
        [Display(Name = "内存容量")]
        public double MachineMemory { get; set; }

        /// <summary>
        /// 机器状态（2:使用中，1:正在被申请，0:空）
        /// </summary>
        [Display(Name = "状态")]
        public MachineStatueEnum MachineState { get; set; }

        /// <summary>
        /// 机器账号
        /// </summary>
        [Display(Name = "账号")]
        [DataType(DataType.Text)]
        [RegularExpression("[A-Za-z0-9]{1,20}", ErrorMessage = "账号不符合规范（英文字母+数字组合）")]
        public string MachineUser { get; set; }

        /// <summary>
        /// 机器密码
        /// </summary>
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 1)]
        public string MachinePassword { get; set; }

        /// <summary>
        /// 申请归还信息
        /// </summary>
        public List<ApprovalModel> Approvals { get; set; }

        /// <summary>
        /// 员工信息
        /// </summary>
        public List<PersonnelModel> Personnels { get; set; }
    }
}
