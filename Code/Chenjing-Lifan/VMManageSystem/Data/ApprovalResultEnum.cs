using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VMManageSystem.Data
{
    public enum ApprovalResultEnum:byte
    {
        /// <summary>
        /// 待审批
        /// </summary>
        [Display(Name= "待审批")]
        Approving = 0,

        /// <summary>
        /// 拒接
        /// </summary>
        [Display(Name = "拒接")]
        NotPass = 1,

        /// <summary>
        /// 通过
        /// </summary>
        [Display(Name = "通过")]
        Pass = 2
    }
}
