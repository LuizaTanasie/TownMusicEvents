namespace TownMusicEvents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class visit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Visits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FanId = c.Int(nullable: false),
                        ArtistId = c.Int(nullable: false),
                        hasClickedALink = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.ArtistId)
                .ForeignKey("dbo.Fans", t => t.FanId)
                .Index(t => t.FanId)
                .Index(t => t.ArtistId);
            
            AddColumn("dbo.Artists", "Spotify", c => c.String());
            AddColumn("dbo.Artists", "SoundCloud", c => c.String());
            AddColumn("dbo.Ratings", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Ratings", "HasVisited");
            DropColumn("dbo.Ratings", "HasClickedLinks");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "HasClickedLinks", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ratings", "HasVisited", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Visits", "FanId", "dbo.Fans");
            DropForeignKey("dbo.Visits", "ArtistId", "dbo.Artists");
            DropIndex("dbo.Visits", new[] { "ArtistId" });
            DropIndex("dbo.Visits", new[] { "FanId" });
            DropColumn("dbo.Ratings", "Date");
            DropColumn("dbo.Artists", "SoundCloud");
            DropColumn("dbo.Artists", "Spotify");
            DropTable("dbo.Visits");
        }
    }
}
