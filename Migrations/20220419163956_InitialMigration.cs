using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mniaAPI.Migrations
{
    public partial class InitialMigration : Migration
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
                    CategoriesId = table.Column<int>(type: "int", nullable: true),
                    Role = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
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

            migrationBuilder.Sql("INSERT INTO Categories(Id, Technology, Name) VALUES('1','Admin', 'ADM')");
            migrationBuilder.Sql("INSERT INTO Categories(Id, Technology, Name) VALUES('2','Java', 'Turma 5')");
            migrationBuilder.Sql("INSERT INTO Categories(Id, Technology, Name) VALUES('3','Angular', 'Turma 3')");
            migrationBuilder.Sql("INSERT INTO Categories(Id, Technology, Name) VALUES('4','Csharp dotnet', 'Turma 6')");
            migrationBuilder.Sql("INSERT INTO Categories(Id, Technology, Name) VALUES('5','Dart', 'Turma 4')");
            migrationBuilder.Sql("INSERT INTO Categories(Id, Technology, Name) VALUES('6','Python', 'Turma 2')");

            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId, Role) VALUES('1','Admin','Admin', '46680074800','ADMI','admin@gft.com', '11B784DBAADDF6853405FA2AA9A95D8E', '1', 'Admin')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId, Role) VALUES('2','Clécio','Clécio', '46680074800','CLEC','clecio@gft.com', '63A9F0EA7BB98050796B649E85481845', '1', 'Admin')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId, Role) VALUES('3','Marcos Ferreira','Marcos Ferreira', '46680074800', 'MNIA', 'mnia@gft.com', '63A9F0EA7BB98050796B649E85481845', '3', 'Starter')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId, Role) VALUES('4','João Pereira','João Pereira', '46680074800', 'JOPE', 'jope@gft.com', '63A9F0EA7BB98050796B649E85481845', '2', 'Starter')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId, Role) VALUES('5','Barbara Leal','Barbara Leal', '46680074800', 'BALE', 'bale@gft.com', '63A9F0EA7BB98050796B649E85481845', '4', 'Starter')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId, Role) VALUES('6','Gustavo Costa','Gustavo Costa', '46680074800', 'GUCO', 'guco@gft.com', '63A9F0EA7BB98050796B649E85481845', '6', 'Starter')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId, Role) VALUES('7','Benedito Augusto','Benedito Augusto', '46680074800', 'BEAU', 'beau@gft.com', '63A9F0EA7BB98050796B649E85481845', '5', 'Starter')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId, Role) VALUES('8','Godoberto Alves','Godoberto Alves', '46680074800', 'GOAL', 'goal@gft.com', '63A9F0EA7BB98050796B649E85481845', '3', 'Starter')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId, Role) VALUES('9','Gilso do Beloberbando','Gilso do Beloberbando', '46680074800', 'GIBE', 'gibe@gft.com', '63A9F0EA7BB98050796B649E85481845', '4', 'Starter')");

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
