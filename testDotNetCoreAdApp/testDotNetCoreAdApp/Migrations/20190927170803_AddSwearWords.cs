using Microsoft.EntityFrameworkCore.Migrations;

namespace testDotNetCoreAdApp.Migrations
{
    public partial class AddSwearWords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[SwearWord] VALUES ('Fuck Trumpet')");
            migrationBuilder.Sql("INSERT INTO [dbo].[SwearWord] VALUES ('Shit Bag')");
            migrationBuilder.Sql("INSERT INTO [dbo].[SwearWord] VALUES ('Bollocks')");
            migrationBuilder.Sql("INSERT INTO [dbo].[SwearWord] VALUES ('Cock Nose')");
            migrationBuilder.Sql("INSERT INTO [dbo].[SwearWord] VALUES ('Knobhead')");
            migrationBuilder.Sql("INSERT INTO [dbo].[SwearWord] VALUES ('Arse Badger')");
            migrationBuilder.Sql("INSERT INTO [dbo].[SwearWord] VALUES ('Twat Waffle')");
            migrationBuilder.Sql("INSERT INTO [dbo].[SwearWord] VALUES ('Dick Weasle')");
            migrationBuilder.Sql("INSERT INTO [dbo].[SwearWord] VALUES ('Scroat')");
            migrationBuilder.Sql("INSERT INTO [dbo].[SwearWord] VALUES ('Smeghead')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[SwearWord] WHERE [Name] = 'Fuck Trumpet'");
            migrationBuilder.Sql("DELETE FROM [dbo].[SwearWord] WHERE [Name] = 'Shit Bag'");
            migrationBuilder.Sql("DELETE FROM [dbo].[SwearWord] WHERE [Name] = 'Bollocks'");
            migrationBuilder.Sql("DELETE FROM [dbo].[SwearWord] WHERE [Name] = 'Cock Nose'");
            migrationBuilder.Sql("DELETE FROM [dbo].[SwearWord] WHERE [Name] = 'Knobhead'");
            migrationBuilder.Sql("DELETE FROM [dbo].[SwearWord] WHERE [Name] = 'Arse Badger'");
            migrationBuilder.Sql("DELETE FROM [dbo].[SwearWord] WHERE [Name] = 'Twat Waffle'");
            migrationBuilder.Sql("DELETE FROM [dbo].[SwearWord] WHERE [Name] = 'Dick Weasle'");
            migrationBuilder.Sql("DELETE FROM [dbo].[SwearWord] WHERE [Name] = 'Scroat'");
            migrationBuilder.Sql("DELETE FROM [dbo].[SwearWord] WHERE [Name] = 'Smeghead'");
        }
    }
}
