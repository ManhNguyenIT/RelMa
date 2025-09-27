using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelMa.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddSequences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sequences",
                schema: "public",
                columns: table => new
                {
                    seq_date = table.Column<DateOnly>(type: "date", nullable: false),
                    tenant_id = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    table_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    current_value = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sequences", x => new { x.seq_date, x.table_name, x.tenant_id });
                });

            migrationBuilder.Sql("""
            CREATE OR REPLACE FUNCTION next_sequence(p_date date, p_tenant text, p_table text)
            RETURNS int AS $$
            DECLARE
              result int;
            BEGIN
              LOOP
                UPDATE sequences
                SET current_value = current_value + 1
                WHERE seq_date = p_date AND tenant_id = p_tenant AND table_name = p_table
                RETURNING current_value INTO result;

                IF FOUND THEN
                  RETURN result;
                END IF;

                BEGIN
                  INSERT INTO sequences(tenant_id, seq_date, current_value)
                  VALUES (p_tenant, p_date, 1)
                  RETURNING current_value INTO result;
                  RETURN result;
                EXCEPTION WHEN unique_violation THEN
                END;
              END LOOP;
            END;
            $$ LANGUAGE plpgsql;
            """);

            migrationBuilder.Sql("""
            CREATE OR REPLACE FUNCTION fn_work_orders_no()
            RETURNS trigger AS $$
            BEGIN
              IF NEW.no IS NULL THEN
                NEW.no := next_sequence(NEW.created_date::date, NEW.tenant_id, 'work_orders');
              END IF;
              RETURN NEW;
            END;
            $$ LANGUAGE plpgsql;
            """);

            migrationBuilder.Sql("""
            CREATE TRIGGER trx_work_orders_no
            BEFORE INSERT ON work_orders
            FOR EACH ROW
            EXECUTE FUNCTION fn_work_orders_no();
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS trx_work_orders_no ON work_orders;");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS fn_work_orders_no;");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS next_sequence;");

            migrationBuilder.DropTable(
                name: "sequences",
                schema: "public");
        }
    }
}
