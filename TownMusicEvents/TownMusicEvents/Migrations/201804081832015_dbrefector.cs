namespace TownMusicEvents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbrefector : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Concerts", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.FanArtists", "Fan_FanId", "dbo.Fans");
            DropForeignKey("dbo.FanArtists", "Artist_ArtistId", "dbo.Artists");
            DropForeignKey("dbo.FanConcerts", "Fan_FanId", "dbo.Fans");
            DropForeignKey("dbo.FanConcerts", "Concert_Id", "dbo.Concerts");
            DropForeignKey("dbo.VenueArtists", "Venue_VenueId", "dbo.Venues");
            DropForeignKey("dbo.VenueArtists", "Artist_ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Concerts", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.Messages", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Messages", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.Venues", "VenueId", "dbo.Users");
            DropIndex("dbo.Concerts", new[] { "ArtistId" });
            DropIndex("dbo.Concerts", new[] { "VenueId" });
            DropIndex("dbo.Venues", new[] { "VenueId" });
            DropIndex("dbo.Messages", new[] { "ArtistId" });
            DropIndex("dbo.Messages", new[] { "VenueId" });
            DropIndex("dbo.FanArtists", new[] { "Fan_FanId" });
            DropIndex("dbo.FanArtists", new[] { "Artist_ArtistId" });
            DropIndex("dbo.FanConcerts", new[] { "Fan_FanId" });
            DropIndex("dbo.FanConcerts", new[] { "Concert_Id" });
            DropIndex("dbo.VenueArtists", new[] { "Venue_VenueId" });
            DropIndex("dbo.VenueArtists", new[] { "Artist_ArtistId" });
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FanId = c.Int(nullable: false),
                        ArtistId = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        HasVisited = c.Boolean(nullable: false),
                        HasClickedLinks = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.ArtistId)
                .ForeignKey("dbo.Fans", t => t.FanId)
                .Index(t => t.FanId)
                .Index(t => t.ArtistId);
            
            AddColumn("dbo.Artists", "PictureUrl", c => c.String());
            DropColumn("dbo.Artists", "Picture1Url");
            DropColumn("dbo.Artists", "Picture2Url");
            DropColumn("dbo.Artists", "Picture3Url");
            DropColumn("dbo.Artists", "Picture4Url");
            DropColumn("dbo.Artists", "Picture5Url");
            DropTable("dbo.Concerts");
            DropTable("dbo.Venues");
            DropTable("dbo.Messages");
            DropTable("dbo.FanArtists");
            DropTable("dbo.FanConcerts");
            DropTable("dbo.VenueArtists");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VenueArtists",
                c => new
                    {
                        Venue_VenueId = c.Int(nullable: false),
                        Artist_ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Venue_VenueId, t.Artist_ArtistId });
            
            CreateTable(
                "dbo.FanConcerts",
                c => new
                    {
                        Fan_FanId = c.Int(nullable: false),
                        Concert_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Fan_FanId, t.Concert_Id });
            
            CreateTable(
                "dbo.FanArtists",
                c => new
                    {
                        Fan_FanId = c.Int(nullable: false),
                        Artist_ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Fan_FanId, t.Artist_ArtistId });
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        DateAndTimeSent = c.DateTime(nullable: false),
                        ArtistId = c.Int(nullable: false),
                        VenueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Venues",
                c => new
                    {
                        VenueId = c.Int(nullable: false),
                        VenueName = c.String(),
                        Location = c.String(),
                        Description = c.String(),
                        OpenHours = c.String(),
                        Picture1Url = c.String(),
                        Picture2Url = c.String(),
                        Picture3Url = c.String(),
                    })
                .PrimaryKey(t => t.VenueId);
            
            CreateTable(
                "dbo.Concerts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Location = c.String(),
                        Description = c.String(),
                        DateAndTime = c.DateTime(nullable: false),
                        PhotoUrl = c.String(),
                        BuyTicketsLink = c.String(),
                        ArtistId = c.Int(nullable: false),
                        VenueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Artists", "Picture5Url", c => c.String());
            AddColumn("dbo.Artists", "Picture4Url", c => c.String());
            AddColumn("dbo.Artists", "Picture3Url", c => c.String());
            AddColumn("dbo.Artists", "Picture2Url", c => c.String());
            AddColumn("dbo.Artists", "Picture1Url", c => c.String());
            DropForeignKey("dbo.Ratings", "FanId", "dbo.Fans");
            DropForeignKey("dbo.Ratings", "ArtistId", "dbo.Artists");
            DropIndex("dbo.Ratings", new[] { "ArtistId" });
            DropIndex("dbo.Ratings", new[] { "FanId" });
            DropColumn("dbo.Artists", "PictureUrl");
            DropTable("dbo.Ratings");
            CreateIndex("dbo.VenueArtists", "Artist_ArtistId");
            CreateIndex("dbo.VenueArtists", "Venue_VenueId");
            CreateIndex("dbo.FanConcerts", "Concert_Id");
            CreateIndex("dbo.FanConcerts", "Fan_FanId");
            CreateIndex("dbo.FanArtists", "Artist_ArtistId");
            CreateIndex("dbo.FanArtists", "Fan_FanId");
            CreateIndex("dbo.Messages", "VenueId");
            CreateIndex("dbo.Messages", "ArtistId");
            CreateIndex("dbo.Venues", "VenueId");
            CreateIndex("dbo.Concerts", "VenueId");
            CreateIndex("dbo.Concerts", "ArtistId");
            AddForeignKey("dbo.Venues", "VenueId", "dbo.Users", "Id");
            AddForeignKey("dbo.Messages", "VenueId", "dbo.Venues", "VenueId");
            AddForeignKey("dbo.Messages", "ArtistId", "dbo.Artists", "ArtistId");
            AddForeignKey("dbo.Concerts", "VenueId", "dbo.Venues", "VenueId");
            AddForeignKey("dbo.VenueArtists", "Artist_ArtistId", "dbo.Artists", "ArtistId", cascadeDelete: true);
            AddForeignKey("dbo.VenueArtists", "Venue_VenueId", "dbo.Venues", "VenueId", cascadeDelete: true);
            AddForeignKey("dbo.FanConcerts", "Concert_Id", "dbo.Concerts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FanConcerts", "Fan_FanId", "dbo.Fans", "FanId", cascadeDelete: true);
            AddForeignKey("dbo.FanArtists", "Artist_ArtistId", "dbo.Artists", "ArtistId", cascadeDelete: true);
            AddForeignKey("dbo.FanArtists", "Fan_FanId", "dbo.Fans", "FanId", cascadeDelete: true);
            AddForeignKey("dbo.Concerts", "ArtistId", "dbo.Artists", "ArtistId");
        }
    }
}
