using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniApi.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionOptions_question_bank_QuestionId",
                table: "QuestionOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionOptions",
                table: "QuestionOptions");

            migrationBuilder.RenameTable(
                name: "QuestionOptions",
                newName: "question_options");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionOptions_QuestionId",
                table: "question_options",
                newName: "IX_question_options_QuestionId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "question_bank",
                type: "timestamp(6)",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(6)",
                oldRowVersion: true,
                oldNullable: true)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "banner",
                type: "timestamp(6)",
                rowVersion: true,
                nullable: true)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "answer_result_record",
                type: "timestamp(6)",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(6)",
                oldRowVersion: true,
                oldNullable: true)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "question_options",
                type: "timestamp(6)",
                rowVersion: true,
                nullable: true)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_question_options",
                table: "question_options",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_question_options_question_bank_QuestionId",
                table: "question_options",
                column: "QuestionId",
                principalTable: "question_bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_question_options_question_bank_QuestionId",
                table: "question_options");

            migrationBuilder.DropPrimaryKey(
                name: "PK_question_options",
                table: "question_options");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "banner");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "question_options");

            migrationBuilder.RenameTable(
                name: "question_options",
                newName: "QuestionOptions");

            migrationBuilder.RenameIndex(
                name: "IX_question_options_QuestionId",
                table: "QuestionOptions",
                newName: "IX_QuestionOptions_QuestionId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "question_bank",
                type: "timestamp(6)",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(6)",
                oldRowVersion: true,
                oldNullable: true)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "answer_result_record",
                type: "timestamp(6)",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(6)",
                oldRowVersion: true,
                oldNullable: true)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionOptions",
                table: "QuestionOptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionOptions_question_bank_QuestionId",
                table: "QuestionOptions",
                column: "QuestionId",
                principalTable: "question_bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
