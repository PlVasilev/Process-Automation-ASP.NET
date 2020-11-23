using Microsoft.EntityFrameworkCore.Migrations;

namespace Seller.Offers.Migrations
{
    public partial class AddedBuyerUsernameToOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorName",
                table: "Offers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorName",
                table: "Offers");
        }
    }
}
