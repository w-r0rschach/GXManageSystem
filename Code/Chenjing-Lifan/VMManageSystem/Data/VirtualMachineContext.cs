using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VMManageSystem.Models;

namespace VMManageSystem.Data
{
    public class VirtualMachineContext : DbContext
    {
        public VirtualMachineContext(DbContextOptions<VirtualMachineContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 用于数据库建表
        /// </summary>
        public DbSet<VMManageSystem.Models.VirtualMachine> MachineInfo { get; set; }

        /// <summary>
        /// 用于数据库建表
        /// </summary>
        public DbSet<VMManageSystem.Models.PersonnelModel> Common_PersonnelInfo { get; set; }

        /// <summary>
        /// 用于数据库建表
        /// </summary>
        public DbSet<VMManageSystem.Models.ApprovalModel> MachApplyAndReturn { get; set; }

        /// <summary>
        /// 正在创建模型时
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApprovalModel>()
            .HasKey(t => new { t.ApplyUserID, t.ExamineUserID, t.MachineInfoID });

            modelBuilder.Entity<ApprovalModel>()
                .HasOne(pt => pt.Personnel)
                .WithMany(p => p.ApplyAndReturnInfos)
                .HasForeignKey(pt => pt.ApplyUserID)
                .HasForeignKey(pt => pt.ExamineUserID);

            modelBuilder.Entity<ApprovalModel>()
                .HasOne(pt => pt.VirtualMachine)
                .WithMany(t => t.Approvals)
                .HasForeignKey(pt => pt.MachineInfoID);
            //base.OnModelCreating(modelBuilder);
        }
    }
}
