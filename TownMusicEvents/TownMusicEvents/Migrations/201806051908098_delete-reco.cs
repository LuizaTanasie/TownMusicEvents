namespace TownMusicEvents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletereco : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Recommendations", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Recommendations", "FanId", "dbo.Fans");
            DropIndex("dbo.Recommendations", new[] { "FanId" });
            DropIndex("dbo.Recommendations", new[] { "ArtistId" });
            DropTable("dbo.Recommendations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Recommendations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FanId = c.Int(nullable: false),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Recommendations", "ArtistId");
            CreateIndex("dbo.Recommendations", "FanId");
            AddForeignKey("dbo.Recommendations", "FanId", "dbo.Fans", "FanId");
            AddForeignKey("dbo.Recommendations", "ArtistId", "dbo.Artists", "ArtistId");
        }
    }
}
