using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassList.Data.Migrations
{
    public partial class Init_RPGClass_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RPGClass",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(nullable: false),
                    HealthPoints = table.Column<int>(nullable: false),
                    Speed = table.Column<int>(nullable: false),
                    SpecialAbilities = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RPGClass", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RPGClass");
        }
    }
}
