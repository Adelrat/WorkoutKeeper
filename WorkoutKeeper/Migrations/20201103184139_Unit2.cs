using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutKeeper.Migrations
{
    public partial class Unit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Trainings_TrainingId",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "TraningId",
                table: "Days");

            migrationBuilder.AlterColumn<int>(
                name: "TrainingId",
                table: "Days",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Trainings_TrainingId",
                table: "Days",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Trainings_TrainingId",
                table: "Days");

            migrationBuilder.AlterColumn<int>(
                name: "TrainingId",
                table: "Days",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TraningId",
                table: "Days",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Trainings_TrainingId",
                table: "Days",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
