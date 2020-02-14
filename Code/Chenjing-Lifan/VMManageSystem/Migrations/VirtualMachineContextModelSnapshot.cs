﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VMManageSystem.Data;

namespace VMManageSystem.Migrations
{
    [DbContext(typeof(VirtualMachineContext))]
    partial class VirtualMachineContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VMManageSystem.Models.ApprovalModel", b =>
                {
                    b.Property<int>("ApplyAndReturnId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ApplyTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ApplyUserID")
                        .HasColumnType("int");

                    b.Property<byte>("ExamineResult")
                        .HasColumnType("tinyint");

                    b.Property<int>("ExamineUserID")
                        .HasColumnType("int");

                    b.Property<int>("MachineInfoID")
                        .HasColumnType("int");

                    b.Property<byte>("OprationType")
                        .HasColumnType("tinyint");

                    b.Property<int?>("PersonnelId")
                        .HasColumnType("int");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(127)")
                        .HasMaxLength(127);

                    b.Property<DateTime>("ResultTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ApplyAndReturnId");

                    b.HasIndex("MachineInfoID");

                    b.HasIndex("PersonnelId");

                    b.ToTable("MachApplyAndReturn");
                });

            modelBuilder.Entity("VMManageSystem.Models.PersonnelModel", b =>
                {
                    b.Property<int>("PersonnelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("Degree")
                        .HasColumnType("tinyint");

                    b.Property<byte>("DepId")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdentityCard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("IsSecrecy")
                        .HasColumnType("tinyint");

                    b.Property<byte>("IsStruggle")
                        .HasColumnType("tinyint");

                    b.Property<byte>("IsWork")
                        .HasColumnType("tinyint");

                    b.Property<string>("LiveAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mailbox")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("MaritalStatus")
                        .HasColumnType("tinyint");

                    b.Property<string>("Nation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OnboardingTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PersonnelName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonnelNo")
                        .HasColumnType("int");

                    b.Property<byte>("PersonnelSex")
                        .HasColumnType("tinyint");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TrialTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("WeChatAccount")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonnelId");

                    b.ToTable("Common_PersonnelInfo");
                });

            modelBuilder.Entity("VMManageSystem.Models.VirtualMachineModel", b =>
                {
                    b.Property<int>("MachineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("MachineDiskCount")
                        .HasColumnType("float");

                    b.Property<string>("MachineIP")
                        .HasColumnType("nvarchar(11)")
                        .HasMaxLength(11);

                    b.Property<double>("MachineMemory")
                        .HasColumnType("float");

                    b.Property<string>("MachinePassword")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<byte>("MachineState")
                        .HasColumnType("tinyint");

                    b.Property<byte>("MachineSystem")
                        .HasColumnType("tinyint");

                    b.Property<string>("MachineUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MachineId");

                    b.ToTable("MachineInfo");
                });

            modelBuilder.Entity("VMManageSystem.Models.ApprovalModel", b =>
                {
                    b.HasOne("VMManageSystem.Models.VirtualMachineModel", "VirtualMachine")
                        .WithMany("Approvals")
                        .HasForeignKey("MachineInfoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VMManageSystem.Models.PersonnelModel", "Personnel")
                        .WithMany("Approvals")
                        .HasForeignKey("PersonnelId");
                });
#pragma warning restore 612, 618
        }
    }
}
