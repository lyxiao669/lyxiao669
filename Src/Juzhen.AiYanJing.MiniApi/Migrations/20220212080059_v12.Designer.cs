﻿// <auto-generated />
using System;
using Juzhen.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Juzhen.AiYanJing.MiniApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220212080059_v12")]
    partial class v12
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("Juzhen.Domain.Aggregates.Achievement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Mark")
                        .HasColumnType("int");

                    b.Property<string>("Result")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("achievement");
                });

            modelBuilder.Entity("Juzhen.Domain.Aggregates.AnswerResultRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("QuestionOption")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("answer_result_record");
                });

            modelBuilder.Entity("Juzhen.Domain.Aggregates.Banner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Img")
                        .HasColumnType("longtext");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp(6)");

                    b.HasKey("Id");

                    b.ToTable("banner");
                });

            modelBuilder.Entity("Juzhen.Domain.Aggregates.IdentityUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Age")
                        .HasColumnType("longtext");

                    b.Property<string>("CompositePhoto")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FullName")
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .HasColumnType("longtext");

                    b.Property<string>("Grade")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsSee")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Mobile")
                        .HasColumnType("longtext");

                    b.Property<string>("Photo")
                        .HasColumnType("longtext");

                    b.Property<string>("QRCode")
                        .HasColumnType("longtext");

                    b.Property<string>("School")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp(6)");

                    b.HasKey("Id");

                    b.ToTable("identity_user");
                });

            modelBuilder.Entity("Juzhen.Domain.Aggregates.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Problem")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp(6)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("question");
                });

            modelBuilder.Entity("Juzhen.Domain.Aggregates.QuestionOptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsAnswer")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Option")
                        .HasColumnType("longtext");

                    b.Property<string>("OptionsAnswer")
                        .HasColumnType("longtext");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp(6)");

                    b.HasKey("Id");

                    b.ToTable("question_options");
                });
#pragma warning restore 612, 618
        }
    }
}
