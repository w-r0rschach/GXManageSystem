using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VMManageSystem.Models
{
    /// <summary>
    /// 员工信息
    /// </summary>
    public class PersonnelModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int PersonnelId { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public int PersonnelNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        public string PersonnelName { get; set; }

        /// <summary>
        /// 部门关联Id
        /// </summary>
        public byte DepId { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public byte PersonnelSex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdentityCard { get; set; }

        /// <summary>
        /// 是否在职
        /// </summary>
        public byte IsWork { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 是否已婚
        /// </summary>
        public byte MaritalStatus { get; set; }

        /// <summary>
        /// 现住地址
        /// </summary>
        public string LiveAddress { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 微信账号
        /// </summary>
        public string WeChatAccount { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Mailbox { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public byte Degree { get; set; }

        /// <summary>
        /// 户籍地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 入职时间
        /// </summary>
        public DateTime OnboardingTime { get; set; }

        /// <summary>
        /// 离职时间
        /// </summary>
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// 试用结束时间
        /// </summary>
        public DateTime TrialTime { get; set; }

        /// <summary>
        /// 奋斗者
        /// </summary>
        public byte IsStruggle { get; set; }

        /// <summary>
        /// 是否保密人员
        /// </summary>
        public byte IsSecrecy { get; set; }

        /// <summary>
        /// 申请归还列表
        /// </summary>
        public List<ApprovalModel> ApplyAndReturnInfos { get; set; }
    }
}
