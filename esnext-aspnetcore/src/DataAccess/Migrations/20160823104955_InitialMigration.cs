using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataSources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false),
                    IsGuest = table.Column<bool>(nullable: false),
                    LastLogin = table.Column<DateTime>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserActionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataSourceTables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DataSourceId = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    TableName = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSourceTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataSourceTables_DataSources_DataSourceId",
                        column: x => x.DataSourceId,
                        principalTable: "DataSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DataSourceId = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Published = table.Column<bool>(nullable: true),
                    PublishedBy = table.Column<string>(nullable: true),
                    PublishedDate = table.Column<DateTime>(nullable: true),
                    SectionName = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercise_DataSources_DataSourceId",
                        column: x => x.DataSourceId,
                        principalTable: "DataSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerTemplate = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Hint = table.Column<string>(nullable: true),
                    InstructionsTemplate = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: true),
                    StartingSql = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    CorrectOptionId = table.Column<Guid>(nullable: true),
                    MultipleChoiceExerciseId = table.Column<int>(nullable: true),
                    QueryBuilderExerciseId = table.Column<int>(nullable: true),
                    QueryExerciseId = table.Column<int>(nullable: true),
                    UnitTestedExerciseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseQuestion_Exercise_MultipleChoiceExerciseId",
                        column: x => x.MultipleChoiceExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExerciseQuestion_Exercise_QueryBuilderExerciseId",
                        column: x => x.QueryBuilderExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExerciseQuestion_Exercise_QueryExerciseId",
                        column: x => x.QueryExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExerciseQuestion_Exercise_UnitTestedExerciseId",
                        column: x => x.UnitTestedExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExerciseQuestionAnswereds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ExerciseQuestionId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseQuestionAnswereds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseQuestionAnswereds_ExerciseQuestion_ExerciseQuestionId",
                        column: x => x.ExerciseQuestionId,
                        principalTable: "ExerciseQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseQuestionAnswereds_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultipleChoiceDataQuery",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExerciseQuestionId = table.Column<int>(nullable: false),
                    SqlQuery = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleChoiceDataQuery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultipleChoiceDataQuery_ExerciseQuestion_ExerciseQuestionId",
                        column: x => x.ExerciseQuestionId,
                        principalTable: "ExerciseQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultipleChoiceOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CorrectAnswerMessage = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 20, nullable: true),
                    ExerciseQuestionId = table.Column<int>(nullable: false),
                    IncorrectAnswerMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleChoiceOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultipleChoiceOption_ExerciseQuestion_ExerciseQuestionId",
                        column: x => x.ExerciseQuestionId,
                        principalTable: "ExerciseQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserActions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    QuestionId = table.Column<int>(nullable: true),
                    ResetDate = table.Column<DateTime>(nullable: true),
                    UserActionTypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserActions_ExerciseQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "ExerciseQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserActions_UserActionTypes_UserActionTypeId",
                        column: x => x.UserActionTypeId,
                        principalTable: "UserActionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserActions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataSourceTables_DataSourceId",
                table: "DataSourceTables",
                column: "DataSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_DataSourceId",
                table: "Exercise",
                column: "DataSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseQuestion_CorrectOptionId",
                table: "ExerciseQuestion",
                column: "CorrectOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseQuestion_MultipleChoiceExerciseId",
                table: "ExerciseQuestion",
                column: "MultipleChoiceExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseQuestion_QueryBuilderExerciseId",
                table: "ExerciseQuestion",
                column: "QueryBuilderExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseQuestion_QueryExerciseId",
                table: "ExerciseQuestion",
                column: "QueryExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseQuestion_UnitTestedExerciseId",
                table: "ExerciseQuestion",
                column: "UnitTestedExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseQuestionAnswereds_ExerciseQuestionId",
                table: "ExerciseQuestionAnswereds",
                column: "ExerciseQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseQuestionAnswereds_UserId",
                table: "ExerciseQuestionAnswereds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoiceDataQuery_ExerciseQuestionId",
                table: "MultipleChoiceDataQuery",
                column: "ExerciseQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoiceOption_ExerciseQuestionId",
                table: "MultipleChoiceOption",
                column: "ExerciseQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActions_QuestionId",
                table: "UserActions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActions_UserActionTypeId",
                table: "UserActions",
                column: "UserActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActions_UserId",
                table: "UserActions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseQuestion_MultipleChoiceOption_CorrectOptionId",
                table: "ExerciseQuestion",
                column: "CorrectOptionId",
                principalTable: "MultipleChoiceOption",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_DataSources_DataSourceId",
                table: "Exercise");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseQuestion_MultipleChoiceOption_CorrectOptionId",
                table: "ExerciseQuestion");

            migrationBuilder.DropTable(
                name: "DataSourceTables");

            migrationBuilder.DropTable(
                name: "ExerciseQuestionAnswereds");

            migrationBuilder.DropTable(
                name: "MultipleChoiceDataQuery");

            migrationBuilder.DropTable(
                name: "UserActions");

            migrationBuilder.DropTable(
                name: "UserActionTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "DataSources");

            migrationBuilder.DropTable(
                name: "MultipleChoiceOption");

            migrationBuilder.DropTable(
                name: "ExerciseQuestion");

            migrationBuilder.DropTable(
                name: "Exercise");
        }
    }
}
