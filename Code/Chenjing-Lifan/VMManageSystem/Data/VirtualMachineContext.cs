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
        public DbSet<VMManageSystem.Models.VirtualMachineModel> MachineInfo { get; set; }

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
            //modelBuilder.Entity<PersonnelModel>()
            //    .HasKey(p => p.PersonnelId);
            //modelBuilder.Entity<VirtualMachineModel>()
            //    .HasKey(v => v.MachineId);
            //modelBuilder.Entity<ApprovalModel>()
            //    .HasKey(a => a.ApplyAndReturnId);

            //modelBuilder.Entity<PersonnelModel>()
            //    .HasMany(p => p.ApprovalModels);
            //modelBuilder.Entity<VirtualMachineModel>()
            //    .HasMany(v => v.Personnels);
            //modelBuilder.Entity<VirtualMachineModel>()
            //    .HasMany(v => v.Approvals);



            //modelBuilder.Entity<ApprovalModel>()
            //.HasKey(t => t.ApplyAndReturnId);

            //modelBuilder.Entity<ApprovalModel>()
            //    .HasOne(pt => pt.Personnel)
            //    .WithMany(p => p.Approvals)
            //    .HasForeignKey(pt => pt.ApplyUserID)
            //    .HasForeignKey(pt => pt.ExamineUserID);

            modelBuilder.Entity<ApprovalModel>()
                .HasOne(pt => pt.VirtualMachine)
                .WithMany(p => p.Approvals)
                .HasForeignKey(pt => pt.MachineInfoID);

            //modelBuilder.Entity<ApprovalModel>()
            //    .HasOne(pt => pt.VirtualMachine)
            //    .WithMany(t => t.Approvals)
            //    .HasForeignKey(pt => pt.MachineInfoID);
            //base.OnModelCreating(modelBuilder);
        }
    }
}
