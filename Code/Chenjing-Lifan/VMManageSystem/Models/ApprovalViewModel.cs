using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        private int _usedDays;
        /// <summary>
        /// 使用天数
        /// </summary>
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
        /// 使用数量
        /// </summary>
        public int UsedCount { get; set; }


    }
}
