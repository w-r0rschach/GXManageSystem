using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VMMachineManage.Models;

namespace VMMachineManage.Data
{
    public class VMMachineManageContext : DbContext
    {
        public VMMachineManageContext (DbContextOptions<VMMachineManageContext> options)
            : base(options)
        {
        }

        public DbSet<VMMachineManage.Models.Common_PersonnelInfoModel> Common_PersonnelInfo { get; set; }
        public DbSet<Common_DepartmentModel> Common_Department { get; set; }
    }
}
