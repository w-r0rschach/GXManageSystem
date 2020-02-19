using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VMManageSystem.Data;

namespace VMManageSystem.Models
{
    public class ApprovalViewModel
    {
        /// <summary>
        /// 申请归还信息
        /// </summary>
        public ApprovalModel Approval { get; set; }

        /// <summary>
        /// 申请归还信息列表
        /// </summary>
        public List<ApprovalModel> Approvals { get; set; }

        /// <summary>
        /// 员工信息
        /// </summary>
        public List<PersonnelModel> Personnels { get; set; }

        /// <summary>
        /// 虚拟机信息列表
        /// </summary>
        public List<VirtualMachineModel> VirtualMachines { get; set; }

        private int _usedDays;
        /// <summary>
        /// 申请天数
        /// </summary>
        [Display(Name = "申请天数")]
        public int UsedDays
        {
            get { return _usedDays; }
            set
            {
                if (Approval == null)
                {
                    Approval = new ApprovalModel();
                }
                Approval.ApplyTime = DateTime.Now;
                Approval.ResultTime = DateTime.Now.AddDays(value);
            }
        }
        /// <summary>
        /// 申请数量
        /// </summary>
        [Range(1, 100)]
        [Display(Name = "申请数量")]
        public int UsedCount { get; set; }

        /// <summary>
        /// 操作系统
        /// </summary>
        [EnumDataType(typeof(OperatingSystemEnum))]
        [Display(Name = "操作系统")]
        public OperatingSystemEnum OperatingSystem { get; set; }

    }
}
