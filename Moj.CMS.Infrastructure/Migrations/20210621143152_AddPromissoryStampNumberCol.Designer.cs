﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Moj.CMS.Infrastructure.Contexts;

namespace Moj.CMS.Infrastructure.Migrations
{
    [DbContext(typeof(CMSDbContext))]
    [Migration("20210621143152_AddPromissoryStampNumberCol")]
    partial class AddPromissoryStampNumberCol
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("CMS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.HasSequence("CaseSequenceHilo")
                .IncrementsBy(10);

            modelBuilder.HasSequence("PartyHilo")
                .IncrementsBy(10);

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Application.Models.Audit.Audit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AffectedColumns")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatorUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifierUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("NewValues")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("OldValues")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PrimaryKey")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("TableName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Type")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("UserId")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("AuditTrails");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Aggregates.Case.Case", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:HiLoSequenceName", "CaseSequenceHilo")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("CaseNumber")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("CaseStatusId")
                        .HasColumnType("int");

                    b.Property<int>("CaseTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatorUserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("DeleterUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletionTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCreateIban")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifierUserId")
                        .HasColumnType("bigint");

                    b.Property<int>("NumberOfAccused")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfComplaint")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfPromissory")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfSadad")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Case");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Aggregates.Party.PartyBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:HiLoSequenceName", "PartyHilo")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatorUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("CurrentIdentityNumber")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<long?>("DeleterUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletionTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("FullName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsIndividual")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifierUserId")
                        .HasColumnType("bigint");

                    b.Property<int>("NationalityId")
                        .HasColumnType("int");

                    b.Property<int>("PartyLocationId")
                        .HasColumnType("int");

                    b.Property<int>("PartyStatusId")
                        .HasColumnType("int");

                    b.Property<int>("PartyTypeId")
                        .HasColumnType("int");

                    b.Property<string>("ShortName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Party");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PartyBase");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Shared.LookupModels.CaseStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatorUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifierUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("CaseStatuses");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Shared.LookupModels.CaseType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatorUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifierUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("CaseTypes");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Shared.LookupModels.Court", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatorUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifierUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Courts");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Shared.LookupModels.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatorUserId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("ExRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifierUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Shared.LookupModels.Division", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("CourtId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatorUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifierUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Divisions");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Shared.LookupModels.Judge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatorUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifierUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Judges");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Shared.LookupModels.Nationality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("A2")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("A3")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Code")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatorUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifierUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Nationality");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Shared.LookupModels.PartyFinancialType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatorUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifierUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("PartyFinancialTypes");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Shared.LookupModels.PartyIdentityType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatorUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifierUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("PartyIdentityTypes");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Shared.LookupModels.PartyLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatorUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifierUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("PartyLocations");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Shared.LookupModels.PartyRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatorUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifierUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("PartyRoles");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Shared.LookupModels.PartyStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatorUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifierUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("PartyStatuses");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Shared.LookupModels.PartyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatorUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifierUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("PartyTypes");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Shared.LookupModels.PromissoryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatorUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifierUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("PromissoryTypes");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Shared.LookupModels.RequestTerminationReasons", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatorUserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LastModifierUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("RequestTerminationReasons");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Aggregates.Party.PartyCorporate", b =>
                {
                    b.HasBaseType("BlazorHero.CleanArchitecture.Domain.Aggregates.Party.PartyBase");

                    b.Property<string>("CommercialRegistry")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasDiscriminator().HasValue("PartyCorporate");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Aggregates.Party.PartyIndividual", b =>
                {
                    b.HasBaseType("BlazorHero.CleanArchitecture.Domain.Aggregates.Party.PartyBase");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SecondName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ThirdName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasDiscriminator().HasValue("PartyIndividual");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Aggregates.Case.Case", b =>
                {
                    b.OwnsMany("BlazorHero.CleanArchitecture.Domain.Aggregates.Case.Entities.CasePromissory", "Promissories", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("CaseId")
                                .HasColumnType("int");

                            b1.Property<DateTime>("CreationTime")
                                .HasColumnType("datetime2");

                            b1.Property<long?>("CreatorUserId")
                                .HasColumnType("bigint");

                            b1.Property<bool>("IsActive")
                                .HasColumnType("bit");

                            b1.Property<DateTime>("IssueDate")
                                .HasColumnType("date");

                            b1.Property<DateTime?>("LastModificationTime")
                                .HasColumnType("datetime2");

                            b1.Property<long?>("LastModifierUserId")
                                .HasColumnType("bigint");

                            b1.Property<string>("PromissoryNumber")
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)");

                            b1.Property<string>("PromissoryStampNumber")
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)");

                            b1.Property<int>("PromissoryTypeId")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("CaseId");

                            b1.ToTable("Promissory");

                            b1.WithOwner()
                                .HasForeignKey("CaseId");
                        });

                    b.OwnsMany("BlazorHero.CleanArchitecture.Domain.Aggregates.Case.Entities.CaseVIban", "VIbanList", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Alias")
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)");

                            b1.Property<decimal?>("CAP")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<int>("CaseId")
                                .HasColumnType("int");

                            b1.Property<DateTime>("CreationTime")
                                .HasColumnType("datetime2");

                            b1.Property<long?>("CreatorUserId")
                                .HasColumnType("bigint");

                            b1.Property<bool>("IsActive")
                                .HasColumnType("bit");

                            b1.Property<DateTime>("IssueDate")
                                .HasColumnType("date");

                            b1.Property<DateTime?>("LastModificationTime")
                                .HasColumnType("datetime2");

                            b1.Property<long?>("LastModifierUserId")
                                .HasColumnType("bigint");

                            b1.Property<string>("VIban")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)");

                            b1.HasKey("Id");

                            b1.HasIndex("CaseId");

                            b1.ToTable("VIban");

                            b1.WithOwner()
                                .HasForeignKey("CaseId");
                        });

                    b.OwnsMany("BlazorHero.CleanArchitecture.Domain.Aggregates.Case.ValueObjects.CaseDetails", "CaseDetails", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("CaseId")
                                .HasColumnType("int");

                            b1.Property<int>("CourtId")
                                .HasColumnType("int");

                            b1.Property<DateTime>("CreationTime")
                                .HasColumnType("datetime2");

                            b1.Property<long?>("CreatorUserId")
                                .HasColumnType("bigint");

                            b1.Property<int>("DivisionId")
                                .HasColumnType("int");

                            b1.Property<DateTime>("IssueDate")
                                .HasColumnType("date");

                            b1.Property<string>("IssueDateHijri")
                                .HasMaxLength(12)
                                .HasColumnType("nvarchar(12)");

                            b1.Property<TimeSpan>("IssueTime")
                                .HasColumnType("time(7)");

                            b1.Property<int>("JudgeId")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("CaseId");

                            b1.ToTable("CaseDetails");

                            b1.WithOwner()
                                .HasForeignKey("CaseId");
                        });

                    b.OwnsMany("BlazorHero.CleanArchitecture.Domain.Aggregates.Case.ValueObjects.CaseParty", "CaseParties", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("CaseId")
                                .HasColumnType("int");

                            b1.Property<DateTime>("CreationTime")
                                .HasColumnType("datetime2");

                            b1.Property<long?>("CreatorUserId")
                                .HasColumnType("bigint");

                            b1.Property<int>("PartyFinancialTypeId")
                                .HasColumnType("int");

                            b1.Property<int>("PartyId")
                                .HasColumnType("int");

                            b1.Property<int>("PartyRoleTypeId")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("CaseId");

                            b1.ToTable("CaseParty");

                            b1.WithOwner()
                                .HasForeignKey("CaseId");
                        });

                    b.Navigation("CaseDetails");

                    b.Navigation("CaseParties");

                    b.Navigation("Promissories");

                    b.Navigation("VIbanList");
                });

            modelBuilder.Entity("BlazorHero.CleanArchitecture.Domain.Aggregates.Party.PartyBase", b =>
                {
                    b.OwnsMany("BlazorHero.CleanArchitecture.Domain.Aggregates.Party.PartyIdentity", "PartyIdentities", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime>("CreationTime")
                                .HasColumnType("datetime2");

                            b1.Property<long?>("CreatorUserId")
                                .HasColumnType("bigint");

                            b1.Property<bool>("IsActive")
                                .HasColumnType("bit");

                            b1.Property<int>("PartyId")
                                .HasColumnType("int");

                            b1.Property<string>("PartyIdentityNumber")
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)");

                            b1.Property<int>("PartyIdentityTypeId")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("PartyId");

                            b1.ToTable("PartyIdentity");

                            b1.WithOwner()
                                .HasForeignKey("PartyId");
                        });

                    b.Navigation("PartyIdentities");
                });
#pragma warning restore 612, 618
        }
    }
}
