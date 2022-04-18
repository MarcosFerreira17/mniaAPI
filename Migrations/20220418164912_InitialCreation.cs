using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mniaAPI.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Technology = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoriesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CategoriesId",
                table: "Users",
                column: "CategoriesId");

            migrationBuilder.Sql("INSERT INTO Categories(Id, Technology, Name) VALUES('1','Csharp dotnet', 'Turma 1')");
            migrationBuilder.Sql("INSERT INTO Categories(Id, Technology, Name) VALUES('2','Java', 'Turma 5')");
            migrationBuilder.Sql("INSERT INTO Categories(Id, Technology, Name) VALUES('3','Angular', 'Turma 3')");
            migrationBuilder.Sql("INSERT INTO Categories(Id, Technology, Name) VALUES('4','ReactJS', 'Turma 6')");
            migrationBuilder.Sql("INSERT INTO Categories(Id, Technology, Name) VALUES('5','Dart', 'Turma 4')");
            migrationBuilder.Sql("INSERT INTO Categories(Id, Technology, Name) VALUES('6','Python', 'Turma 2')");

            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Email, Password) VALUES('1','Marcos Ferreira', 'mnia@gft.com', 'root')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Email, Password) VALUES('2','Admin', 'admin@gft.com', 'Gft@1234')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Email, Password) VALUES('3','Clécio', 'clecio@gft.com', 'root')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
