﻿// <auto-generated />
using System;
using CodeCraftApi.Database;
using CodeCraftApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CodeCraftApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "exercise_difficulty", new[] { "easy", "hard", "medium", "unassigned" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "group_size", new[] { "group", "pair", "team" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "role", new[] { "student", "teacher", "unassigned" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "session_status", new[] { "active", "passive", "reconnecting" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "status", new[] { "active", "passive" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CategoryExercise", b =>
                {
                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("uuid")
                        .HasColumnName("categories_id");

                    b.Property<Guid>("ExercisesId")
                        .HasColumnType("uuid")
                        .HasColumnName("exercises_id");

                    b.HasKey("CategoriesId", "ExercisesId")
                        .HasName("pk_category_exercise");

                    b.HasIndex("ExercisesId")
                        .HasDatabaseName("ix_category_exercise_exercises_id");

                    b.ToTable("category_exercise", (string)null);
                });

            modelBuilder.Entity("CodeCraftApi.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("CodeCraftApi.Domain.Entities.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<ExerciseDifficulty>("ExerciseDifficulty")
                        .HasColumnType("exercise_difficulty")
                        .HasColumnName("exercise_difficulty");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_exercises");

                    b.HasIndex("AuthorId")
                        .HasDatabaseName("ix_exercises_author_id");

                    b.ToTable("exercises", (string)null);
                });

            modelBuilder.Entity("CodeCraftApi.Domain.Entities.ExerciseItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Contraints")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("contraints");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("DescriptionShort")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description_short");

                    b.Property<Guid?>("ExerciseId")
                        .HasColumnType("uuid")
                        .HasColumnName("exercise_id");

                    b.Property<string>("Hints")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hints");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_exercise_item");

                    b.HasIndex("ExerciseId")
                        .HasDatabaseName("ix_exercise_item_exercise_id");

                    b.ToTable("exercise_item", (string)null);
                });

            modelBuilder.Entity("CodeCraftApi.Domain.Entities.ExerciseStep", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Contraints")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("contraints");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("DescriptionShort")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description_short");

                    b.Property<Guid?>("ExerciseItemId")
                        .HasColumnType("uuid")
                        .HasColumnName("exercise_item_id");

                    b.Property<string>("Hints")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hints");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_exercise_step");

                    b.HasIndex("ExerciseItemId")
                        .HasDatabaseName("ix_exercise_step_exercise_item_id");

                    b.ToTable("exercise_step", (string)null);
                });

            modelBuilder.Entity("CodeCraftApi.Domain.Entities.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<GroupSize>("GroupSize")
                        .HasColumnType("group_size")
                        .HasColumnName("group_size");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_groups");

                    b.ToTable("groups", (string)null);
                });

            modelBuilder.Entity("CodeCraftApi.Domain.Entities.GroupExercise", b =>
                {
                    b.Property<Guid>("ExercisesId")
                        .HasColumnType("uuid")
                        .HasColumnName("exercises_id");

                    b.Property<Guid>("GroupsId")
                        .HasColumnType("uuid")
                        .HasColumnName("groups_id");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("boolean")
                        .HasColumnName("is_visible");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("ExercisesId", "GroupsId")
                        .HasName("pk_group_exercise");

                    b.HasIndex("GroupsId")
                        .HasDatabaseName("ix_group_exercise_groups_id");

                    b.ToTable("group_exercise", (string)null);
                });

            modelBuilder.Entity("CodeCraftApi.Domain.Entities.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<SessionStatus>("SessionStatus")
                        .HasColumnType("session_status")
                        .HasColumnName("session_status");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_sessions");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_sessions_user_id");

                    b.ToTable("sessions", (string)null);
                });

            modelBuilder.Entity("CodeCraftApi.Domain.Entities.Test", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("content");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid?>("ExerciseStepId")
                        .HasColumnType("uuid")
                        .HasColumnName("exercise_step_id");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_tests");

                    b.HasIndex("ExerciseStepId")
                        .HasDatabaseName("ix_tests_exercise_step_id");

                    b.ToTable("tests", (string)null);
                });

            modelBuilder.Entity("CodeCraftApi.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<Role>("Role")
                        .HasColumnType("role")
                        .HasColumnName("role");

                    b.Property<Status>("Status")
                        .HasColumnType("status")
                        .HasColumnName("status");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("GroupUser", b =>
                {
                    b.Property<Guid>("GroupsId")
                        .HasColumnType("uuid")
                        .HasColumnName("groups_id");

                    b.Property<Guid>("MembersId")
                        .HasColumnType("uuid")
                        .HasColumnName("members_id");

                    b.HasKey("GroupsId", "MembersId")
                        .HasName("pk_group_user");

                    b.HasIndex("MembersId")
                        .HasDatabaseName("ix_group_user_members_id");

                    b.ToTable("group_user", (string)null);
                });

            modelBuilder.Entity("CategoryExercise", b =>
                {
                    b.HasOne("CodeCraftApi.Domain.Entities.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_category_exercise_categories_categories_id");

                    b.HasOne("CodeCraftApi.Domain.Entities.Exercise", null)
                        .WithMany()
                        .HasForeignKey("ExercisesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_category_exercise_exercises_exercises_id");
                });

            modelBuilder.Entity("CodeCraftApi.Domain.Entities.Exercise", b =>
                {
                    b.HasOne("CodeCraftApi.Domain.Entities.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_exercises_users_author_id");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("CodeCraftApi.Domain.Entities.ExerciseItem", b =>
                {
                    b.HasOne("CodeCraftApi.Domain.Entities.Exercise", null)
                        .WithMany("SubExercises")
                        .HasForeignKey("ExerciseId")
                        .HasConstraintName("fk_exercise_item_exercises_exercise_id");
                });

            modelBuilder.Entity("CodeCraftApi.Domain.Entities.ExerciseStep", b =>
                {
                    b.HasOne("CodeCraftApi.Domain.Entities.ExerciseItem", null)
                        .WithMany("Steps")
                        .HasForeignKey("ExerciseItemId")
                        .HasConstraintName("fk_exercise_step_exercise_item_exercise_item_id");
                });

            modelBuilder.Entity("CodeCraftApi.Domain.Entities.GroupExercise", b =>
                {
                    b.HasOne("CodeCraftApi.Domain.Entities.Exercise", null)
                        .WithMany()
                        .HasForeignKey("ExercisesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_group_exercise_exercises_exercises_id");

                    b.HasOne("CodeCraftApi.Domain.Entities.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_group_exercise_groups_groups_id");
                });

            modelBuilder.Entity("CodeCraftApi.Domain.Entities.Session", b =>
                {
                    b.HasOne("CodeCraftApi.Domain.Entities.User", null)
                        .WithMany("Sessions")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_sessions_users_user_id");
                });

            modelBuilder.Entity("CodeCraftApi.Domain.Entities.Test", b =>
                {
                    b.HasOne("CodeCraftApi.Domain.Entities.ExerciseStep", null)
                        .WithMany("Tests")
                        .HasForeignKey("ExerciseStepId")
                        .HasConstraintName("fk_tests_exercise_step_exercise_step_id");
                });

            modelBuilder.Entity("GroupUser", b =>
                {
                    b.HasOne("CodeCraftApi.Domain.Entities.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_group_user_groups_groups_id");

                    b.HasOne("CodeCraftApi.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_group_user_users_members_id");
                });

            modelBuilder.Entity("CodeCraftApi.Domain.Entities.Exercise", b =>
                {
                    b.Navigation("SubExercises");
                });

            modelBuilder.Entity("CodeCraftApi.Domain.Entities.ExerciseItem", b =>
                {
                    b.Navigation("Steps");
                });

            modelBuilder.Entity("CodeCraftApi.Domain.Entities.ExerciseStep", b =>
                {
                    b.Navigation("Tests");
                });

            modelBuilder.Entity("CodeCraftApi.Domain.Entities.User", b =>
                {
                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
