﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToggleOn.EntityFrameworkCore.SqlServer;

#nullable disable

namespace ToggleOn.EntityFrameworkCore.SqlServer.Migrations
{
    [DbContext(typeof(ToggleOnContext))]
    [Migration("20240127160321_NameConstraint")]
    partial class NameConstraint
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ToggleOn.Domain.Environment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasAlternateKey("ProjectId", "Name");

                    b.ToTable("Environment", (string)null);
                });

            modelBuilder.Entity("ToggleOn.Domain.Feature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasAlternateKey("ProjectId", "Name");

                    b.ToTable("Feature", (string)null);
                });

            modelBuilder.Entity("ToggleOn.Domain.FeatureFilter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FeatureToggleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FeatureToggleId");

                    b.ToTable("FeatureFilter", (string)null);
                });

            modelBuilder.Entity("ToggleOn.Domain.FeatureFilterParameter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FeatureFilterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FeatureFilterId");

                    b.ToTable("FeatureFilterParameter", (string)null);
                });

            modelBuilder.Entity("ToggleOn.Domain.FeatureGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IpAddresses")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserIds")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("FeatureGroup", (string)null);
                });

            modelBuilder.Entity("ToggleOn.Domain.FeatureToggle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<Guid>("EnvironmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FeatureGroups")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FeatureId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EnvironmentId");

                    b.HasIndex("FeatureId");

                    b.ToTable("FeatureToggle");
                });

            modelBuilder.Entity("ToggleOn.Domain.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Project", (string)null);
                });

            modelBuilder.Entity("ToggleOn.Domain.Environment", b =>
                {
                    b.HasOne("ToggleOn.Domain.Project", "Project")
                        .WithMany("Environments")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ToggleOn.Domain.Feature", b =>
                {
                    b.HasOne("ToggleOn.Domain.Project", "Project")
                        .WithMany("Features")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ToggleOn.Domain.FeatureFilter", b =>
                {
                    b.HasOne("ToggleOn.Domain.FeatureToggle", "FeatureToggle")
                        .WithMany("Filters")
                        .HasForeignKey("FeatureToggleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("FeatureToggle");
                });

            modelBuilder.Entity("ToggleOn.Domain.FeatureFilterParameter", b =>
                {
                    b.HasOne("ToggleOn.Domain.FeatureFilter", "Filter")
                        .WithMany("Parameters")
                        .HasForeignKey("FeatureFilterId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Filter");
                });

            modelBuilder.Entity("ToggleOn.Domain.FeatureToggle", b =>
                {
                    b.HasOne("ToggleOn.Domain.Environment", "Environment")
                        .WithMany("FeatureToggles")
                        .HasForeignKey("EnvironmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ToggleOn.Domain.Feature", "Feature")
                        .WithMany("Toggles")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Environment");

                    b.Navigation("Feature");
                });

            modelBuilder.Entity("ToggleOn.Domain.Environment", b =>
                {
                    b.Navigation("FeatureToggles");
                });

            modelBuilder.Entity("ToggleOn.Domain.Feature", b =>
                {
                    b.Navigation("Toggles");
                });

            modelBuilder.Entity("ToggleOn.Domain.FeatureFilter", b =>
                {
                    b.Navigation("Parameters");
                });

            modelBuilder.Entity("ToggleOn.Domain.FeatureToggle", b =>
                {
                    b.Navigation("Filters");
                });

            modelBuilder.Entity("ToggleOn.Domain.Project", b =>
                {
                    b.Navigation("Environments");

                    b.Navigation("Features");
                });
#pragma warning restore 612, 618
        }
    }
}