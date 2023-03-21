﻿// <auto-generated />
using System;
using Domain.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    [DbContext(typeof(EFDbContext))]
    [Migration("20230309114859_CharacterPreferenceTable")]
    partial class CharacterPreferenceTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Career", b =>
                {
                    b.Property<int>("CareerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CareerId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CareerId");

                    b.ToTable("Careers");
                });

            modelBuilder.Entity("Domain.Entities.Character", b =>
                {
                    b.Property<int>("CharacterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CharacterId"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<int?>("Career")
                        .HasColumnType("integer");

                    b.Property<int?>("Chronotype")
                        .HasColumnType("integer");

                    b.Property<int?>("Clothes")
                        .HasColumnType("integer");

                    b.Property<int?>("Color")
                        .HasColumnType("integer");

                    b.Property<int?>("Decor")
                        .HasColumnType("integer");

                    b.Property<int>("Family")
                        .HasColumnType("integer");

                    b.Property<int>("Generation")
                        .HasColumnType("integer");

                    b.Property<bool>("Glasses")
                        .HasColumnType("boolean");

                    b.Property<int>("Goal")
                        .HasColumnType("integer");

                    b.Property<int?>("Hobby")
                        .HasColumnType("integer");

                    b.Property<bool>("InFamily")
                        .HasColumnType("boolean");

                    b.Property<int?>("Music")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Parent1")
                        .HasColumnType("integer");

                    b.Property<int>("Parent2")
                        .HasColumnType("integer");

                    b.Property<int>("Partner")
                        .HasColumnType("integer");

                    b.HasKey("CharacterId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("Domain.Entities.CharacterPreference", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsLike")
                        .HasColumnType("boolean");

                    b.Property<int>("PreferenceId")
                        .HasColumnType("integer");

                    b.ToTable("CharacterPreferences");
                });

            modelBuilder.Entity("Domain.Entities.Family", b =>
                {
                    b.Property<int>("FamilyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FamilyId"));

                    b.Property<int>("Generation")
                        .HasColumnType("integer");

                    b.Property<int>("Inheritance1")
                        .HasColumnType("integer");

                    b.Property<int>("Inheritance2")
                        .HasColumnType("integer");

                    b.Property<int>("Inheritance3")
                        .HasColumnType("integer");

                    b.Property<int>("Inheritance4")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("FamilyId");

                    b.ToTable("Families");
                });

            modelBuilder.Entity("Domain.Entities.Goal", b =>
                {
                    b.Property<int>("GoalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GoalId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsChild")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("GoalId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("Domain.Entities.InheritanceLaw", b =>
                {
                    b.Property<int>("InheritanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("InheritanceId"));

                    b.Property<bool>("AllowsManualChoice")
                        .HasColumnType("boolean");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsStrict")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("InheritanceId");

                    b.ToTable("InheritanceLaws");
                });

            modelBuilder.Entity("Domain.Entities.Msg", b =>
                {
                    b.Property<int>("MsgId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MsgId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MsgEn")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MsgRu")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("MsgId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Domain.Entities.Preference", b =>
                {
                    b.Property<int>("PreferenceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PreferenceId"));

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MinAge")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PreferenceId");

                    b.ToTable("Preferences");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<Guid>("ConfirmationToken")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
