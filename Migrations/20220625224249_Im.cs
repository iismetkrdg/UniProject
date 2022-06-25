using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduProject.Migrations
{
    public partial class Im : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ders",
                columns: table => new
                {
                    DersId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DersKodu = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ders", x => x.DersId);
                });

            migrationBuilder.CreateTable(
                name: "DersNotu",
                columns: table => new
                {
                    DersNotuId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    DersAdı = table.Column<string>(type: "TEXT", nullable: true),
                    Konu = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersNotu", x => x.DersNotuId);
                });

            migrationBuilder.CreateTable(
                name: "Duyuru",
                columns: table => new
                {
                    DuyuruId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DuyuruName = table.Column<string>(type: "TEXT", nullable: true),
                    DuyuruText = table.Column<string>(type: "TEXT", nullable: true),
                    DuyuruTarih = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duyuru", x => x.DuyuruId);
                });

            migrationBuilder.CreateTable(
                name: "ForumBaslik",
                columns: table => new
                {
                    ForumBaslikId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Creator = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumBaslik", x => x.ForumBaslikId);
                });

            migrationBuilder.CreateTable(
                name: "ForumComment",
                columns: table => new
                {
                    ForumCommentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ForumId = table.Column<int>(type: "INTEGER", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    Creator = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumComment", x => x.ForumCommentId);
                });

            migrationBuilder.CreateTable(
                name: "Ilan",
                columns: table => new
                {
                    IlanId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Arıyor = table.Column<bool>(type: "INTEGER", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    iletisim = table.Column<string>(type: "TEXT", nullable: true),
                    atCreated = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilan", x => x.IlanId);
                });

            migrationBuilder.CreateTable(
                name: "Sınav",
                columns: table => new
                {
                    SınavId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    DersAdı = table.Column<string>(type: "TEXT", nullable: true),
                    FileName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sınav", x => x.SınavId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    AtSignUp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ders");

            migrationBuilder.DropTable(
                name: "DersNotu");

            migrationBuilder.DropTable(
                name: "Duyuru");

            migrationBuilder.DropTable(
                name: "ForumBaslik");

            migrationBuilder.DropTable(
                name: "ForumComment");

            migrationBuilder.DropTable(
                name: "Ilan");

            migrationBuilder.DropTable(
                name: "Sınav");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
