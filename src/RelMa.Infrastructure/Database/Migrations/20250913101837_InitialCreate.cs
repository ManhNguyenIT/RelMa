using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelMa.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "files",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ext = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    source = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    size = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    tenant_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_files", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "locations",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tenant_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_locations", x => x.id);
                    table.ForeignKey(
                        name: "fk_locations_locations_parent_id",
                        column: x => x.parent_id,
                        principalSchema: "public",
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "manufacturers",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    tenant_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_manufacturers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "materials",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    image = table.Column<string>(type: "text", nullable: true),
                    min_qty = table.Column<int>(type: "integer", nullable: false),
                    available_qty = table.Column<int>(type: "integer", nullable: false),
                    incoming_qty = table.Column<int>(type: "integer", nullable: false),
                    allocated_qty = table.Column<int>(type: "integer", nullable: false),
                    tenant_id = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_materials", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sets",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    tenant_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    username = table.Column<string>(type: "text", nullable: true),
                    company = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    tenant_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "storages",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    location_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tenant_id = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_storages", x => x.id);
                    table.ForeignKey(
                        name: "fk_storages_locations_location_id",
                        column: x => x.location_id,
                        principalSchema: "public",
                        principalTable: "locations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "assets",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    area = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    barcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    serial_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    images = table.Column<string[]>(type: "text[]", nullable: false),
                    location_id = table.Column<Guid>(type: "uuid", nullable: false),
                    manufacturer_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tenant_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assets", x => x.id);
                    table.ForeignKey(
                        name: "fk_assets_locations_location_id",
                        column: x => x.location_id,
                        principalSchema: "public",
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_assets_manufacturers_manufacturer_id",
                        column: x => x.manufacturer_id,
                        principalSchema: "public",
                        principalTable: "manufacturers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    leader_id = table.Column<string>(type: "text", nullable: false),
                    tenant_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_teams", x => x.id);
                    table.ForeignKey(
                        name: "fk_teams_users_leader_id",
                        column: x => x.leader_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "maintenances",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    asset_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_maintenances", x => x.id);
                    table.ForeignKey(
                        name: "fk_maintenances_assets_asset_id",
                        column: x => x.asset_id,
                        principalSchema: "public",
                        principalTable: "assets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "requests",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    asset_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    image = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    tenant_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_requests", x => x.id);
                    table.ForeignKey(
                        name: "fk_requests_assets_asset_id",
                        column: x => x.asset_id,
                        principalSchema: "public",
                        principalTable: "assets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "teams_and_members",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    team_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_teams_and_members", x => new { x.user_id, x.team_id });
                    table.ForeignKey(
                        name: "fk_teams_and_members_teams_team_id",
                        column: x => x.team_id,
                        principalSchema: "public",
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_teams_and_members_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "work_orders",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    request_id = table.Column<Guid>(type: "uuid", nullable: true),
                    maintenance_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tenant_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_work_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_work_orders_maintenances_maintenance_id",
                        column: x => x.maintenance_id,
                        principalSchema: "public",
                        principalTable: "maintenances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_work_orders_requests_request_id",
                        column: x => x.request_id,
                        principalSchema: "public",
                        principalTable: "requests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "parts",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    minimum = table.Column<int>(type: "integer", nullable: false),
                    storage_id = table.Column<Guid>(type: "uuid", nullable: true),
                    location_id = table.Column<Guid>(type: "uuid", nullable: true),
                    material_id = table.Column<Guid>(type: "uuid", nullable: false),
                    work_order_entity_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tenant_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_parts", x => x.id);
                    table.ForeignKey(
                        name: "fk_parts_locations_location_id",
                        column: x => x.location_id,
                        principalSchema: "public",
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_parts_materials_material_id",
                        column: x => x.material_id,
                        principalSchema: "public",
                        principalTable: "materials",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_parts_storages_storage_id",
                        column: x => x.storage_id,
                        principalSchema: "public",
                        principalTable: "storages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_parts_work_orders_work_order_entity_id",
                        column: x => x.work_order_entity_id,
                        principalSchema: "public",
                        principalTable: "work_orders",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    asset_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    work_order_entity_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tenant_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tasks", x => x.id);
                    table.ForeignKey(
                        name: "fk_tasks_assets_asset_id",
                        column: x => x.asset_id,
                        principalSchema: "public",
                        principalTable: "assets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_tasks_work_orders_work_order_entity_id",
                        column: x => x.work_order_entity_id,
                        principalSchema: "public",
                        principalTable: "work_orders",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "sets_and_parts",
                schema: "public",
                columns: table => new
                {
                    part_id = table.Column<Guid>(type: "uuid", nullable: false),
                    set_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sets_and_parts", x => new { x.part_id, x.set_id });
                    table.ForeignKey(
                        name: "fk_sets_and_parts_parts_part_id",
                        column: x => x.part_id,
                        principalSchema: "public",
                        principalTable: "parts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sets_and_parts_sets_set_id",
                        column: x => x.set_id,
                        principalSchema: "public",
                        principalTable: "sets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "checklists",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    task_id = table.Column<Guid>(type: "uuid", nullable: false),
                    work_order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_checklists", x => x.id);
                    table.ForeignKey(
                        name: "fk_checklists_tasks_task_id",
                        column: x => x.task_id,
                        principalSchema: "public",
                        principalTable: "tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_checklists_work_orders_work_order_id",
                        column: x => x.work_order_id,
                        principalSchema: "public",
                        principalTable: "work_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_assets_location_id",
                schema: "public",
                table: "assets",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "ix_assets_manufacturer_id",
                schema: "public",
                table: "assets",
                column: "manufacturer_id");

            migrationBuilder.CreateIndex(
                name: "ix_checklists_task_id",
                schema: "public",
                table: "checklists",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "ix_checklists_work_order_id",
                schema: "public",
                table: "checklists",
                column: "work_order_id");

            migrationBuilder.CreateIndex(
                name: "ix_locations_parent_id",
                schema: "public",
                table: "locations",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_maintenances_asset_id",
                schema: "public",
                table: "maintenances",
                column: "asset_id");

            migrationBuilder.CreateIndex(
                name: "ix_parts_location_id",
                schema: "public",
                table: "parts",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "ix_parts_material_id",
                schema: "public",
                table: "parts",
                column: "material_id");

            migrationBuilder.CreateIndex(
                name: "ix_parts_storage_id",
                schema: "public",
                table: "parts",
                column: "storage_id");

            migrationBuilder.CreateIndex(
                name: "ix_parts_work_order_entity_id",
                schema: "public",
                table: "parts",
                column: "work_order_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_requests_asset_id",
                schema: "public",
                table: "requests",
                column: "asset_id");

            migrationBuilder.CreateIndex(
                name: "ix_sets_and_parts_set_id",
                schema: "public",
                table: "sets_and_parts",
                column: "set_id");

            migrationBuilder.CreateIndex(
                name: "ix_storages_location_id",
                schema: "public",
                table: "storages",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "ix_tasks_asset_id",
                schema: "public",
                table: "tasks",
                column: "asset_id");

            migrationBuilder.CreateIndex(
                name: "ix_tasks_work_order_entity_id",
                schema: "public",
                table: "tasks",
                column: "work_order_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_teams_leader_id",
                schema: "public",
                table: "teams",
                column: "leader_id");

            migrationBuilder.CreateIndex(
                name: "ix_teams_and_members_team_id",
                schema: "public",
                table: "teams_and_members",
                column: "team_id");

            migrationBuilder.CreateIndex(
                name: "ix_work_orders_maintenance_id",
                schema: "public",
                table: "work_orders",
                column: "maintenance_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_work_orders_request_id",
                schema: "public",
                table: "work_orders",
                column: "request_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "checklists",
                schema: "public");

            migrationBuilder.DropTable(
                name: "files",
                schema: "public");

            migrationBuilder.DropTable(
                name: "sets_and_parts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "teams_and_members",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tasks",
                schema: "public");

            migrationBuilder.DropTable(
                name: "parts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "sets",
                schema: "public");

            migrationBuilder.DropTable(
                name: "teams",
                schema: "public");

            migrationBuilder.DropTable(
                name: "materials",
                schema: "public");

            migrationBuilder.DropTable(
                name: "storages",
                schema: "public");

            migrationBuilder.DropTable(
                name: "work_orders",
                schema: "public");

            migrationBuilder.DropTable(
                name: "users",
                schema: "public");

            migrationBuilder.DropTable(
                name: "maintenances",
                schema: "public");

            migrationBuilder.DropTable(
                name: "requests",
                schema: "public");

            migrationBuilder.DropTable(
                name: "assets",
                schema: "public");

            migrationBuilder.DropTable(
                name: "locations",
                schema: "public");

            migrationBuilder.DropTable(
                name: "manufacturers",
                schema: "public");
        }
    }
}
