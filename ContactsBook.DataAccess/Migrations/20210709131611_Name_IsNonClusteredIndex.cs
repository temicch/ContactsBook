using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactsBook.DataAccess.MsSql.Migrations
{
    public partial class Name_IsNonClusteredIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Name",
                table: "Contacts",
                column: "Name")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contacts_Name",
                table: "Contacts");
        }
    }
}
