using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Alumni.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "registration_date",
                table: "event_registrations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 8, 16, 18, 7, 33, 851, DateTimeKind.Utc).AddTicks(9224),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 8, 16, 18, 4, 18, 598, DateTimeKind.Utc).AddTicks(8592));

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
                    { new Guid("00a48e80-b6db-45d7-a2a8-545d2ae50927"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000002"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(7569) },
                    { new Guid("06acea2b-7e40-400a-b3fa-f4da8f9d05ac"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000002"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(7540) },
                    { new Guid("0b44bff3-96ff-4829-9d88-8a04e5211054"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(2022) },
                    { new Guid("0da040c4-0c0a-4ed1-be6e-c5898fcbfb5f"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(1957) },
                    { new Guid("0dabb09e-7edb-40db-b63b-b1eb5d2360f2"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000001"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5885) },
                    { new Guid("121c8284-6698-41f5-a3da-9a1c7c244c39"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000004"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5780) },
                    { new Guid("1b7c7af0-de43-4ebe-8f8c-5a6069818593"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000005"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5876) },
                    { new Guid("1e0be5a4-8aa9-4bfc-b964-0bea86df6cbc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(1880) },
                    { new Guid("1ea44e83-4110-4b4a-8b39-7bc012fe756f"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000005"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(8903) },
                    { new Guid("23f88562-e97b-4345-a8a0-0bcc61f3369f"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000003"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5904) },
                    { new Guid("332ead56-5282-41c3-9f5d-4a13171e970e"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("60000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(2050) },
                    { new Guid("3367e736-cd5b-487a-8c04-f5b07ba3ad02"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000004"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(8941) },
                    { new Guid("35a93c73-71fa-420a-b703-88aa2317d15f"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000003"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(8932) },
                    { new Guid("3ac775a9-a90d-44b9-9482-4ea1a37f90dc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(1938) },
                    { new Guid("3d6426d1-4441-4528-a457-c661b61ffcca"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000003"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5770) },
                    { new Guid("461d6f78-f158-4dfb-89a9-7affbddfbc4a"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("60000000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(2078) },
                    { new Guid("47824f8c-989a-4e89-bb66-5516492c527b"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000005"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5829) },
                    { new Guid("4b043e3f-32c5-49f4-a8b1-4b31a695825f"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("60000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(2069) },
                    { new Guid("4e36ea87-8d8d-4644-a3c1-951096f10c60"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000003"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5810) },
                    { new Guid("50dfa465-66c6-4374-94d1-15b5834a8f34"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(1836) },
                    { new Guid("5187276d-13fd-4b71-9ed4-0674b039e0ad"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("70000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(2087) },
                    { new Guid("51dd6a38-f8c5-40c9-9060-75ebe14d25f8"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000005"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(8884) },
                    { new Guid("528e5d59-fe1b-4e44-8bbc-1e1c5e13b158"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(1865) },
                    { new Guid("57bb6726-d106-4493-a875-9429f6489568"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000003"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(7550) },
                    { new Guid("5a7ce4f8-3bbe-438a-9e7c-c25d30853270"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000003"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(7578) },
                    { new Guid("5af9f0c7-bc74-4030-9fac-7b7cf92da61b"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(2041) },
                    { new Guid("5cf6a16c-e81a-4f23-ab9d-a88419919e5e"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000002"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5760) },
                    { new Guid("67fea9f1-e6e6-44a9-83dd-fd037f9cd441"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000001"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(9854) },
                    { new Guid("73c78555-4225-4696-bf74-8157b93505a7"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5838) },
                    { new Guid("795c0845-b975-4f74-a2a6-c7117d24014d"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(915) },
                    { new Guid("7efcd0d9-081e-44d9-a065-7e3e3acb3ed7"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(8894) },
                    { new Guid("83c04944-bcbb-415a-a74a-5c67b535efeb"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(1856) },
                    { new Guid("8c1309dd-3800-4edc-b410-096c22e524f6"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000003"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5857) },
                    { new Guid("9048ca57-1190-4a4b-b59a-f6856dc3aafe"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(1889) },
                    { new Guid("95af21f4-c3ee-4cd3-9663-2c86d25a063b"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000001"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(7587) },
                    { new Guid("965aa026-424d-4b10-bc25-d8e0af3a15a7"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(1996) },
                    { new Guid("96a66b9f-e2d1-4701-947e-c29bd71fa726"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(9830) },
                    { new Guid("98898878-b5a6-48c7-afe3-b8fa2bbda4d9"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(7531) },
                    { new Guid("9ae5d005-e3ce-4964-b6ea-195ef1168b71"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(1967) },
                    { new Guid("9d6bb92d-3e1d-44d2-a213-b306dcb6650f"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(1948) },
                    { new Guid("a3165b9d-027c-47b2-8efd-b0eedc980001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(2031) },
                    { new Guid("a5e0f081-c04f-4a11-98c3-3b7682a187e5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("70000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(2097) },
                    { new Guid("a6a6a540-7347-490f-9c1b-1dd1bd7acffb"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000001"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5933) },
                    { new Guid("a7e3294a-5192-4bfd-9ae6-26ce81682814"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(1919) },
                    { new Guid("ae00eccb-78de-4eab-9ad3-31203a7f691d"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(1976) },
                    { new Guid("b28d064b-37ea-41c6-b39e-165feff54e0c"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000004"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5914) },
                    { new Guid("b67ceebd-6023-454f-a450-93100d614e07"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000001"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(8913) },
                    { new Guid("c0fc81de-efb8-4708-9d2f-0562359dc716"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("60000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(2059) },
                    { new Guid("c290c24c-a81e-4254-a0b6-52f65da6fa21"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000001"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(7559) },
                    { new Guid("cbd80f75-ae60-476f-972d-be9236eadf1a"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(1899) },
                    { new Guid("cdf54822-001f-411a-86ba-d064b6a9af2a"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000002"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5895) },
                    { new Guid("cefe708a-d467-4ed4-82d4-42a01fba8687"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(8873) },
                    { new Guid("cfb7291e-2393-45ea-9426-193827bba5ff"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(1986) },
                    { new Guid("d2e2b781-3dd9-4227-abdb-00353b7fb2a5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000001"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(9844) },
                    { new Guid("dabcda43-6c5c-4591-b669-c6d89bc18fc1"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000002"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5799) },
                    { new Guid("dd9c66e9-68f0-422f-b75b-e42785fee571"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000005"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(8951) },
                    { new Guid("dfecfbb2-aefe-4167-9e6a-904f85d9031a"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(1846) },
                    { new Guid("e21c707c-7949-483d-8ed8-84d15f037347"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000002"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5848) },
                    { new Guid("e72df8e0-d8cb-45f2-97a2-2e54e1eb4df4"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5789) },
                    { new Guid("e7eb1e57-7d67-4af7-a3d8-b9a8c4064dc8"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000004"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5819) },
                    { new Guid("e9e99813-8774-4350-9d30-f8168aa377c6"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(1908) },
                    { new Guid("ec4d3cba-f5d7-4c86-b607-4acea2c8ca99"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000001"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5749) },
                    { new Guid("ece3673b-b24f-4e97-b6dc-cad11e11bca5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000004"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5867) },
                    { new Guid("ee6fe9fe-2ab7-45c0-a277-6b712f930057"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(7520) },
                    { new Guid("f1931bd0-63c5-4341-ae11-8a2520f440e6"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("30000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(1929) },
                    { new Guid("f25eb5eb-7835-458c-be66-640f3a0b75f8"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000002"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(8922) },
                    { new Guid("f9a7ad56-8a46-44c9-9d58-a1856d14e1e9"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000005"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 885, DateTimeKind.Utc).AddTicks(5923) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "first_name", "last_name", "password_hash", "role_id", "updated_at" },
                values: new object[,]
                {
                    { new Guid("a1111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@alumni.com", "Admin", "User", "hashed_password_here", new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a2222222-2222-2222-2222-222222222222"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "staff@alumni.com", "Staff", "User", "hashed_password_here", new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a3333333-3333-3333-3333-333333333333"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "alumni@example.com", "John", "Doe", "hashed_password_here", new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "role_navigation",
                columns: new[] { "id", "created_at", "navigation_item_id", "role_id", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("024ddc66-c1fa-4400-b180-a3edc2c84a4f"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000011"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(4504) },
                    { new Guid("079339f9-c33f-4cd5-913f-a04b846eef52"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000001"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(8490) },
                    { new Guid("0b173479-aef6-459b-9bc1-3037ed9fdbe2"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000011"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(7419) },
                    { new Guid("1235cebe-cd3a-489c-a654-ab35185fb008"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000010"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(7410) },
                    { new Guid("1636947f-b72a-4b7f-adca-72259519cecd"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000006"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(4457) },
                    { new Guid("169d2c01-1134-4b04-b014-efd3805aaaec"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000001"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(5982) },
                    { new Guid("1dabf041-56a4-4006-9c59-ab36a0031766"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000004"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(4438) },
                    { new Guid("1f4071c4-6fff-4586-835e-b07b2c7ef519"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000002"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(7372) },
                    { new Guid("2a44c521-21b8-4d55-bfc9-10689cf7ac11"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000004"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(6012) },
                    { new Guid("2af5db6a-9b7d-4ae2-86a9-91fcb4e788d3"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000003"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(4428) },
                    { new Guid("31a43c89-aefe-4626-9994-2830d13901b6"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000005"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(2101) },
                    { new Guid("3850bd58-4271-439c-8266-b4767ab303d9"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000012"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(2173) },
                    { new Guid("3cc4ab61-b01f-4cac-bbb6-ba91ac2dad9c"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000003"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(7381) },
                    { new Guid("452f4175-a374-4b56-996f-40458a40897c"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000007"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(6021) },
                    { new Guid("4c897759-9616-455f-b05b-e0e70d264085"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000004"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(2091) },
                    { new Guid("4fa12adb-657a-43f4-88f8-648f59f9d8d8"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000013"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(2182) },
                    { new Guid("5765fbd8-431c-47ab-ae86-19380777ceb3"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000016"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(4552) },
                    { new Guid("588cca2c-5872-4da1-8343-605a54412439"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000018"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(2231) },
                    { new Guid("58dd23d4-2be8-432c-884a-e57e595fde5c"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000009"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(2143) },
                    { new Guid("5b0de5aa-ad87-4f77-8406-ddd5087f5d36"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000010"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(4494) },
                    { new Guid("5b1c815d-6897-4f55-9b95-ee30045256d3"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000008"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(2133) },
                    { new Guid("5b26884f-403c-4dcf-8ffd-fb05e7c32eba"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000001"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(7361) },
                    { new Guid("5cdce4a7-96f1-4c9f-a9a8-0f91b71e762b"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000015"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(2201) },
                    { new Guid("5e06a318-8ce0-44e7-b405-e868ccff51bf"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000002"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(4419) },
                    { new Guid("730cd7bf-96b4-4636-bba4-82e75a313b4b"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000016"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(2211) },
                    { new Guid("732e59bb-652c-43a8-87ab-56bba30fc5bd"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000007"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(4466) },
                    { new Guid("79097abe-93ef-424f-aec2-1bce0e355935"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000010"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(2154) },
                    { new Guid("93ec64c6-f583-4c2c-9994-8934b73c5948"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(1099) },
                    { new Guid("961d4cd6-8d3d-4c1a-97ac-87a81a72b58e"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000004"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(7391) },
                    { new Guid("9930380d-6b79-4194-8a4c-5fc2a7c75cf4"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000002"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(8500) },
                    { new Guid("9b2ce310-0b1d-429a-a9d2-09d7dc16c8f8"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000014"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(2192) },
                    { new Guid("9d22fcc8-e3cc-4df2-8bcb-6a4c3fde6e05"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000006"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(2114) },
                    { new Guid("9dfe700f-3feb-4db7-af05-6de99771e7ec"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000013"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(6066) },
                    { new Guid("a2790e09-807e-411e-a080-503b861d234b"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000003"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(6002) },
                    { new Guid("a7612fda-1fe6-4a92-a60f-ae8392d8e7c5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000015"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(4543) },
                    { new Guid("a814693e-b89c-4c92-a324-1bb98015aed5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000009"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(4485) },
                    { new Guid("b1cc00b5-f2a8-44a9-bfbc-35f8f52e25bb"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000007"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(2124) },
                    { new Guid("b2a10213-5fc1-4026-8ace-4f718f9f21a5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000008"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(6030) },
                    { new Guid("b3ef933e-a4ee-44d1-89e9-e8be0bf2ad77"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000012"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(4513) },
                    { new Guid("b63bf184-3320-427c-b1ff-9d081ea83d81"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000013"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(4522) },
                    { new Guid("b7c5eeb5-d326-40b4-b5bd-881230a45305"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000005"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(4447) },
                    { new Guid("c6d46451-5adf-47c6-a63e-7ffd4f009dc5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000003"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(8510) },
                    { new Guid("cc60a1db-086e-41b1-8e09-7a6948370d69"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000009"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(7401) },
                    { new Guid("d183c4d9-e071-4933-9d1c-ac22f36808e3"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000004"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(8519) },
                    { new Guid("d8fdfe48-e002-4eee-a11c-639d75457637"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000019"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(2241) },
                    { new Guid("d956d068-ae2f-4493-beab-4ac072b250eb"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000017"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(2220) },
                    { new Guid("dbd8e9be-54a4-4687-aaaf-13e95151bdfe"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000010"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(6040) },
                    { new Guid("dfcccbb6-f1a3-4540-a460-065f05ae63bc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(2164) },
                    { new Guid("e48b62f4-cd80-4e21-92ac-6438709d2d6e"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(2082) },
                    { new Guid("eb81fbf4-1771-4f5b-bf5b-dc52e1e62e99"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(2071) },
                    { new Guid("ed9b1dc0-1327-4933-9721-c4214dcbdd67"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000014"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(4532) },
                    { new Guid("f0e5b1bc-0eac-4c4c-897d-6dc9dce3a274"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000012"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(7429) },
                    { new Guid("f280b19e-446e-4851-86de-ad232f10171e"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000008"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(4475) },
                    { new Guid("f9fbf9c9-72c8-423e-b020-e869c90bcd75"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000002"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(5993) },
                    { new Guid("fc522d07-6bf0-43fd-9eb3-7925fa719c96"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("90000000-0000-0000-0000-000000000001"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 8, 16, 18, 7, 33, 886, DateTimeKind.Utc).AddTicks(4408) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("024ddc66-c1fa-4400-b180-a3edc2c84a4f"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("079339f9-c33f-4cd5-913f-a04b846eef52"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("0b173479-aef6-459b-9bc1-3037ed9fdbe2"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("1235cebe-cd3a-489c-a654-ab35185fb008"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("1636947f-b72a-4b7f-adca-72259519cecd"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("169d2c01-1134-4b04-b014-efd3805aaaec"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("1dabf041-56a4-4006-9c59-ab36a0031766"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("1f4071c4-6fff-4586-835e-b07b2c7ef519"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("2a44c521-21b8-4d55-bfc9-10689cf7ac11"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("2af5db6a-9b7d-4ae2-86a9-91fcb4e788d3"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("31a43c89-aefe-4626-9994-2830d13901b6"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("3850bd58-4271-439c-8266-b4767ab303d9"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("3cc4ab61-b01f-4cac-bbb6-ba91ac2dad9c"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("452f4175-a374-4b56-996f-40458a40897c"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("4c897759-9616-455f-b05b-e0e70d264085"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("4fa12adb-657a-43f4-88f8-648f59f9d8d8"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("5765fbd8-431c-47ab-ae86-19380777ceb3"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("588cca2c-5872-4da1-8343-605a54412439"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("58dd23d4-2be8-432c-884a-e57e595fde5c"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("5b0de5aa-ad87-4f77-8406-ddd5087f5d36"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("5b1c815d-6897-4f55-9b95-ee30045256d3"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("5b26884f-403c-4dcf-8ffd-fb05e7c32eba"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("5cdce4a7-96f1-4c9f-a9a8-0f91b71e762b"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("5e06a318-8ce0-44e7-b405-e868ccff51bf"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("730cd7bf-96b4-4636-bba4-82e75a313b4b"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("732e59bb-652c-43a8-87ab-56bba30fc5bd"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("79097abe-93ef-424f-aec2-1bce0e355935"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("93ec64c6-f583-4c2c-9994-8934b73c5948"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("961d4cd6-8d3d-4c1a-97ac-87a81a72b58e"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("9930380d-6b79-4194-8a4c-5fc2a7c75cf4"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("9b2ce310-0b1d-429a-a9d2-09d7dc16c8f8"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("9d22fcc8-e3cc-4df2-8bcb-6a4c3fde6e05"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("9dfe700f-3feb-4db7-af05-6de99771e7ec"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("a2790e09-807e-411e-a080-503b861d234b"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("a7612fda-1fe6-4a92-a60f-ae8392d8e7c5"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("a814693e-b89c-4c92-a324-1bb98015aed5"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("b1cc00b5-f2a8-44a9-bfbc-35f8f52e25bb"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("b2a10213-5fc1-4026-8ace-4f718f9f21a5"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("b3ef933e-a4ee-44d1-89e9-e8be0bf2ad77"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("b63bf184-3320-427c-b1ff-9d081ea83d81"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("b7c5eeb5-d326-40b4-b5bd-881230a45305"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("c6d46451-5adf-47c6-a63e-7ffd4f009dc5"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("cc60a1db-086e-41b1-8e09-7a6948370d69"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("d183c4d9-e071-4933-9d1c-ac22f36808e3"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("d8fdfe48-e002-4eee-a11c-639d75457637"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("d956d068-ae2f-4493-beab-4ac072b250eb"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("dbd8e9be-54a4-4687-aaaf-13e95151bdfe"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("dfcccbb6-f1a3-4540-a460-065f05ae63bc"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("e48b62f4-cd80-4e21-92ac-6438709d2d6e"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("eb81fbf4-1771-4f5b-bf5b-dc52e1e62e99"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("ed9b1dc0-1327-4933-9721-c4214dcbdd67"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("f0e5b1bc-0eac-4c4c-897d-6dc9dce3a274"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("f280b19e-446e-4851-86de-ad232f10171e"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("f9fbf9c9-72c8-423e-b020-e869c90bcd75"));

            migrationBuilder.DeleteData(
                table: "role_navigation",
                keyColumn: "id",
                keyValue: new Guid("fc522d07-6bf0-43fd-9eb3-7925fa719c96"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("00a48e80-b6db-45d7-a2a8-545d2ae50927"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("06acea2b-7e40-400a-b3fa-f4da8f9d05ac"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("0b44bff3-96ff-4829-9d88-8a04e5211054"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("0da040c4-0c0a-4ed1-be6e-c5898fcbfb5f"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("0dabb09e-7edb-40db-b63b-b1eb5d2360f2"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("121c8284-6698-41f5-a3da-9a1c7c244c39"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("1b7c7af0-de43-4ebe-8f8c-5a6069818593"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("1e0be5a4-8aa9-4bfc-b964-0bea86df6cbc"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("1ea44e83-4110-4b4a-8b39-7bc012fe756f"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("23f88562-e97b-4345-a8a0-0bcc61f3369f"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("332ead56-5282-41c3-9f5d-4a13171e970e"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("3367e736-cd5b-487a-8c04-f5b07ba3ad02"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("35a93c73-71fa-420a-b703-88aa2317d15f"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("3ac775a9-a90d-44b9-9482-4ea1a37f90dc"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("3d6426d1-4441-4528-a457-c661b61ffcca"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("461d6f78-f158-4dfb-89a9-7affbddfbc4a"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("47824f8c-989a-4e89-bb66-5516492c527b"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("4b043e3f-32c5-49f4-a8b1-4b31a695825f"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("4e36ea87-8d8d-4644-a3c1-951096f10c60"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("50dfa465-66c6-4374-94d1-15b5834a8f34"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("5187276d-13fd-4b71-9ed4-0674b039e0ad"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("51dd6a38-f8c5-40c9-9060-75ebe14d25f8"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("528e5d59-fe1b-4e44-8bbc-1e1c5e13b158"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("57bb6726-d106-4493-a875-9429f6489568"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("5a7ce4f8-3bbe-438a-9e7c-c25d30853270"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("5af9f0c7-bc74-4030-9fac-7b7cf92da61b"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("5cf6a16c-e81a-4f23-ab9d-a88419919e5e"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("67fea9f1-e6e6-44a9-83dd-fd037f9cd441"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("73c78555-4225-4696-bf74-8157b93505a7"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("795c0845-b975-4f74-a2a6-c7117d24014d"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("7efcd0d9-081e-44d9-a065-7e3e3acb3ed7"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("83c04944-bcbb-415a-a74a-5c67b535efeb"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("8c1309dd-3800-4edc-b410-096c22e524f6"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("9048ca57-1190-4a4b-b59a-f6856dc3aafe"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("95af21f4-c3ee-4cd3-9663-2c86d25a063b"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("965aa026-424d-4b10-bc25-d8e0af3a15a7"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("96a66b9f-e2d1-4701-947e-c29bd71fa726"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("98898878-b5a6-48c7-afe3-b8fa2bbda4d9"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("9ae5d005-e3ce-4964-b6ea-195ef1168b71"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("9d6bb92d-3e1d-44d2-a213-b306dcb6650f"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("a3165b9d-027c-47b2-8efd-b0eedc980001"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("a5e0f081-c04f-4a11-98c3-3b7682a187e5"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("a6a6a540-7347-490f-9c1b-1dd1bd7acffb"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("a7e3294a-5192-4bfd-9ae6-26ce81682814"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("ae00eccb-78de-4eab-9ad3-31203a7f691d"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("b28d064b-37ea-41c6-b39e-165feff54e0c"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("b67ceebd-6023-454f-a450-93100d614e07"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("c0fc81de-efb8-4708-9d2f-0562359dc716"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("c290c24c-a81e-4254-a0b6-52f65da6fa21"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("cbd80f75-ae60-476f-972d-be9236eadf1a"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("cdf54822-001f-411a-86ba-d064b6a9af2a"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("cefe708a-d467-4ed4-82d4-42a01fba8687"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("cfb7291e-2393-45ea-9426-193827bba5ff"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("d2e2b781-3dd9-4227-abdb-00353b7fb2a5"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("dabcda43-6c5c-4591-b669-c6d89bc18fc1"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("dd9c66e9-68f0-422f-b75b-e42785fee571"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("dfecfbb2-aefe-4167-9e6a-904f85d9031a"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("e21c707c-7949-483d-8ed8-84d15f037347"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("e72df8e0-d8cb-45f2-97a2-2e54e1eb4df4"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("e7eb1e57-7d67-4af7-a3d8-b9a8c4064dc8"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("e9e99813-8774-4350-9d30-f8168aa377c6"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("ec4d3cba-f5d7-4c86-b607-4acea2c8ca99"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("ece3673b-b24f-4e97-b6dc-cad11e11bca5"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("ee6fe9fe-2ab7-45c0-a277-6b712f930057"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("f1931bd0-63c5-4341-ae11-8a2520f440e6"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("f25eb5eb-7835-458c-be66-640f3a0b75f8"));

            migrationBuilder.DeleteData(
                table: "role_permissions",
                keyColumn: "id",
                keyValue: new Guid("f9a7ad56-8a46-44c9-9d58-a1856d14e1e9"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("a1111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("a2222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("a3333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "navigation_items",
                keyColumn: "id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "navigation_items",
                keyColumn: "id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "navigation_items",
                keyColumn: "id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "navigation_items",
                keyColumn: "id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "navigation_items",
                keyColumn: "id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "navigation_items",
                keyColumn: "id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "navigation_items",
                keyColumn: "id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "navigation_items",
                keyColumn: "id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "navigation_items",
                keyColumn: "id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "navigation_items",
                keyColumn: "id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "navigation_items",
                keyColumn: "id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "navigation_items",
                keyColumn: "id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "navigation_items",
                keyColumn: "id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "navigation_items",
                keyColumn: "id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "navigation_items",
                keyColumn: "id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "navigation_items",
                keyColumn: "id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "navigation_items",
                keyColumn: "id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "navigation_items",
                keyColumn: "id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "navigation_items",
                keyColumn: "id",
                keyValue: new Guid("90000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "navigation_groups",
                keyColumn: "id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "navigation_groups",
                keyColumn: "id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "navigation_groups",
                keyColumn: "id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "navigation_groups",
                keyColumn: "id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "navigation_groups",
                keyColumn: "id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000005"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "registration_date",
                table: "event_registrations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2025, 8, 16, 18, 4, 18, 598, DateTimeKind.Utc).AddTicks(8592),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2025, 8, 16, 18, 7, 33, 851, DateTimeKind.Utc).AddTicks(9224));
        }
    }
}
