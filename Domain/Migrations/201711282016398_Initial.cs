namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hosts",
                c => new
                    {
                        HostId = c.Int(nullable: false, identity: true),
                        HostUrl = c.String(),
                        TimeCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.HostId);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        PageId = c.Int(nullable: false, identity: true),
                        HostId = c.Int(nullable: false),
                        MinResponseTime = c.Double(nullable: false),
                        MaxResponseTime = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.PageId)
                .ForeignKey("dbo.Hosts", t => t.HostId, cascadeDelete: true)
                .Index(t => t.HostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pages", "HostId", "dbo.Hosts");
            DropIndex("dbo.Pages", new[] { "HostId" });
            DropTable("dbo.Pages");
            DropTable("dbo.Hosts");
        }
    }
}
