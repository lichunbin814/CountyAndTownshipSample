namespace CountyAndTownshipSample.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.County",
                c => new
                    {
                        Code = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 3),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Township",
                c => new
                    {
                        Code = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 4),
                        PostCode = c.Int(nullable: false),
                        CountyCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Code)
                .ForeignKey("dbo.County", t => t.CountyCode, cascadeDelete: true)
                .Index(t => t.CountyCode);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Township", "CountyCode", "dbo.County");
            DropIndex("dbo.Township", new[] { "CountyCode" });
            DropTable("dbo.Township");
            DropTable("dbo.County");
        }
    }
}
