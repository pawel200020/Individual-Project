using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class defaultparam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE [dbo].[__EFMigrationsHistory] ADD AppliedAtUtc DATETIME NULL; ");
            migrationBuilder.Sql("ALTER TABLE [dbo].[__EFMigrationsHistory] ADD CONSTRAINT DF__Migrations_AppliedAtUtc DEFAULT GETUTCDATE() FOR[AppliedAtUtc]; ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint("DF__Migrations_AppliedAtUtc",
                "__EFMigrationsHistory");
            migrationBuilder.DropColumn("AppliedAtUtc", "__EFMigrationsHistory");
        }
    }
}
