using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeAnimals.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    animalID = table.Column<int>(type: "int", nullable: true),
                    animalName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    animalKind = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    animalGender = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    animalBirthDate = table.Column<DateTime>(type: "date", nullable: true),
                    animalBreed = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    numberFeedings = table.Column<int>(type: "int", nullable: true),
                    levelOfTraining = table.Column<int>(type: "int", nullable: true),
                    catchingMouses = table.Column<bool>(type: "bit", nullable: true),
                    ownerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Evidences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerKind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Addres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimalId = table.Column<int>(type: "int", nullable: true),
                    AnimalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimalKind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimalGender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimalBirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnimalBreed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberFeedings = table.Column<int>(type: "int", nullable: true),
                    LevelOfTraining = table.Column<int>(type: "int", nullable: true),
                    CatchingMouses = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evidences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ownerId = table.Column<int>(type: "int", nullable: true),
                    ownerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    birthDate = table.Column<DateTime>(type: "date", nullable: true),
                    ownerKind = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    addres = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Evidences");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
