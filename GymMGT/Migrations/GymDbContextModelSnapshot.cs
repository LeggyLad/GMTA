﻿// <auto-generated />
using System;
using GymMGT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GymMGT.Migrations
{
    [DbContext(typeof(GymDbContext))]
    partial class GymDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("GymMGT.Models.BloodGroup", b =>
                {
                    b.Property<int>("BloodGroupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("BloodGroupName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BloodGroupID");

                    b.ToTable("BloodGroups");
                });

            modelBuilder.Entity("GymMGT.Models.GymTrainee", b =>
                {
                    b.Property<int>("TraineeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("BloodGroupID")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Height")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("TrainingLevelID")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("TraineeId");

                    b.HasIndex("BloodGroupID");

                    b.HasIndex("TrainingLevelID");

                    b.ToTable("Trainees");
                });

            modelBuilder.Entity("GymMGT.Models.MonthlyFeeVoucher", b =>
                {
                    b.Property<int>("MonthlyFeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Date")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TraineeID")
                        .HasColumnType("int");

                    b.HasKey("MonthlyFeeID");

                    b.HasIndex("TraineeID");

                    b.ToTable("MonthlyFeeVouchers");
                });

            modelBuilder.Entity("GymMGT.Models.TrainingLevel", b =>
                {
                    b.Property<int>("TrainingLevelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("TrainingLevelName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TrainingLevelID");

                    b.ToTable("TrainingLevels");
                });

            modelBuilder.Entity("GymMGT.Models.GymTrainee", b =>
                {
                    b.HasOne("GymMGT.Models.BloodGroup", "BloodGroup")
                        .WithMany("GymTrainees")
                        .HasForeignKey("BloodGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GymMGT.Models.TrainingLevel", "TrainingLevel")
                        .WithMany("GymTrainees")
                        .HasForeignKey("TrainingLevelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BloodGroup");

                    b.Navigation("TrainingLevel");
                });

            modelBuilder.Entity("GymMGT.Models.MonthlyFeeVoucher", b =>
                {
                    b.HasOne("GymMGT.Models.GymTrainee", "GymTrainee")
                        .WithMany()
                        .HasForeignKey("TraineeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GymTrainee");
                });

            modelBuilder.Entity("GymMGT.Models.BloodGroup", b =>
                {
                    b.Navigation("GymTrainees");
                });

            modelBuilder.Entity("GymMGT.Models.TrainingLevel", b =>
                {
                    b.Navigation("GymTrainees");
                });
#pragma warning restore 612, 618
        }
    }
}
