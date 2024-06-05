﻿// <auto-generated />
using MapaAndMaya.Services.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MapaAndMaya.PostGresSql.Migrations.Migrations
{
    [DbContext(typeof(MapaAndMayaDbContext))]
    [Migration("20240525185454_AddRefactorizedModel")]
    partial class AddRefactorizedModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MapaAndMaya.Services.Models.AcademicCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("AcademicCourses");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.AcademicYear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Order")
                        .IsUnique();

                    b.ToTable("AcademicYears");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AcademicCourseId")
                        .HasColumnType("integer");

                    b.Property<int>("DegreeModalityId")
                        .HasColumnType("integer");

                    b.Property<int>("StudyPlanId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AcademicCourseId");

                    b.HasIndex("StudyPlanId");

                    b.HasIndex("DegreeModalityId", "StudyPlanId", "AcademicCourseId")
                        .IsUnique();

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.Degree", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Degrees");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.DegreeModality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DegreeId")
                        .HasColumnType("integer");

                    b.Property<int>("ModalityId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DegreeId");

                    b.HasIndex("ModalityId", "DegreeId")
                        .IsUnique();

                    b.ToTable("DegreeModalities");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.Modality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Modalities");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.Period", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Periods");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.Sede", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("SedeTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("TownId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("SedeTypeId");

                    b.HasIndex("TownId");

                    b.ToTable("Sedes");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.SedeCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<int>("SedeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("SedeId");

                    b.ToTable("SedeCourses");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.SedeType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("SedeTypes");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.StudyPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("StudyPlans");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.Town", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.YearsInCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AcademicYearId")
                        .HasColumnType("integer");

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AcademicYearId");

                    b.HasIndex("CourseId", "AcademicYearId")
                        .IsUnique();

                    b.ToTable("YearsInCourses");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.Course", b =>
                {
                    b.HasOne("MapaAndMaya.Services.Models.AcademicCourse", "AcademicCourse")
                        .WithMany("Courses")
                        .HasForeignKey("AcademicCourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MapaAndMaya.Services.Models.DegreeModality", "DegreeModality")
                        .WithMany("Courses")
                        .HasForeignKey("DegreeModalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MapaAndMaya.Services.Models.StudyPlan", "StudyPlan")
                        .WithMany("Courses")
                        .HasForeignKey("StudyPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AcademicCourse");

                    b.Navigation("DegreeModality");

                    b.Navigation("StudyPlan");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.DegreeModality", b =>
                {
                    b.HasOne("MapaAndMaya.Services.Models.Degree", "Degree")
                        .WithMany("DegreeModalities")
                        .HasForeignKey("DegreeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MapaAndMaya.Services.Models.Modality", "Modality")
                        .WithMany()
                        .HasForeignKey("ModalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Degree");

                    b.Navigation("Modality");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.Sede", b =>
                {
                    b.HasOne("MapaAndMaya.Services.Models.SedeType", "Type")
                        .WithMany("FacultyFilials")
                        .HasForeignKey("SedeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MapaAndMaya.Services.Models.Town", "Town")
                        .WithMany("FacultyFilials")
                        .HasForeignKey("TownId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Town");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.SedeCourse", b =>
                {
                    b.HasOne("MapaAndMaya.Services.Models.Course", "Course")
                        .WithMany("SedeCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MapaAndMaya.Services.Models.Sede", "Sede")
                        .WithMany("SedeCourses")
                        .HasForeignKey("SedeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Sede");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.YearsInCourse", b =>
                {
                    b.HasOne("MapaAndMaya.Services.Models.AcademicYear", "AcademicYear")
                        .WithMany("YearsInCourse")
                        .HasForeignKey("AcademicYearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MapaAndMaya.Services.Models.Course", "Course")
                        .WithMany("YearsInCourse")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AcademicYear");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.AcademicCourse", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.AcademicYear", b =>
                {
                    b.Navigation("YearsInCourse");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.Course", b =>
                {
                    b.Navigation("SedeCourses");

                    b.Navigation("YearsInCourse");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.Degree", b =>
                {
                    b.Navigation("DegreeModalities");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.DegreeModality", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.Sede", b =>
                {
                    b.Navigation("SedeCourses");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.SedeType", b =>
                {
                    b.Navigation("FacultyFilials");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.StudyPlan", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("MapaAndMaya.Services.Models.Town", b =>
                {
                    b.Navigation("FacultyFilials");
                });
#pragma warning restore 612, 618
        }
    }
}
