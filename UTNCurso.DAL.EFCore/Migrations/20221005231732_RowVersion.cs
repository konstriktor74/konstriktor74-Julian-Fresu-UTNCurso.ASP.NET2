using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTNCurso.DAL.EFCore.Migrations
{
    public partial class RowVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RowVersion",
                table: "TodoItem",
                type: "INTEGER",
                rowVersion: true,
                nullable: false,
                defaultValue: 0);
            migrationBuilder.Sql(@"
                CREATE TRIGGER UTNTodoItemRowVersion
                AFTER UPDATE ON TodoItem
                BEGIN
                    UPDATE TodoItem
                    SET RowVersion = RowVersion + 1
                    WHERE rowid = NEW.rowid;
                END;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "TodoItem");
        }
    }
}
