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
                name: "file_entity",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(26)", nullable: false),
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
                    table.PrimaryKey("pk_file_entity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "locations",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(26)", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    parent_id = table.Column<string>(type: "character varying(26)", nullable: true),
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
                name: "manufacturer_entity",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(26)", nullable: false),
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
                    table.PrimaryKey("pk_manufacturer_entity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "materials",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(26)", nullable: false),
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
                name: "set_entity",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(26)", nullable: false),
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
                    table.PrimaryKey("pk_set_entity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_entity",
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
                    table.PrimaryKey("pk_user_entity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "storages",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(26)", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    location_id = table.Column<string>(type: "character varying(26)", nullable: true),
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
                    id = table.Column<string>(type: "character varying(26)", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    area = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    barcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    serial_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    location_id = table.Column<string>(type: "character varying(26)", nullable: false),
                    manufacturer_id = table.Column<string>(type: "character varying(26)", nullable: true),
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
                        name: "fk_assets_manufacturer_entity_manufacturer_id",
                        column: x => x.manufacturer_id,
                        principalSchema: "public",
                        principalTable: "manufacturer_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_entity",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(26)", nullable: false),
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
                    table.PrimaryKey("pk_team_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_team_entity_user_entity_leader_id",
                        column: x => x.leader_id,
                        principalSchema: "public",
                        principalTable: "user_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "items",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(26)", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    storage_id = table.Column<string>(type: "character varying(26)", nullable: true),
                    location_id = table.Column<string>(type: "character varying(26)", nullable: true),
                    material_id = table.Column<string>(type: "character varying(26)", nullable: false),
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
                    table.PrimaryKey("pk_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_items_locations_location_id",
                        column: x => x.location_id,
                        principalSchema: "public",
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_items_materials_material_id",
                        column: x => x.material_id,
                        principalSchema: "public",
                        principalTable: "materials",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_items_storages_storage_id",
                        column: x => x.storage_id,
                        principalSchema: "public",
                        principalTable: "storages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "maintenance_entity",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(26)", nullable: false),
                    asset_id = table.Column<string>(type: "character varying(26)", nullable: false),
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
                    table.PrimaryKey("pk_maintenance_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_maintenance_entity_assets_asset_id",
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
                    id = table.Column<string>(type: "character varying(26)", nullable: false),
                    asset_id = table.Column<string>(type: "character varying(26)", nullable: false),
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
                    team_id = table.Column<string>(type: "character varying(26)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_teams_and_members", x => new { x.user_id, x.team_id });
                    table.ForeignKey(
                        name: "fk_teams_and_members_team_entity_team_id",
                        column: x => x.team_id,
                        principalSchema: "public",
                        principalTable: "team_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_teams_and_members_user_entity_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "user_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "work_orders",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(26)", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    request_id = table.Column<string>(type: "character varying(26)", nullable: true),
                    maintenance_id = table.Column<string>(type: "character varying(26)", nullable: true),
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
                        name: "fk_work_orders_maintenance_entity_maintenance_id",
                        column: x => x.maintenance_id,
                        principalSchema: "public",
                        principalTable: "maintenance_entity",
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
                name: "part_entity",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(26)", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    part_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    image = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    cost = table.Column<decimal>(type: "numeric", nullable: false),
                    work_order_entity_id = table.Column<string>(type: "character varying(26)", nullable: true),
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
                    table.PrimaryKey("pk_part_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_part_entity_work_orders_work_order_entity_id",
                        column: x => x.work_order_entity_id,
                        principalSchema: "public",
                        principalTable: "work_orders",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "task_entity",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(26)", nullable: false),
                    asset_id = table.Column<string>(type: "character varying(26)", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    work_order_entity_id = table.Column<string>(type: "character varying(26)", nullable: true),
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
                    table.PrimaryKey("pk_task_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_task_entity_assets_asset_id",
                        column: x => x.asset_id,
                        principalSchema: "public",
                        principalTable: "assets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_task_entity_work_orders_work_order_entity_id",
                        column: x => x.work_order_entity_id,
                        principalSchema: "public",
                        principalTable: "work_orders",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "parts_and_items",
                schema: "public",
                columns: table => new
                {
                    part_id = table.Column<string>(type: "character varying(26)", nullable: false),
                    item_id = table.Column<string>(type: "character varying(26)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_parts_and_items", x => new { x.part_id, x.item_id });
                    table.ForeignKey(
                        name: "fk_parts_and_items_items_item_id",
                        column: x => x.item_id,
                        principalSchema: "public",
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_parts_and_items_part_entity_part_id",
                        column: x => x.part_id,
                        principalSchema: "public",
                        principalTable: "part_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sets_and_parts",
                schema: "public",
                columns: table => new
                {
                    part_id = table.Column<string>(type: "character varying(26)", nullable: false),
                    set_id = table.Column<string>(type: "character varying(26)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sets_and_parts", x => new { x.part_id, x.set_id });
                    table.ForeignKey(
                        name: "fk_sets_and_parts_part_entity_part_id",
                        column: x => x.part_id,
                        principalSchema: "public",
                        principalTable: "part_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sets_and_parts_set_entity_set_id",
                        column: x => x.set_id,
                        principalSchema: "public",
                        principalTable: "set_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "checklist_entity",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(26)", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    task_id = table.Column<string>(type: "character varying(26)", nullable: false),
                    work_order_id = table.Column<string>(type: "character varying(26)", nullable: false),
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
                    table.PrimaryKey("pk_checklist_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_checklist_entity_task_entity_task_id",
                        column: x => x.task_id,
                        principalSchema: "public",
                        principalTable: "task_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_checklist_entity_work_orders_work_order_id",
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
                name: "ix_checklist_entity_task_id",
                schema: "public",
                table: "checklist_entity",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "ix_checklist_entity_work_order_id",
                schema: "public",
                table: "checklist_entity",
                column: "work_order_id");

            migrationBuilder.CreateIndex(
                name: "ix_items_location_id",
                schema: "public",
                table: "items",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "ix_items_material_id",
                schema: "public",
                table: "items",
                column: "material_id");

            migrationBuilder.CreateIndex(
                name: "ix_items_storage_id",
                schema: "public",
                table: "items",
                column: "storage_id");

            migrationBuilder.CreateIndex(
                name: "ix_locations_parent_id",
                schema: "public",
                table: "locations",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_maintenance_entity_asset_id",
                schema: "public",
                table: "maintenance_entity",
                column: "asset_id");

            migrationBuilder.CreateIndex(
                name: "ix_part_entity_work_order_entity_id",
                schema: "public",
                table: "part_entity",
                column: "work_order_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_parts_and_items_item_id",
                schema: "public",
                table: "parts_and_items",
                column: "item_id");

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
                name: "ix_task_entity_asset_id",
                schema: "public",
                table: "task_entity",
                column: "asset_id");

            migrationBuilder.CreateIndex(
                name: "ix_task_entity_work_order_entity_id",
                schema: "public",
                table: "task_entity",
                column: "work_order_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_team_entity_leader_id",
                schema: "public",
                table: "team_entity",
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
                name: "checklist_entity",
                schema: "public");

            migrationBuilder.DropTable(
                name: "file_entity",
                schema: "public");

            migrationBuilder.DropTable(
                name: "parts_and_items",
                schema: "public");

            migrationBuilder.DropTable(
                name: "sets_and_parts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "teams_and_members",
                schema: "public");

            migrationBuilder.DropTable(
                name: "task_entity",
                schema: "public");

            migrationBuilder.DropTable(
                name: "items",
                schema: "public");

            migrationBuilder.DropTable(
                name: "part_entity",
                schema: "public");

            migrationBuilder.DropTable(
                name: "set_entity",
                schema: "public");

            migrationBuilder.DropTable(
                name: "team_entity",
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
                name: "user_entity",
                schema: "public");

            migrationBuilder.DropTable(
                name: "maintenance_entity",
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
                name: "manufacturer_entity",
                schema: "public");
        }
    }
}
