using FluentMigrator;

namespace Layer.Domain.Migrator.Migrator
{

    [Migration(02415051000, "ساخت دیتابیس")]
    public class _202415051000_upadte_GetOnlinePrice_GEN_IRC : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("38.upadte_GetOnlinePrice_GEN&IRC.sql");
        }
        public override void Down()
        {
        }
    }
}
