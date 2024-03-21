using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class create_table_booking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.CreateTable(
						   name: "Bookings",
						   columns: table => new
						   {
							   Id = table.Column<int>(type: "int", nullable: false)
								   .Annotation("SqlServer:Identity", "1, 1"),
							   TourId = table.Column<int>(type: "int", nullable: false),
							   InternalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
							   CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
							   CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
							   BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
							   NumberOfPersons = table.Column<int>(type: "int", nullable: false),
							   TotalPrice = table.Column<double>(type: "float", nullable: false),
							   CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
							   CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
							   UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
							   UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
							   IsDeleted = table.Column<bool>(type: "bit", nullable: true)
						   },
						   constraints: table =>
						   {
							   table.PrimaryKey("PK_Bookings", x => x.Id);
							   table.ForeignKey(
								   name: "FK_Bookings_Tours_TourId",
								   column: x => x.TourId,
								   principalTable: "Tours",
								   principalColumn: "Id",
								   onDelete: ReferentialAction.Cascade);
						   });

			migrationBuilder.CreateIndex(
				name: "IX_Bookings_TourId",
				table: "Bookings",
				column: "TourId");
		}


		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropTable(
			   name: "Bookings");
		}
    }
}
