﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VMMachineManage.Data;

namespace VMMachineManage.Migrations
{
    [DbContext(typeof(VMMachineManageContext))]
    partial class VMMachineManageContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VMMachineManage.Models.Common_DepartmentModel", b =>
                {
                    b.Property<int>("DepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DepName")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<int>("ParentNumber")
                        .HasColumnType("int");

                    b.HasKey("DepId");

                    b.ToTable("Common_Department");
                });

            modelBuilder.Entity("VMMachineManage.Models.Common_PersonnelInfoModel", b =>
                {
                    b.Property<int>("PersonnelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Degree")
                        .HasColumnType("int")
                        .HasMaxLength(1);

                    b.Property<int>("DepId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdentityCard")
                        .HasColumnType("nvarchar(18)")
                        .HasMaxLength(18);

                    b.Property<int>("IsSecrecy")
                        .HasColumnType("int")
                        .HasMaxLength(1);

                    b.Property<int>("IsStruggle")
                        .HasColumnType("int")
                        .HasMaxLength(1);

                    b.Property<int>("IsWork")
                        .HasColumnType("int")
                        .HasMaxLength(1);

                    b.Property<string>("LiveAddress")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Mailbox")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int>("MaritalStatus")
                        .HasColumnType("int")
                        .HasMaxLength(1);

                    b.Property<string>("Nation")
                        .HasColumnType("nvarchar(6)")
                        .HasMaxLength(6);

                    b.Property<DateTime>("OnBoarDingTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PassWord")
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.Property<string>("PersonnelName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("PersonnelNo")
                        .HasColumnType("int");

                    b.Property<int>("PersonnelSex")
                        .HasColumnType("int")
                        .HasMaxLength(1);

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<DateTime>("TrialTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("WeChatAccount")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("PersonnelId");

                    b.ToTable("Common_PersonnelInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
