using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DataAccess.Models;

namespace DataAccess.Migrations
{
    [DbContext(typeof(SqlConfidenceContext))]
    partial class SqlConfidenceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccess.Models.DataSource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("Deleted");

                    b.Property<string>("DeletedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Name");

                    b.Property<string>("UpdatedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("DataSources");
                });

            modelBuilder.Entity("DataAccess.Models.DataSourceTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("DataSourceId");

                    b.Property<bool>("Deleted");

                    b.Property<string>("DeletedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("TableName");

                    b.Property<string>("UpdatedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("DataSourceId");

                    b.ToTable("DataSourceTables");
                });

            modelBuilder.Entity("DataAccess.Models.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("DataSourceId");

                    b.Property<bool>("Deleted");

                    b.Property<string>("DeletedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<int>("Order");

                    b.Property<bool?>("Published");

                    b.Property<string>("PublishedBy");

                    b.Property<DateTime?>("PublishedDate");

                    b.Property<int>("SectionId");

                    b.Property<string>("Summary");

                    b.Property<string>("UpdatedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("DataSourceId");

                    b.HasIndex("SectionId");

                    b.ToTable("Exercise");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Exercise");
                });

            modelBuilder.Entity("DataAccess.Models.ExerciseQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("Deleted");

                    b.Property<string>("DeletedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Description");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Instructions");

                    b.Property<int?>("Order");

                    b.Property<string>("UpdatedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("ExerciseQuestion");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ExerciseQuestion");
                });

            modelBuilder.Entity("DataAccess.Models.ExerciseQuestionAnswered", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("ExerciseQuestionId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseQuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("ExerciseQuestionAnswereds");
                });

            modelBuilder.Entity("DataAccess.Models.MultipleChoiceDataQuery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExerciseQuestionId");

                    b.Property<string>("SqlQuery");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseQuestionId");

                    b.ToTable("MultipleChoiceDataQuery");
                });

            modelBuilder.Entity("DataAccess.Models.MultipleChoiceOption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CorrectAnswerMessage");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<int>("ExerciseQuestionId");

                    b.Property<string>("IncorrectAnswerMessage");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseQuestionId");

                    b.ToTable("MultipleChoiceOption");
                });

            modelBuilder.Entity("DataAccess.Models.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("Deleted");

                    b.Property<string>("DeletedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("UpdatedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("DataAccess.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("Deleted");

                    b.Property<string>("DeletedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsGuest");

                    b.Property<DateTime?>("LastLogin");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("UpdatedBy")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataAccess.Models.UserAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<int?>("QuestionId");

                    b.Property<DateTime?>("ResetDate");

                    b.Property<int>("UserActionTypeId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserActionTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("UserActions");
                });

            modelBuilder.Entity("DataAccess.Models.UserActionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("UserActionTypes");
                });

            modelBuilder.Entity("DataAccess.Models.MultipleChoiceExercise", b =>
                {
                    b.HasBaseType("DataAccess.Models.Exercise");


                    b.ToTable("MultipleChoiceExercise");

                    b.HasDiscriminator().HasValue("MultipleChoiceExercise");
                });

            modelBuilder.Entity("DataAccess.Models.QueryBuilderExercise", b =>
                {
                    b.HasBaseType("DataAccess.Models.Exercise");


                    b.ToTable("QueryBuilderExercise");

                    b.HasDiscriminator().HasValue("QueryBuilderExercise");
                });

            modelBuilder.Entity("DataAccess.Models.QueryExercise", b =>
                {
                    b.HasBaseType("DataAccess.Models.Exercise");


                    b.ToTable("QueryExercise");

                    b.HasDiscriminator().HasValue("QueryExercise");
                });

            modelBuilder.Entity("DataAccess.Models.UnitTestedExercise", b =>
                {
                    b.HasBaseType("DataAccess.Models.Exercise");


                    b.ToTable("UnitTestedExercise");

                    b.HasDiscriminator().HasValue("UnitTestedExercise");
                });

            modelBuilder.Entity("DataAccess.Models.MultipleChoiceQuestion", b =>
                {
                    b.HasBaseType("DataAccess.Models.ExerciseQuestion");

                    b.Property<Guid?>("CorrectOptionId");

                    b.Property<int>("MultipleChoiceExerciseId");

                    b.HasIndex("CorrectOptionId");

                    b.HasIndex("MultipleChoiceExerciseId");

                    b.ToTable("MultipleChoiceQuestion");

                    b.HasDiscriminator().HasValue("MultipleChoiceQuestion");
                });

            modelBuilder.Entity("DataAccess.Models.QueryBuilderQuestion", b =>
                {
                    b.HasBaseType("DataAccess.Models.ExerciseQuestion");

                    b.Property<int>("QueryBuilderExerciseId");

                    b.HasIndex("QueryBuilderExerciseId");

                    b.ToTable("QueryBuilderQuestion");

                    b.HasDiscriminator().HasValue("QueryBuilderQuestion");
                });

            modelBuilder.Entity("DataAccess.Models.QueryQuestion", b =>
                {
                    b.HasBaseType("DataAccess.Models.ExerciseQuestion");

                    b.Property<string>("CorrectAnswerQuery");

                    b.Property<int>("QueryExerciseId");

                    b.HasIndex("QueryExerciseId");

                    b.ToTable("QueryQuestion");

                    b.HasDiscriminator().HasValue("QueryQuestion");
                });

            modelBuilder.Entity("DataAccess.Models.UnitTestedQuestion", b =>
                {
                    b.HasBaseType("DataAccess.Models.ExerciseQuestion");

                    b.Property<int>("UnitTestedExerciseId");

                    b.HasIndex("UnitTestedExerciseId");

                    b.ToTable("UnitTestedQuestion");

                    b.HasDiscriminator().HasValue("UnitTestedQuestion");
                });

            modelBuilder.Entity("DataAccess.Models.DataSourceTable", b =>
                {
                    b.HasOne("DataAccess.Models.DataSource", "DataSource")
                        .WithMany("DataSourceTables")
                        .HasForeignKey("DataSourceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Models.Exercise", b =>
                {
                    b.HasOne("DataAccess.Models.DataSource", "DataSource")
                        .WithMany("Exercises")
                        .HasForeignKey("DataSourceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataAccess.Models.Section", "Section")
                        .WithMany("Exercises")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Models.ExerciseQuestionAnswered", b =>
                {
                    b.HasOne("DataAccess.Models.ExerciseQuestion", "ExerciseQuestion")
                        .WithMany("ExerciseQuestionAnswereds")
                        .HasForeignKey("ExerciseQuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataAccess.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Models.MultipleChoiceDataQuery", b =>
                {
                    b.HasOne("DataAccess.Models.MultipleChoiceQuestion", "Question")
                        .WithMany("DataQueries")
                        .HasForeignKey("ExerciseQuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Models.MultipleChoiceOption", b =>
                {
                    b.HasOne("DataAccess.Models.MultipleChoiceQuestion", "ExerciseQuestion")
                        .WithMany("Options")
                        .HasForeignKey("ExerciseQuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Models.UserAction", b =>
                {
                    b.HasOne("DataAccess.Models.ExerciseQuestion", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId");

                    b.HasOne("DataAccess.Models.UserActionType", "UserActionType")
                        .WithMany("UserActions")
                        .HasForeignKey("UserActionTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataAccess.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Models.MultipleChoiceQuestion", b =>
                {
                    b.HasOne("DataAccess.Models.MultipleChoiceOption", "CorrectOption")
                        .WithMany()
                        .HasForeignKey("CorrectOptionId");

                    b.HasOne("DataAccess.Models.MultipleChoiceExercise", "Exercise")
                        .WithMany("ExerciseQuestions")
                        .HasForeignKey("MultipleChoiceExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Models.QueryBuilderQuestion", b =>
                {
                    b.HasOne("DataAccess.Models.QueryBuilderExercise", "Exercise")
                        .WithMany("ExerciseQuestions")
                        .HasForeignKey("QueryBuilderExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Models.QueryQuestion", b =>
                {
                    b.HasOne("DataAccess.Models.QueryExercise", "Exercise")
                        .WithMany("ExerciseQuestions")
                        .HasForeignKey("QueryExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccess.Models.UnitTestedQuestion", b =>
                {
                    b.HasOne("DataAccess.Models.UnitTestedExercise", "Exercise")
                        .WithMany("ExerciseQuestions")
                        .HasForeignKey("UnitTestedExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
