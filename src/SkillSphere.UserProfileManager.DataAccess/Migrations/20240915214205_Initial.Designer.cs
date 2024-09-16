﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SkillSphere.UserProfileManager.DataAccess;

#nullable disable

namespace SkillSphere.UserProfileManager.DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240915214205_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SkillSphere.UserProfileManager.DataAccess.Entities.GoalEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Progress")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserProfileEntityId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.HasIndex("UserProfileEntityId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("SkillSphere.UserProfileManager.DataAccess.Entities.LearningHistoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CompletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CourseTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserProfileEntityId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CourseTitle")
                        .IsUnique();

                    b.HasIndex("UserProfileEntityId");

                    b.ToTable("LearningHistory");
                });

            modelBuilder.Entity("SkillSphere.UserProfileManager.DataAccess.Entities.SkillEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserProfileEntityId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("UserProfileEntityId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("SkillSphere.UserProfileManager.DataAccess.Entities.UserProfileEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("ProfilePictureUrl")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("SkillSphere.UserProfileManager.DataAccess.Entities.GoalEntity", b =>
                {
                    b.HasOne("SkillSphere.UserProfileManager.DataAccess.Entities.UserProfileEntity", null)
                        .WithMany("Goals")
                        .HasForeignKey("UserProfileEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SkillSphere.UserProfileManager.DataAccess.Entities.LearningHistoryEntity", b =>
                {
                    b.HasOne("SkillSphere.UserProfileManager.DataAccess.Entities.UserProfileEntity", null)
                        .WithMany("LearningHistories")
                        .HasForeignKey("UserProfileEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SkillSphere.UserProfileManager.DataAccess.Entities.SkillEntity", b =>
                {
                    b.HasOne("SkillSphere.UserProfileManager.DataAccess.Entities.UserProfileEntity", null)
                        .WithMany("Skills")
                        .HasForeignKey("UserProfileEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SkillSphere.UserProfileManager.DataAccess.Entities.UserProfileEntity", b =>
                {
                    b.Navigation("Goals");

                    b.Navigation("LearningHistories");

                    b.Navigation("Skills");
                });
#pragma warning restore 612, 618
        }
    }
}
