using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleCatalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonSummaryView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE VIEW PersonSummary AS
          SELECT Id, FirstName, LastName, Email
          FROM People");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW PersonSummary");
        }

    }
}
