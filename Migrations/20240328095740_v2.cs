using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace My_Pro.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomTypeId = table.Column<int>(type: "int", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomImages_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, "Phòng dành cho một người, thường có một giường đơn.", false, "Phòng Đơn (Single Room)" },
                    { 2, "Phòng dành cho hai người, thường có một giường đôi.", false, "Phòng Đôi (Double Room)" },
                    { 3, "Phòng dành cho một gia đình hoặc một nhóm người, thường có nhiều giường hoặc giường đôi lớn để phù hợp với nhiều người.", false, "Phòng Gia Đình (Family Room)" },
                    { 4, "Một loại phòng cao cấp có diện tích lớn hơn, thường bao gồm khu vực sống và khu vực ngủ riêng biệt. Có thể có các tiện nghi cao cấp như phòng tắm riêng, phòng khách riêng, vv.", false, "Suite" },
                    { 5, "Một phiên bản nâng cấp của phòng đơn hoặc phòng đôi, thường có các tiện nghi cao cấp hơn như không gian rộng rãi hơn, quang cảnh tốt hơn, vv.", false, "Phòng Deluxe (Deluxe Room)" },
                    { 6, "Phòng dành cho những khách hàng có nhu cầu kinh doanh, thường có các tiện nghi làm việc như bàn làm việc và kết nối Internet cao cấp.", false, "Phòng Executive" },
                    { 7, "Phòng có tầm nhìn ra biển, thường có cửa sổ hoặc ban công nhìn ra biển.", false, "Phòng Hướng Biển (Ocean View Room)" },
                    { 8, "Phòng có tầm nhìn ra thành phố, thường có cửa sổ hoặc ban công nhìn ra các tòa nhà và cảnh đô thị.", false, "Phòng Hướng Thành Phố (City View Room)" },
                    { 9, "Phòng lớn có thể chứa một nhóm người, thường được sử dụng cho các đoàn du lịch hoặc hội nghị.", false, "Phòng Hợp Nhóm (Group Room)" },
                    { 10, "Loại phòng sang trọng ở trên cùng của khách sạn, thường có diện tích lớn, quang cảnh tốt nhất và các tiện nghi cao cấp như hồ bơi riêng, sân thượng riêng, vv.", false, "Phòng Penthouse" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Description", "IsDeleted", "Name", "PricePerNight", "RoomTypeId" },
                values: new object[,]
                {
                    { 1, "Phòng 101", false, "Phòng 101", 150m, 1 },
                    { 2, "Phòng 102", false, "Phòng 102", 150m, 1 },
                    { 3, "Phòng 103", false, "Phòng 103", 200m, 1 },
                    { 4, "Phòng 104", false, "Phòng 104", 200m, 1 },
                    { 5, "Phòng 201", false, "Phòng 201", 250m, 2 },
                    { 6, "Phòng 202", false, "Phòng 202", 250m, 2 },
                    { 7, "Phòng 203", false, "Phòng 203", 300m, 2 },
                    { 8, "Phòng 301", false, "Phòng 301", 350m, 3 },
                    { 9, "Phòng 302", false, "Phòng 302", 350m, 3 },
                    { 10, "Phòng 401", false, "Phòng 401", 400m, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomImages_RoomId",
                table: "RoomImages",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomImages");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "RoomTypes");
        }
    }
}
