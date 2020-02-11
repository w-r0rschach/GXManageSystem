using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VMMachineManage.Models
{
    public class Common_PersonnelInfoModel
    {
        /// <summary>
        /// 员工Id
        /// </summary>
        [Key]
        [Display(Name = "员工Id")]
        public int PersonnelId { get; set; }
        /// <summary>
        /// 员工密码
        /// </summary>
        [StringLength(16)]
        [Display(Name ="密码")]
        public string PassWord { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        [Display(Name = "工号")]
        public int PersonnelNo { get; set; }
        /// <summary>
        /// 员工名称
        /// </summary>
        [StringLength(50)]
        [Display(Name = "用户名")]
        public string PersonnelName { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        [Display(Name = "所属部门")]
        public int DepId { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(200)]
        [Display(Name = "头像")]
        public string Avatar { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Range(0,2)]
        [Display(Name = "性别")]
        public int PersonnelSex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
         [Display(Name = "出生日期")]
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [StringLength(18)]
        [Display(Name = "身份证号")]
        public string IdentityCard { get; set; }
        /// <summary>
        /// 是否在职
        /// </summary>
         [Display(Name = "是否在职")]
         [Range(0,2)] 
        public int IsWork { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        [StringLength(6)]
        [Display(Name = "民族")]
        public string Nation { get; set; }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        [Range(0,2)]
        [Display(Name = "婚姻状况")]
        public int MaritalStatus { get; set; }
        /// <summary>
        /// 现住地址
        /// </summary>
        [StringLength(200)]
        [Display(Name = "现住地址")]
        public string LiveAddress { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [StringLength(500)]
        [Display(Name = "电话号码")]
        public string Phone { get; set; }
        /// <summary>
        /// 微信账号
        /// </summary>
        [StringLength(100)]
        [Display(Name = "微信账号")]
        public string WeChatAccount { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(500)]
        [Display(Name = "邮箱")]
        public string Mailbox { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        [Range(0,8)]
        [Display(Name = "学历")]
        public int Degree { get; set; }
        /// <summary>
        /// 户籍地址
        /// </summary>
        [StringLength(200)]
        [Display(Name = "户籍地址")]
        public string Address { get; set; }
        /// <summary>
        /// 入职时间
        /// </summary>
        [Display(Name = "入职时间")]
        public DateTime OnBoarDingTime { get; set; }
        /// <summary>
        /// 离职时间
        /// </summary>
         [Display(Name = "离职时间")]
        public DateTime DepartureTime { get; set; }
        /// <summary>
        /// 试用结束时间
        /// </summary>
        [Display(Name = "试用结束时间")]
        public DateTime TrialTime { get; set; }
        /// <summary>
        /// 奋斗者
        /// </summary>
        [Range(0,2)]
        [Display(Name = "奋斗者")]
        public int IsStruggle { get; set; }
        /// <summary>
        /// 否是保密人员
        /// </summary>
        [Range(0,2)]
        [Display(Name = "保密人员")]
        public int IsSecrecy { get; set; }
    }
}
