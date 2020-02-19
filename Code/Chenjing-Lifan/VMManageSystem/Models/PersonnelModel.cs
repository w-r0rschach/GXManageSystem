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
        [Range(1, 500)]
        [Display(Name = "工号")]
        public int PersonnelNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        [DataType(DataType.Text)]
        //[StringLength(10, MinimumLength = 2)]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "不能为空")]
        [RegularExpression("[\u4e00-\u9fa5]{2,10}", ErrorMessage = "中文字符，字符长度2~10")]
        public string PersonnelName { get; set; }

        /// <summary>
        /// 部门关联Id
        /// </summary>
        [Display(Name = "部门")]
        public byte DepId { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Display(Name = "头像")]
        public string Avatar { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public byte PersonnelSex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "出生日期")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [DataType(DataType.Text)]
        [Display(Name = "身份证号码")]
        //[StringLength(18, ErrorMessage = "字符长度", MinimumLength = 18)]
        [RegularExpression("[0-9A-Z]{18}", ErrorMessage = "数字或数字+X组合，字符长度18")]
        public string IdentityCard { get; set; }

        /// <summary>
        /// 是否在职
        /// </summary>
        [Display(Name = "是否在职")]
        public byte IsWork { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        [Display(Name = "民族")]
        public string Nation { get; set; }

        /// <summary>
        /// 是否已婚
        /// </summary>
        [Display(Name = "是否已婚")]
        public byte MaritalStatus { get; set; }

        /// <summary>
        /// 现住地址
        /// </summary>
        [DataType(DataType.Text)]
        [Display(Name = "现住地址")]
        public string LiveAddress { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [Display(Name = "电话号码")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        /// <summary>
        /// 微信账号
        /// </summary>
        [Display(Name = "电话号码")]
        [Required(AllowEmptyStrings = true)]
        public string WeChatAccount { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false)]
        public string Mailbox { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        [Display(Name = "学历")]
        public byte Degree { get; set; }

        /// <summary>
        /// 户籍地址
        /// </summary>
        [Display(Name = "户籍地址")]
        [Required(AllowEmptyStrings = false)]
        public string Address { get; set; }

        /// <summary>
        /// 入职时间
        /// </summary>
        [Display(Name = "入职时间")]
        [DataType(DataType.DateTime)]
        public DateTime OnboardingTime { get; set; }

        /// <summary>
        /// 离职时间
        /// </summary>
        [Display(Name = "离职时间")]
        [DataType(DataType.DateTime)]
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// 试用结束时间
        /// </summary>
        [Display(Name = "试用结束时间")]
        [DataType(DataType.DateTime)]
        public DateTime TrialTime { get; set; }

        /// <summary>
        /// 奋斗者
        /// </summary>
        [Display(Name = "奋斗者")]
        public byte IsStruggle { get; set; }

        /// <summary>
        /// 是否保密人员
        /// </summary>
        [Display(Name = "是否保密人员")]
        public byte IsSecrecy { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [Display(Name = "账号")]
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }

        /// <summary>
        /// 虚拟机最大申请数
        /// </summary>
        [Display(Name = "虚拟机最大申请数")]
        public int AppMaxCount { get; set; }

        /// <summary>
        /// 申请归还列表
        /// </summary>
        public List<ApprovalModel> Approvals { get; set; }
    }
}
