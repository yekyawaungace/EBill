using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelInsurance.Repository.Ef.Migrations
{
    public partial class InitialSchoolDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Speakers");

            migrationBuilder.DropColumn(
                name: "TDateTime",
                table: "TransactionLogs");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "TransactionLogs",
                newName: "tranRef");

            migrationBuilder.RenameColumn(
                name: "FormName",
                table: "TransactionLogs",
                newName: "respDesc");

            migrationBuilder.RenameColumn(
                name: "Events",
                table: "TransactionLogs",
                newName: "respCode");

            migrationBuilder.RenameColumn(
                name: "TermsAndConditions",
                table: "Settings",
                newName: "Body");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "Travellers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "TransactionLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "TransactionLogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "amount",
                table: "TransactionLogs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "approvalCode",
                table: "TransactionLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "cardNo",
                table: "TransactionLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "currencyCode",
                table: "TransactionLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "eci",
                table: "TransactionLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "invoiceNo",
                table: "TransactionLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "merchantID",
                table: "TransactionLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "paymentID",
                table: "TransactionLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "referenceNo",
                table: "TransactionLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "transactionDateTime",
                table: "TransactionLogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactUs",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnglishBody",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FaceBook",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PopupBanner",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebSite",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertificateID",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Endosement",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "TransactionLogs");

            migrationBuilder.DropColumn(
                name: "amount",
                table: "TransactionLogs");

            migrationBuilder.DropColumn(
                name: "approvalCode",
                table: "TransactionLogs");

            migrationBuilder.DropColumn(
                name: "cardNo",
                table: "TransactionLogs");

            migrationBuilder.DropColumn(
                name: "currencyCode",
                table: "TransactionLogs");

            migrationBuilder.DropColumn(
                name: "eci",
                table: "TransactionLogs");

            migrationBuilder.DropColumn(
                name: "invoiceNo",
                table: "TransactionLogs");

            migrationBuilder.DropColumn(
                name: "merchantID",
                table: "TransactionLogs");

            migrationBuilder.DropColumn(
                name: "paymentID",
                table: "TransactionLogs");

            migrationBuilder.DropColumn(
                name: "referenceNo",
                table: "TransactionLogs");

            migrationBuilder.DropColumn(
                name: "transactionDateTime",
                table: "TransactionLogs");

            migrationBuilder.DropColumn(
                name: "ContactUs",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "EnglishBody",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "FaceBook",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "PopupBanner",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "WebSite",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "CertificateID",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Endosement",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "tranRef",
                table: "TransactionLogs",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "respDesc",
                table: "TransactionLogs",
                newName: "FormName");

            migrationBuilder.RenameColumn(
                name: "respCode",
                table: "TransactionLogs",
                newName: "Events");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "Settings",
                newName: "TermsAndConditions");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "Travellers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "TransactionLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "TDateTime",
                table: "TransactionLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Speakers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Experience = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qualification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SpeakerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SpeakingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SpeakingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.Id);
                });
        }
    }
}
