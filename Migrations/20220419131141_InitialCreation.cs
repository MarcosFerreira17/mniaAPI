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
                    Username = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CPF = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FourLetters = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId) VALUES('1','Marcos Ferreira','Marcos Ferreira', '46680074800', 'MNIA', 'mnia@gft.com', '63A9F0EA7BB98050796B649E85481845', '1')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId) VALUES('2','Admin','Admin', '46680074800','AMID','admin@gft.com', '11B784DBAADDF6853405FA2AA9A95D8E', '1')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username,CPF, FourLetters, Email, Password, CategoriesId) VALUES('3','Clécio','Clécio', '46680074800','CLIO','clecio@gft.com', '63A9F0EA7BB98050796B649E85481845', '1')");

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
