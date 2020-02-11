﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMManageSystem.Models
{
    public class VirtualMachineViewModel
    {
        /// <summary>
        /// 虚拟机列表
        /// </summary>
        public List<VirtualMachine> VirtualMachines { get; set; }

        /// <summary>
        /// 搜索内容
        /// </summary>
        public string SearchString { get; set; }

        /// <summary>
        /// 操作系统
        /// </summary>
        public SelectList OperationSystem { get; set; }
    }
}