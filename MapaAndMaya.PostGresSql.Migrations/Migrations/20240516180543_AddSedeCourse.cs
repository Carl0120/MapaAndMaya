using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MapaAndMaya.PostGresSql.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddSedeCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacultyFilials");

            migrationBuilder.CreateTable(
                name: "Sedes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TownId = table.Column<int>(type: "integer", nullable: false),
                    SedeTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sedes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sedes_SedeTypes_SedeTypeId",
                        column: x => x.SedeTypeId,
                        principalTable: "SedeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sedes_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SedeCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    SedeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SedeCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SedeCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SedeCourses_Sedes_SedeId",
                        column: x => x.SedeId,
                        principalTable: "Sedes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SedeCourses_CourseId",
                table: "SedeCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_SedeCourses_SedeId",
                table: "SedeCourses",
                column: "SedeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sedes_Name",
                table: "Sedes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sedes_SedeTypeId",
                table: "Sedes",
                column: "SedeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sedes_TownId",
                table: "Sedes",
                column: "TownId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SedeCourses");

            migrationBuilder.DropTable(
                name: "Sedes");

            migrationBuilder.CreateTable(
                name: "FacultyFilials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SedeTypeId = table.Column<int>(type: "integer", nullable: false),
                    TownId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyFilials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacultyFilials_SedeTypes_SedeTypeId",
                        column: x => x.SedeTypeId,
                        principalTable: "SedeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacultyFilials_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacultyFilials_Name",
                table: "FacultyFilials",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FacultyFilials_SedeTypeId",
                table: "FacultyFilials",
                column: "SedeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyFilials_TownId",
                table: "FacultyFilials",
                column: "TownId");
        }
    }
}
