using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class InitialQueryQuestionChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerTemplate",
                table: "ExerciseQuestion");

            migrationBuilder.DropColumn(
                name: "Hint",
                table: "ExerciseQuestion");

            migrationBuilder.DropColumn(
                name: "InstructionsTemplate",
                table: "ExerciseQuestion");

            migrationBuilder.DropColumn(
                name: "StartingSql",
                table: "ExerciseQuestion");

            migrationBuilder.AddColumn<string>(
                name: "Instructions",
                table: "ExerciseQuestion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswerQuery",
                table: "ExerciseQuestion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instructions",
                table: "ExerciseQuestion");

            migrationBuilder.DropColumn(
                name: "CorrectAnswerQuery",
                table: "ExerciseQuestion");

            migrationBuilder.AddColumn<string>(
                name: "AnswerTemplate",
                table: "ExerciseQuestion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hint",
                table: "ExerciseQuestion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstructionsTemplate",
                table: "ExerciseQuestion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartingSql",
                table: "ExerciseQuestion",
                nullable: true);
        }
    }
}
