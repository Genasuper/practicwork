using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace kat_mob_soft.Migrations
{
    public partial class AddContactMessagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_gift_certificate_CertificateId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_user_profiles_users_UserId",
                table: "user_profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_gift_certificate",
                table: "gift_certificate");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "user_profiles");

            migrationBuilder.RenameTable(
                name: "gift_certificate",
                newName: "gift_certificates");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "user_profiles",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "user_profiles",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "user_profiles",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "user_profiles",
                newName: "full_name");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "user_profiles",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "user_profiles",
                newName: "birth_date");

            migrationBuilder.RenameIndex(
                name: "IX_user_profiles_UserId",
                table: "user_profiles",
                newName: "IX_user_profiles_user_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "orders",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "gift_certificates",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "role",
                table: "users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                defaultValue: "user",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "users",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "full_name",
                table: "user_profiles",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "user_profiles",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_gift_certificates",
                table: "gift_certificates",
                column: "id");

            migrationBuilder.CreateTable(
                name: "contact_messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    subject = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    message = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValue: "new"),
                    admin_notes = table.Column<string>(type: "text", nullable: true),
                    admin_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact_messages", x => x.id);
                    table.ForeignKey(
                        name: "FK_contact_messages_users_admin_id",
                        column: x => x.admin_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_contact_messages_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contact_messages_admin_id",
                table: "contact_messages",
                column: "admin_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_messages_user_id",
                table: "contact_messages",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_gift_certificates_CertificateId",
                table: "orders",
                column: "CertificateId",
                principalTable: "gift_certificates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_profiles_users_user_id",
                table: "user_profiles",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_gift_certificates_CertificateId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_user_profiles_users_user_id",
                table: "user_profiles");

            migrationBuilder.DropTable(
                name: "contact_messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_gift_certificates",
                table: "gift_certificates");

            migrationBuilder.RenameTable(
                name: "gift_certificates",
                newName: "gift_certificate");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "user_profiles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "user_profiles",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "user_profiles",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "full_name",
                table: "user_profiles",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "user_profiles",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                table: "user_profiles",
                newName: "BirthDate");

            migrationBuilder.RenameIndex(
                name: "IX_user_profiles_user_id",
                table: "user_profiles",
                newName: "IX_user_profiles_UserId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "orders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "gift_certificate",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "role",
                table: "users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldDefaultValue: "user");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "users",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "user_profiles",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "user_profiles",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "user_profiles",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_gift_certificate",
                table: "gift_certificate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_gift_certificate_CertificateId",
                table: "orders",
                column: "CertificateId",
                principalTable: "gift_certificate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_profiles_users_UserId",
                table: "user_profiles",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
