using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassList.Data.Migrations
{
    public partial class Changed_Model_Property_Name_SpecialAbility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialAbilities",
                table: "RPGClass");

            migrationBuilder.AddColumn<string>(
                name: "SpecialAbility",
                table: "RPGClass",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialAbility",
                table: "RPGClass");

            migrationBuilder.AddColumn<string>(
                name: "SpecialAbilities",
                table: "RPGClass",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
