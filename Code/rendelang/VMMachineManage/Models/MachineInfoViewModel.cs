using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMMachineManage.Models
{
    public class MachineInfoViewModel
    {
        /// <summary>
        /// 虚拟机列表
        /// </summary>
        public List<MachineInfoModel> MachineInfo { get; set; }
        /// <summary>
        /// 操作系统
        /// </summary>
        public SelectList MachineSystem { get; set; }
        /// <summary>
        /// 操作系统
        /// </summary>
        public SelectList OprationType { get; set; }
        /// <summary>
        /// 搜索文本
        /// </summary>
        public string SearchString { get; set; }

    }
}
