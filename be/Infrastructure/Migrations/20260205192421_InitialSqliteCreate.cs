using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Alumni.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialSqliteCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "navigation_groups",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    order = table.Column<int>(type: "INTEGER", nullable: false),
                    is_active = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_navigation_groups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    code = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    module = table.Column<string>(type: "TEXT", nullable: false),
                    action = table.Column<string>(type: "TEXT", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    is_system = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "navigation_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    label = table.Column<string>(type: "TEXT", nullable: false),
                    icon = table.Column<string>(type: "TEXT", nullable: true),
                    url = table.Column<string>(type: "TEXT", nullable: true),
                    order = table.Column<int>(type: "INTEGER", nullable: false),
                    parent_id = table.Column<Guid>(type: "TEXT", nullable: true),
                    group_id = table.Column<Guid>(type: "TEXT", nullable: true),
                    is_active = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_navigation_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_navigation_items_navigation_groups_group_id",
                        column: x => x.group_id,
                        principalTable: "navigation_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_navigation_items_navigation_items_parent_id",
                        column: x => x.parent_id,
                        principalTable: "navigation_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "approval_levels",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    level = table.Column<int>(type: "INTEGER", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    is_active = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    role_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_approval_levels", x => x.id);
                    table.ForeignKey(
                        name: "FK_approval_levels_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    role_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    permission_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permissions", x => x.id);
                    table.ForeignKey(
                        name: "FK_role_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permissions_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    password_hash = table.Column<string>(type: "TEXT", nullable: false),
                    first_name = table.Column<string>(type: "TEXT", nullable: false),
                    last_name = table.Column<string>(type: "TEXT", nullable: false),
                    role_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "role_navigation",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    role_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    navigation_item_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_navigation", x => x.id);
                    table.ForeignKey(
                        name: "FK_role_navigation_navigation_items_navigation_item_id",
                        column: x => x.navigation_item_id,
                        principalTable: "navigation_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_navigation_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "alumni",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    graduation_year = table.Column<int>(type: "INTEGER", nullable: false),
                    degree = table.Column<string>(type: "TEXT", nullable: false),
                    major = table.Column<string>(type: "TEXT", nullable: false),
                    current_company = table.Column<string>(type: "TEXT", nullable: true),
                    current_position = table.Column<string>(type: "TEXT", nullable: true),
                    location = table.Column<string>(type: "TEXT", nullable: true),
                    bio = table.Column<string>(type: "TEXT", nullable: true),
                    linkedin_url = table.Column<string>(type: "TEXT", nullable: true),
                    github_url = table.Column<string>(type: "TEXT", nullable: true),
                    website_url = table.Column<string>(type: "TEXT", nullable: true),
                    profile_image_url = table.Column<string>(type: "TEXT", nullable: true),
                    is_public = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alumni", x => x.id);
                    table.ForeignKey(
                        name: "FK_alumni_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    title = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    location = table.Column<string>(type: "TEXT", nullable: true),
                    start_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    end_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    max_attendees = table.Column<int>(type: "INTEGER", nullable: true),
                    current_attendees = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    is_online = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    meeting_url = table.Column<string>(type: "TEXT", nullable: true),
                    created_by = table.Column<Guid>(type: "TEXT", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.id);
                    table.ForeignKey(
                        name: "FK_events_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "event_registrations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    event_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    registration_date = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2026, 2, 5, 19, 24, 20, 762, DateTimeKind.Utc).AddTicks(409)),
                    status = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_registrations", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_registrations_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_event_registrations_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "navigation_groups",
                columns: new[] { "id", "created_at", "description", "is_active", "name", "order", "updated_at" },
                values: new object[,]
                {
                    { new Guid("80000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Main navigation items", true, "Main", 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("80000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Administrative functions", true, "Administration", 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("80000000-0000-0000-0000-000000000003"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Content management", true, "Content", 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("80000000-0000-0000-0000-000000000004"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Analytics and reports", true, "Analytics", 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("80000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "System settings", true, "Settings", 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id", "action", "code", "created_at", "description", "module", "updated_at" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "View", "users.view", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "View user information", "User", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "Create", "users.create", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Create new users", "User", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "Edit", "users.edit", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Edit user information", "User", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "Delete", "users.delete", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Delete users", "User", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("20000000-0000-0000-0000-000000000001"), "View", "alumni.view", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "View alumni profiles", "Alumni", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("20000000-0000-0000-0000-000000000002"), "Create", "alumni.create", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Create alumni profiles", "Alumni", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("20000000-0000-0000-0000-000000000003"), "Edit", "alumni.edit", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Edit alumni profiles", "Alumni", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("20000000-0000-0000-0000-000000000004"), "Delete", "alumni.delete", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Delete alumni profiles", "Alumni", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("20000000-0000-0000-0000-000000000005"), "Approve", "alumni.approve", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Approve alumni registrations", "Alumni", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("30000000-0000-0000-0000-000000000001"), "View", "events.view", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "View events", "Event", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("30000000-0000-0000-0000-000000000002"), "Create", "events.create", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Create events", "Event", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("30000000-0000-0000-0000-000000000003"), "Edit", "events.edit", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Edit events", "Event", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("30000000-0000-0000-0000-000000000004"), "Delete", "events.delete", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Delete events", "Event", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("30000000-0000-0000-0000-000000000005"), "Approve", "events.approve", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Approve events", "Event", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("40000000-0000-0000-0000-000000000001"), "View", "content.view", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "View content", "Content", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("40000000-0000-0000-0000-000000000002"), "Create", "content.create", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Create content", "Content", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("40000000-0000-0000-0000-000000000003"), "Edit", "content.edit", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Edit content", "Content", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("40000000-0000-0000-0000-000000000004"), "Delete", "content.delete", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Delete content", "Content", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("40000000-0000-0000-0000-000000000005"), "Publish", "content.publish", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Publish content", "Content", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("50000000-0000-0000-0000-000000000001"), "View", "analytics.view", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "View analytics", "Analytics", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("50000000-0000-0000-0000-000000000002"), "Export", "analytics.export", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Export analytics data", "Analytics", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("60000000-0000-0000-0000-000000000001"), "View", "roles.view", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "View roles", "Role", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("60000000-0000-0000-0000-000000000002"), "Create", "roles.create", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Create roles", "Role", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("60000000-0000-0000-0000-000000000003"), "Edit", "roles.edit", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Edit roles", "Role", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("60000000-0000-0000-0000-000000000004"), "Delete", "roles.delete", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Delete roles", "Role", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("70000000-0000-0000-0000-000000000001"), "View", "settings.view", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "View system settings", "Settings", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("70000000-0000-0000-0000-000000000002"), "Edit", "settings.edit", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Edit system settings", "Settings", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "created_at", "description", "is_system", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Super Administrator with full system access", true, "SuperAdmin", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Administrator with management access", true, "Admin", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Staff member with limited administrative access", true, "Staff", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Alumni member with basic access", true, "Alumni", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Content moderator with approval rights", true, "Moderator", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "navigation_items",
                columns: new[] { "id", "created_at", "group_id", "icon", "is_active", "label", "order", "parent_id", "updated_at", "url" },
                values: new object[,]
                {
                    { new Guid("90000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("80000000-0000-0000-0000-000000000001"), "home", true, "Dashboard", 1, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/dashboard" },
                    { new Guid("90000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("80000000-0000-0000-0000-000000000001"), "users", true, "Alumni", 2, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/alumni" },
                    { new Guid("90000000-0000-0000-0000-000000000003"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("80000000-0000-0000-0000-000000000001"), "calendar", true, "Events", 3, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/events" },
                    { new Guid("90000000-0000-0000-0000-000000000004"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("80000000-0000-0000-0000-000000000001"), "user", true, "Profile", 4, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/profile" },
                    { new Guid("90000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("80000000-0000-0000-0000-000000000002"), "users", true, "User Management", 1, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/admin/users" },
                    { new Guid("90000000-0000-0000-0000-000000000006"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("80000000-0000-0000-0000-000000000002"), "shield", true, "Role Management", 2, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/admin/roles" },
                    { new Guid("90000000-0000-0000-0000-000000000007"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("80000000-0000-0000-0000-000000000002"), "graduation-cap", true, "Alumni Management", 3, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/admin/alumni" },
                    { new Guid("90000000-0000-0000-0000-000000000008"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("80000000-0000-0000-0000-000000000002"), "calendar", true, "Event Management", 4, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/admin/events" },
                    { new Guid("90000000-0000-0000-0000-000000000009"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("80000000-0000-0000-0000-000000000002"), "check-circle", true, "Approvals", 5, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/admin/approvals" },
                    { new Guid("90000000-0000-0000-0000-000000000010"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("80000000-0000-0000-0000-000000000003"), "file-text", true, "Content Management", 1, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/admin/content" },
                    { new Guid("90000000-0000-0000-0000-000000000011"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("80000000-0000-0000-0000-000000000003"), "newspaper", true, "News & Updates", 2, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/admin/content/news" },
                    { new Guid("90000000-0000-0000-0000-000000000012"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("80000000-0000-0000-0000-000000000003"), "folder", true, "Resources", 3, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/admin/content/resources" },
                    { new Guid("90000000-0000-0000-0000-000000000013"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("80000000-0000-0000-0000-000000000004"), "bar-chart", true, "Dashboard Analytics", 1, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/admin/analytics" },
                    { new Guid("90000000-0000-0000-0000-000000000014"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("80000000-0000-0000-0000-000000000004"), "users", true, "Alumni Reports", 2, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/admin/analytics/alumni" },
                    { new Guid("90000000-0000-0000-0000-000000000015"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("80000000-0000-0000-0000-000000000004"), "calendar", true, "Event Reports", 3, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/admin/analytics/events" },
                    { new Guid("90000000-0000-0000-0000-000000000016"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("80000000-0000-0000-0000-000000000004"), "trending-up", true, "Engagement Metrics", 4, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/admin/analytics/engagement" },
                    { new Guid("90000000-0000-0000-0000-000000000017"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("80000000-0000-0000-0000-000000000005"), "settings", true, "System Settings", 1, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/admin/settings" },
                    { new Guid("90000000-0000-0000-0000-000000000018"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("80000000-0000-0000-0000-000000000005"), "mail", true, "Email Templates", 2, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/admin/settings/email" },
                    { new Guid("90000000-0000-0000-0000-000000000019"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("80000000-0000-0000-0000-000000000005"), "navigation", true, "Navigation", 3, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/admin/settings/navigation" }
                });

            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "id", "created_at", "permission_id", "role_id", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("04809fa0-dfc1-4174-b950-b79f267d9176"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000005"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9155) },
                    { new Guid("04c8858c-adaa-41fe-8d83-04060280f0f6"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000005"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(959) },
                    { new Guid("07c9be96-7792-43fc-b4a6-1d12caec3e66"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6838) },
                    { new Guid("08cac6df-24be-49a3-b1b6-a3dca4eb7e13"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000002"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9140) },
                    { new Guid("0ae17d29-5f37-4e20-8cd5-5af96b15a93a"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000005"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9163) },
                    { new Guid("0c00131e-f3ec-47ef-9c15-103d33a42871"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000001"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(961) },
                    { new Guid("14a982e7-e16e-4980-9500-2fd0ea970f9c"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(116) },
                    { new Guid("19e743ae-36b8-4811-8f3b-efc9d26ab7c0"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6835) },
                    { new Guid("1b876df5-c169-4c10-a588-1c2710e9e689"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000003"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9159) },
                    { new Guid("1c39ff81-750e-4a6d-95a4-8968648509b8"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6845) },
                    { new Guid("1d26d0fc-9cad-4ae6-b508-314e33db38c8"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(952) },
                    { new Guid("2491d4af-2116-49fb-b465-8d8a89a99020"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6843) },
                    { new Guid("25ed6017-1740-4a9c-90d8-acc7c44b8968"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9148) },
                    { new Guid("2728d576-1222-40fc-8a2e-fbe582fc70ec"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000005"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9146) },
                    { new Guid("28f9ec91-8dbc-48ea-8d12-2aa168bd562b"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6862) },
                    { new Guid("2acac99f-999d-4468-b99a-5a472f52be23"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000003"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(123) },
                    { new Guid("2b9ac2c0-d12c-405f-8a67-b3ed93dd8e72"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("60000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6870) },
                    { new Guid("2d18b1d2-9a65-49a8-9258-a471d9959284"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6849) },
                    { new Guid("3194ceb4-f82b-4b3e-b258-e6be30ca0c30"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000003"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(963) },
                    { new Guid("39fa9242-44cb-492b-b64f-e1e5398208a6"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6840) },
                    { new Guid("442514dd-ebe1-4b4e-86c3-7f2b99066ab8"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000004"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9132) },
                    { new Guid("4cb51a5d-238e-4a98-8630-f47aa3175838"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6855) },
                    { new Guid("52281366-3a2d-44fe-a05e-40eb68a2d6d9"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000002"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(121) },
                    { new Guid("5415b3fe-4a93-4b30-b81f-30b5e72b587a"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000001"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9165) },
                    { new Guid("5a8ee0e7-2cab-41d4-98ff-cd25b7f91c98"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000004"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9161) },
                    { new Guid("69f98344-f2a6-4170-a825-45ccef923aa1"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000001"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9156) },
                    { new Guid("6ad0ada8-e9aa-4e6a-aaca-52d609d4490b"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6853) },
                    { new Guid("77288e0d-7ecc-45c1-94f7-bc2a3236cbbc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("70000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6874) },
                    { new Guid("7876d179-c717-4020-afc8-0bd91a7fb8f2"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000004"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9152) },
                    { new Guid("7bebd91a-a981-400c-a46b-50af808358c2"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6839) },
                    { new Guid("7f1bf86f-444f-4b30-a29d-141ae9636e82"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6865) },
                    { new Guid("81c44102-bb17-4b92-b3d2-f139bd1780b5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9133) },
                    { new Guid("83171f13-5ea3-4992-afa9-2b7e037d4677"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6867) },
                    { new Guid("873231d2-d130-455a-b764-8ada689e9e01"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6421) },
                    { new Guid("89f94058-0fcd-40ab-8b40-89ec08ab06ba"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000004"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9144) },
                    { new Guid("8cede750-58bb-4dfa-ae9a-9e56af024114"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("60000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6871) },
                    { new Guid("8d87924b-9736-48bf-a031-60572c94ceae"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6859) },
                    { new Guid("9841aa50-fc67-4d52-995e-0fc566e2ebec"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000001"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9125) },
                    { new Guid("98624c46-5c2f-4896-bf23-fd9e737942e0"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000004"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(964) },
                    { new Guid("a41f859f-df4e-437e-9f15-4700c6d6e107"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6854) },
                    { new Guid("a9713927-261f-49a8-9ca2-7e98cbe00925"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000001"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(124) },
                    { new Guid("aac08836-db8c-4e51-bddb-33b081585c38"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("60000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6868) },
                    { new Guid("ac833e24-c9be-45c0-bd31-9ac6b6c93689"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000002"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9129) },
                    { new Guid("ad996e3e-8610-4a7c-a5a6-b3a443ba6dd6"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(1478) },
                    { new Guid("af576d5f-93be-46b0-9d9a-df38ac1c8ce7"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(958) },
                    { new Guid("b1b2c39e-c253-43fb-bbc7-0aa59aed4202"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000005"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(965) },
                    { new Guid("b29c2f2b-6557-4045-88fa-319b5e899172"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000001"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(120) },
                    { new Guid("b487c41b-c55b-44a8-8aa1-8014eacf0ad9"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(1474) },
                    { new Guid("b729446f-3eaf-49d7-8551-9655bd3baae4"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(107) },
                    { new Guid("bb375465-289c-4bca-bf68-4b8701784055"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000003"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9130) },
                    { new Guid("bd4c0598-05ad-4301-bb99-177922395982"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000005"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(957) },
                    { new Guid("c3f4231a-2c32-41dc-bc8b-872bc91fb22c"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6864) },
                    { new Guid("c903d891-ae4c-4ef8-a4d5-d034dea694ee"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6844) },
                    { new Guid("cbaf1e82-e853-4383-b129-236932a8370d"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6851) },
                    { new Guid("cfcec572-e89c-4d1e-b867-a80214d23678"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("60000000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6873) },
                    { new Guid("d79d9e23-ad2d-4a0c-a03d-fef010725814"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000003"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(119) },
                    { new Guid("dcadcd31-d202-4755-a0b3-c3d513eece73"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000002"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9158) },
                    { new Guid("df67b433-3561-4137-a6b2-63c845031835"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000002"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9149) },
                    { new Guid("df9b845f-3037-4924-a50b-54f569ffcba1"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000002"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(962) },
                    { new Guid("e2870522-4a54-4bb9-a7fb-4c178f373ded"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6858) },
                    { new Guid("e4b096f2-ea8d-436b-9790-64e595547be2"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000003"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9143) },
                    { new Guid("ecc62315-ab8f-4676-8fc6-68798e871787"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000003"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(9150) },
                    { new Guid("f09d6b39-c793-4948-8253-0e0097a49204"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000001"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(1480) },
                    { new Guid("f2e8afbf-7538-4cde-8506-8092739589fc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6857) },
                    { new Guid("f94ca942-760c-43e2-bffb-419c379611ec"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("70000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6876) },
                    { new Guid("fc241e10-8492-4592-a808-f58975a54917"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000002"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(118) },
                    { new Guid("fcc6461c-6631-4f30-94df-94302f8b5f24"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 772, DateTimeKind.Utc).AddTicks(6866) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "first_name", "last_name", "password_hash", "role_id", "updated_at" },
                values: new object[,]
                {
                    { new Guid("a1111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@alumni.com", "Admin", "User", "$2a$11$dgxpRa4JM9Le0QH1kUmy7earThhdxfWeICblOMyqzxgAtzKQQ2BaC", new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a2222222-2222-2222-2222-222222222222"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "staff@alumni.com", "Staff", "User", "$2a$11$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi", new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a3333333-3333-3333-3333-333333333333"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "alumni@example.com", "John", "Doe", "$2a$11$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi", new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "role_navigation",
                columns: new[] { "id", "created_at", "navigation_item_id", "role_id", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("05f0fcb0-9807-4da0-bc38-b3774762f32e"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000016"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3029) },
                    { new Guid("06a737bf-7e8f-494b-b3a1-308c322aa5b7"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000010"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4008) },
                    { new Guid("0cce29e4-b292-4344-9702-b93aa91a82ff"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000004"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(5882) },
                    { new Guid("13eca219-8b3f-4fad-9fb6-4385bcecdcdc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000003"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(8679) },
                    { new Guid("161e1bae-b016-46d4-8ad3-e96d0165f0ba"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3005) },
                    { new Guid("17541395-7d6e-4ffb-93e7-4fa5bb7458c3"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000003"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4000) },
                    { new Guid("21f74887-21ad-4fef-b21b-5d829df7152c"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000001"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3798) },
                    { new Guid("222c6f29-e843-40ea-91f7-6b6b84a355b4"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000009"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(5884) },
                    { new Guid("282f7102-5a22-483a-8aca-c43d082243cc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000010"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4928) },
                    { new Guid("2a0faca3-eb25-4eec-9137-bac181eb4b6e"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000012"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4011) },
                    { new Guid("2e44180d-16f7-4af4-b390-776fcd469eb0"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000015"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4016) },
                    { new Guid("356b93e0-50df-4615-958a-5dbcf627ac47"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000008"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4927) },
                    { new Guid("35ba3685-102c-4df1-b402-0089c59f20ab"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000013"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4929) },
                    { new Guid("3dd78fdb-5a85-4bd6-bfee-a538e1354ec4"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000017"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3030) },
                    { new Guid("3e089bc7-3ed2-4f23-8c0b-c57d23043b42"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000007"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3016) },
                    { new Guid("4457ce60-1f8d-4680-b6fc-e2cda1971134"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000002"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3996) },
                    { new Guid("4835726a-01e5-4e27-a73f-c40ea84d3606"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000014"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3027) },
                    { new Guid("4ba0915a-fe95-4177-b440-f470474b526e"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3022) },
                    { new Guid("4c42c91f-4828-46d3-9386-dbcecf3c9ac8"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000008"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4005) },
                    { new Guid("53cd76e5-2691-4833-8fe3-d9e927439c48"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000012"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3023) },
                    { new Guid("5591d663-3750-4a3d-8c61-a803062edce1"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000009"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3019) },
                    { new Guid("5db4524b-3098-42d8-b72d-c9950404b5f1"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000014"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4013) },
                    { new Guid("5f94ceac-3bb3-445b-903b-2e37249ee767"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000011"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(5887) },
                    { new Guid("61ac1582-6ef9-47d8-bd21-2b412ad337b8"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000011"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4010) },
                    { new Guid("64dd97ef-7e9a-464e-8464-f1d0c1c45369"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3001) },
                    { new Guid("662b6d46-5ac1-4259-a30c-18f18371c705"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000019"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3033) },
                    { new Guid("6be080b6-3464-405f-9888-3c108cbe806c"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000010"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3021) },
                    { new Guid("6bec62ff-08bb-42d5-b1dd-801c3394ae31"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000013"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4012) },
                    { new Guid("6e8bfbd7-70f2-4816-81f9-886be7520a54"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000007"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4004) },
                    { new Guid("888f2ff7-a4f8-492a-a093-f7b2197511f4"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000013"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3024) },
                    { new Guid("8b037af1-b5fb-4d84-b753-df525313d21c"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000012"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(5888) },
                    { new Guid("8c73f969-2801-46e7-9bb6-f1fb0aa73b0d"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000001"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(8674) },
                    { new Guid("9047f6a3-fb2c-4197-8560-3e5cdda1c732"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000002"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4919) },
                    { new Guid("9484d1f5-de1d-4ab4-afe9-2727e64917be"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000015"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3028) },
                    { new Guid("952d3734-9465-4d7c-a9b0-aea8fde05071"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000006"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4003) },
                    { new Guid("9663b99f-d885-407e-8a3e-a084b93c6bbe"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000009"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4006) },
                    { new Guid("9b013d5c-811e-403f-88a7-fecaa2983298"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000002"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(5877) },
                    { new Guid("a08f2bdc-675f-4078-9597-81c4bce28977"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000005"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4002) },
                    { new Guid("a1518867-b272-47db-bdc2-5018f47a96de"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(2615) },
                    { new Guid("a44be761-3e8f-48cd-8c4e-4260083c0f0c"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000003"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4923) },
                    { new Guid("abf6f354-3812-437c-baee-c15a7c76eed0"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000001"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(5875) },
                    { new Guid("b1dea53c-f829-4955-8e34-98387707bc67"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000008"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3017) },
                    { new Guid("ba1a29d7-4e72-493b-80a8-ba338bdaa6cd"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000004"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(8681) },
                    { new Guid("bcacf5e7-9692-46b4-8978-4a0487818e20"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000016"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4017) },
                    { new Guid("c022f47d-e80e-4011-ae44-a23b8fe5463f"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000004"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4924) },
                    { new Guid("c951a795-50a6-46da-b530-a4b53846b292"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3007) },
                    { new Guid("cf2a9822-9193-4ecc-904b-9cb185d245ab"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000006"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3015) },
                    { new Guid("cf56c38c-9964-4899-8af3-bf4c99c7f221"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000003"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(5881) },
                    { new Guid("dd0af9cc-83eb-4155-9c44-3d3880023a97"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000004"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4001) },
                    { new Guid("de54685f-d079-4f47-975b-8d95068809ec"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3004) },
                    { new Guid("dea2ecdc-763b-41ad-a877-7e97bea15673"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000002"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(8678) },
                    { new Guid("dec94f27-f50f-44f8-8879-0463386b1fc5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000010"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(5885) },
                    { new Guid("f48891ed-3b49-401e-8048-dacdbb2cdda0"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000018"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(3032) },
                    { new Guid("f7b6238c-64b4-49c4-98ad-213065c7de8b"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000007"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4925) },
                    { new Guid("fd850b2d-5f21-47a5-bfbe-2edb508699ee"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000001"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 2, 5, 19, 24, 20, 773, DateTimeKind.Utc).AddTicks(4918) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_alumni_user_id",
                table: "alumni",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_approval_levels_role_id",
                table: "approval_levels",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_registrations_event_id_user_id",
                table: "event_registrations",
                columns: new[] { "event_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_event_registrations_user_id",
                table: "event_registrations",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_events_created_by",
                table: "events",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_navigation_groups_name",
                table: "navigation_groups",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_navigation_items_group_id",
                table: "navigation_items",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_navigation_items_parent_id",
                table: "navigation_items",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_permissions_code",
                table: "permissions",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_role_navigation_navigation_item_id",
                table: "role_navigation",
                column: "navigation_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_navigation_role_id_navigation_item_id",
                table: "role_navigation",
                columns: new[] { "role_id", "navigation_item_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_permission_id",
                table: "role_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_role_id_permission_id",
                table: "role_permissions",
                columns: new[] { "role_id", "permission_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_roles_name",
                table: "roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                table: "users",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alumni");

            migrationBuilder.DropTable(
                name: "approval_levels");

            migrationBuilder.DropTable(
                name: "event_registrations");

            migrationBuilder.DropTable(
                name: "role_navigation");

            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "navigation_items");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "navigation_groups");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
