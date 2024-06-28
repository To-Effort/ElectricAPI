using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Electric.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class ininits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EleAuditLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ApiUrl = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false, comment: "API接口地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Method = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false, comment: "接口的方法")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Parameters = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false, comment: "参数")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReturnValue = table.Column<string>(type: "longtext", nullable: false, comment: "返回结果")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExecutionDuration = table.Column<int>(type: "int", nullable: false, comment: "执行时间"),
                    ClientIpAddress = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false, comment: "客户端IP")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BrowserInfo = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false, comment: "浏览器信息")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AuditLogType = table.Column<int>(type: "int", nullable: false, comment: "日志类型 0:正常日志记录，99：异常日志"),
                    ExceptionMessage = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false, comment: "异常信息")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Exception = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false, comment: "详细异常")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "创建者Id"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "最后编辑时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleAuditLog", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ElePermission",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "权限名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "权限编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "Url地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Component = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "Vue页面组件")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Icon = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "图标")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PermissionType = table.Column<int>(type: "int", nullable: false, comment: "菜单类型,0：菜单权限、元素权限、Api权限、数据权限"),
                    ApiMethod = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, comment: "API方法：GET、PUT、POST、DELETE")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sort = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    ParentId = table.Column<long>(type: "bigint", nullable: false, comment: "父菜单Id"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "状态，0：禁用，1：正常"),
                    Remark = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "创建者Id"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "最后编辑时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElePermission", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EleRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "创建者Id"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "最后编辑时间"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "状态，0：禁用，1：正常"),
                    Remark = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleRole", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EleUser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "创建者Id"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "最后编辑时间"),
                    FullName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleUser", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EleRoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleRoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EleRoleClaim_EleRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "EleRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EleRolePermission",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "创建者Id"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "最后编辑时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleRolePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EleRolePermission_ElePermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ElePermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EleRolePermission_EleRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "EleRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EleUserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleUserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EleUserClaim_EleUser_UserId",
                        column: x => x.UserId,
                        principalTable: "EleUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EleUserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_EleUserLogin_EleUser_UserId",
                        column: x => x.UserId,
                        principalTable: "EleUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EleUserRole",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleUserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_EleUserRole_EleRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "EleRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EleUserRole_EleUser_UserId",
                        column: x => x.UserId,
                        principalTable: "EleUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EleUserToken",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EleUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_EleUserToken_EleUser_UserId",
                        column: x => x.UserId,
                        principalTable: "EleUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "ElePermission",
                columns: new[] { "Id", "ApiMethod", "Code", "Component", "CreationTime", "CreatorId", "Icon", "LastModificationTime", "Name", "ParentId", "PermissionType", "Remark", "Sort", "Status", "Url" },
                values: new object[,]
                {
                    { 1L, "", "system", "", new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8739), 0L, "el-icon-s-tools", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "系统管理", 0L, 0, null, 0, 1, "system" },
                    { 2L, "", "system.user", "views/documentation/index", new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8757), 0L, "el-icon-user-solid", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "用户管理", 1L, 0, null, 0, 1, "system.user" },
                    { 3L, "", "system.role", "views/documentation/index", new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8759), 0L, "peoples", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "角色管理", 1L, 0, null, 0, 1, "role" },
                    { 4L, "", "system.permission", "views/documentation/index", new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8761), 0L, "list", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "菜单管理", 1L, 0, null, 0, 1, "permission" },
                    { 5L, "", "system.rolepermission", "views/documentation/index", new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8762), 0L, "example", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "角色权限", 1L, 0, null, 0, 1, "rolepermission" },
                    { 6L, "", "system.user.add", "", new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8774), 0L, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "添加", 2L, 1, null, 0, 1, "" },
                    { 7L, "", "system.user.edit", "", new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8775), 0L, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "编辑", 2L, 1, null, 0, 1, "" },
                    { 8L, "", "system.user.delete", "", new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8777), 0L, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "删除", 2L, 1, null, 0, 1, "" },
                    { 9L, "", "system.role.add", "", new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8780), 0L, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "添加", 3L, 1, null, 0, 1, "" },
                    { 10L, "", "system.role.edit", "", new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8781), 0L, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "编辑", 3L, 1, null, 0, 1, "" },
                    { 11L, "", "system.role.delete", "", new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8783), 0L, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "删除", 3L, 1, null, 0, 1, "" },
                    { 12L, "", "system.permission.add", "", new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8785), 0L, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "添加", 4L, 1, null, 0, 1, "" },
                    { 13L, "", "system.permission.edit", "", new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8787), 0L, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "编辑", 4L, 1, null, 0, 1, "" },
                    { 14L, "", "system.permission.delete", "", new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8789), 0L, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "删除", 4L, 1, null, 0, 1, "" },
                    { 15L, "", "system.rolepermission.update", "", new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8790), 0L, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "更新", 5L, 1, null, 0, 1, "" },
                    { 16L, "", "log.auditlog", "", new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8772), 0L, "bug", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "日志管理", 0L, 0, null, 0, 1, "log" }
                });

            migrationBuilder.InsertData(
                table: "EleRole",
                columns: new[] { "Id", "ConcurrencyStamp", "CreationTime", "CreatorId", "LastModificationTime", "Name", "NormalizedName", "Remark", "Status" },
                values: new object[] { 1L, null, new DateTime(2024, 6, 28, 9, 2, 41, 56, DateTimeKind.Local).AddTicks(5157), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "管理员", "管理员", null, 1 });

            migrationBuilder.InsertData(
                table: "EleUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreationTime", "CreatorId", "Email", "EmailConfirmed", "FullName", "LastModificationTime", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Remark", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1L, 0, "eccef0d5-4418-4a9d-9e1a-0fc91932d973", new DateTime(2024, 6, 28, 9, 2, 41, 56, DateTimeKind.Local).AddTicks(5473), 0L, "admin@eletric.com", true, "管理员", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "ADMIN@ELETRIC.COM", "ADMIN", "AQAAAAIAAYagAAAAEGqG7+m9cCqhZqjTLOEjoelzjw4ZDgFVGKgjIB6oaJJUs+HvleOrfUNJfxlHDctVRg==", "123456789", false, null, "abc", 1, false, "admin" });

            migrationBuilder.InsertData(
                table: "EleRolePermission",
                columns: new[] { "Id", "CreationTime", "CreatorId", "LastModificationTime", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8925), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, 1L },
                    { 2L, new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8930), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, 1L },
                    { 3L, new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8931), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L, 1L },
                    { 4L, new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8932), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L, 1L },
                    { 5L, new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8932), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L, 1L },
                    { 6L, new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8935), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6L, 1L },
                    { 7L, new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8935), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7L, 1L },
                    { 8L, new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8936), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8L, 1L },
                    { 9L, new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8937), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9L, 1L },
                    { 10L, new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8938), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10L, 1L },
                    { 11L, new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8939), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11L, 1L },
                    { 12L, new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8939), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12L, 1L },
                    { 13L, new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8940), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 13L, 1L },
                    { 14L, new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8941), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 14L, 1L },
                    { 15L, new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8941), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15L, 1L },
                    { 16L, new DateTime(2024, 6, 28, 9, 2, 41, 131, DateTimeKind.Local).AddTicks(8934), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 16L, 1L }
                });

            migrationBuilder.InsertData(
                table: "EleUserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1L, 1L });

            migrationBuilder.CreateIndex(
                name: "IX_EleAuditLog_AuditLogType",
                table: "EleAuditLog",
                column: "AuditLogType");

            migrationBuilder.CreateIndex(
                name: "IX_ElePermission_Code",
                table: "ElePermission",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElePermission_ParentId",
                table: "ElePermission",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ElePermission_Sort",
                table: "ElePermission",
                column: "Sort");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "EleRole",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EleRoleClaim_RoleId",
                table: "EleRoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_EleRolePermission_PermissionId",
                table: "EleRolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_EleRolePermission_RoleId",
                table: "EleRolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "EleUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "EleUser",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EleUserClaim_UserId",
                table: "EleUserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EleUserLogin_UserId",
                table: "EleUserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EleUserRole_RoleId",
                table: "EleUserRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EleAuditLog");

            migrationBuilder.DropTable(
                name: "EleRoleClaim");

            migrationBuilder.DropTable(
                name: "EleRolePermission");

            migrationBuilder.DropTable(
                name: "EleUserClaim");

            migrationBuilder.DropTable(
                name: "EleUserLogin");

            migrationBuilder.DropTable(
                name: "EleUserRole");

            migrationBuilder.DropTable(
                name: "EleUserToken");

            migrationBuilder.DropTable(
                name: "ElePermission");

            migrationBuilder.DropTable(
                name: "EleRole");

            migrationBuilder.DropTable(
                name: "EleUser");
        }
    }
}
