using System;
using System.Collections.Generic;
using System.Text;
using GX.ManageSystem.Portal.Models.UserModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GX.ManageSystem.Portal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        /// <summary>
        /// 员工信息
        /// </summary>
        public DbSet<Common_PersonnelInfoModel> Common_PersonnelInfo { get; set; }
        /// <summary>
        /// 部门信息
        /// </summary>
        public DbSet<Common_DepartmentModel> Common_Department { get; set; }
    }
}
