namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrlField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "Url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pages", "Url");
        }
    }
}
