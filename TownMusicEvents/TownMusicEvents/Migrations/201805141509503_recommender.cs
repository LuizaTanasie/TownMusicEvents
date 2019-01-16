namespace TownMusicEvents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recommender : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recommendations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FanId = c.Int(nullable: false),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.ArtistId)
                .ForeignKey("dbo.Fans", t => t.FanId)
                .Index(t => t.FanId)
                .Index(t => t.ArtistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recommendations", "FanId", "dbo.Fans");
            DropForeignKey("dbo.Recommendations", "ArtistId", "dbo.Artists");
            DropIndex("dbo.Recommendations", new[] { "ArtistId" });
            DropIndex("dbo.Recommendations", new[] { "FanId" });
            DropTable("dbo.Recommendations");
        }
    }
}
