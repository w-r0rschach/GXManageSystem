using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GX.ManageSystem.Portal.Models.UserModel
{
    public class Common_PersonnelInfoModel
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        [Key]
        public int PersonnelId { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public int PersonnelNo { get; set; }
        /// <summary>
        /// 员工名称
        /// </summary>
        [StringLength(50)]
        public string PersonnelName { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public int DepId { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(200)]
        public string Avatar { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [StringLength(1)]
        public int PersonnelSex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [StringLength(18)]
        public string IdentityCard { get; set; }
        /// <summary>
        /// 是否在职
        /// </summary>
        [StringLength(1)]
        public int IsWork { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        [StringLength(6)]
        public string Nation { get; set; }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        [StringLength(1)]
        public int MaritalStatus { get; set; }
        /// <summary>
        /// 现住地址
        /// </summary>
        [StringLength(200)]
        public string LiveAddress { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [StringLength(500)]
        public string Phone { get; set; }
        /// <summary>
        /// 微信账号
        /// </summary>
        [StringLength(100)]
        public string WeChatAccount { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(500)]
        public string Mailbox { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        [StringLength(1)]
        public int Degree { get; set; }
        /// <summary>
        /// 户籍地址
        /// </summary>
        [StringLength(200)]
        public string Address { get; set; }
        /// <summary>
        /// 入职时间
        /// </summary>
        public DateTime OnBoarDingTime { get; set; }
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
        [StringLength(1)]
        public int IsStruggle { get; set; }
        /// <summary>
        /// 否是保密人员
        /// </summary>
        [StringLength(1)]
        public int IsSecrecy { get; set; }

    }
}
