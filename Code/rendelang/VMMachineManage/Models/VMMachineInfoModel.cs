using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMMachineManage.Models
{
    public class VMMachineInfoModel
    {
        /// <summary>
        /// 虚拟机信息
        /// </summary>
        public MachineInfoModel machineInfo { get; set; }
        /// <summary>
        /// 虚拟机申请信息
        /// </summary>
        public MachApplyAndReturnModel MachApplyAndReturn { get; set; }
        /// <summary>
        /// 员工信息
        /// </summary>
        public PersonnelInfoModel PersonnelInfo { get; set; }
    }
}
