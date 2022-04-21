using Microsoft.EntityFrameworkCore.Migrations;

namespace mniaAPI.Migrations
{
    public partial class ImageUpload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.Sql("INSERT INTO Categories(Id, Technology, Name) VALUES('1','Admin', 'Categoria de administradores')");
            migrationBuilder.Sql("INSERT INTO Categories(Id, Technology, Name) VALUES('2','Java', 'Turma 1')");
            migrationBuilder.Sql("INSERT INTO Categories(Id, Technology, Name) VALUES('3','Angular', 'Turma 3')");
            migrationBuilder.Sql("INSERT INTO Categories(Id, Technology, Name) VALUES('4','Csharp', 'Turma 6')");
            migrationBuilder.Sql("INSERT INTO Categories(Id, Technology, Name) VALUES('5','Dart', 'Turma 4')");
            migrationBuilder.Sql("INSERT INTO Categories(Id, Technology, Name) VALUES('6','Python', 'Turma 2')");

            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId, Role, FileName) VALUES('1','Admin','Admin', '46680074800','ADMI','admin@mnia.com', '11B784DBAADDF6853405FA2AA9A95D8E', '1', 'Admin', 'img1.jpg')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId, Role, FileName) VALUES('2','Clécio','Clécio', '46680074800','CLEC','clecio.silva@gft.com', '63A9F0EA7BB98050796B649E85481845', '1', 'Admin', 'img2.jpg')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId, Role, FileName) VALUES('3','Marcos Ferreira','Marcos Ferreira', '46680074800', 'MNIA', 'mnia@gft.com', '63A9F0EA7BB98050796B649E85481845', '3', 'Starter', 'img3.jpg')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId, Role, FileName) VALUES('4','João Pereira','João Pereira', '46680074800', 'JOPE', 'jope@mnia.com', '63A9F0EA7BB98050796B649E85481845', '2', 'Starter', 'img4.jpg')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId, Role, FileName) VALUES('5','Barbara Leal','Barbara Leal', '46680074800', 'BALE', 'bale@mnia.com', '63A9F0EA7BB98050796B649E85481845', '4', 'Starter', 'img5.jpg')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId, Role, FileName) VALUES('6','Gustavo Costa','Gustavo Costa', '46680074800', 'GUCO', 'guco@mnia.com', '63A9F0EA7BB98050796B649E85481845', '6', 'Starter', 'img6.jpg')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId, Role, FileName) VALUES('7','Benedito Augusto','Benedito Augusto', '46680074800', 'BEAU', 'beau@mnia.com', '63A9F0EA7BB98050796B649E85481845', '5', 'Starter', 'img7.jpg')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId, Role, FileName) VALUES('8','Godoberto Alves','Godoberto Alves', '46680074800', 'GOAL', 'goal@mnia.com', '63A9F0EA7BB98050796B649E85481845', '3', 'Starter', 'img8.jpg')");
            migrationBuilder.Sql("INSERT INTO Users(Id, Fullname, Username, CPF, FourLetters, Email, Password, CategoriesId, Role, FileName) VALUES('9','Gilso do Beloberbando','Gilso do Beloberbando', '46680074800', 'GIBE', 'gibe@mnia.com', '63A9F0EA7BB98050796B649E85481845', '4', 'Starter', 'img9.jpg')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Users");
        }
    }
}
