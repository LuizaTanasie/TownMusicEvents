namespace TownMusicEvents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ArtistId = c.Int(nullable: false),
                        Biography = c.String(),
                        Website = c.String(),
                        YouTube = c.String(),
                        Facebook = c.String(),
                        Instagram = c.String(),
                        Twitter = c.String(),
                        Picture1Url = c.String(),
                        Picture2Url = c.String(),
                        Picture3Url = c.String(),
                        Picture4Url = c.String(),
                        Picture5Url = c.String(),
                    })
                .PrimaryKey(t => t.ArtistId)
                .ForeignKey("dbo.Users", t => t.ArtistId)
                .Index(t => t.ArtistId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.ArtistId)
                .ForeignKey("dbo.Venues", t => t.VenueId)
                .Index(t => t.ArtistId)
                .Index(t => t.VenueId);
            
            CreateTable(
                "dbo.Fans",
                c => new
                    {
                        FanId = c.Int(nullable: false),
                        About = c.String(),
                        PhotoUrl = c.String(),
                    })
                .PrimaryKey(t => t.FanId)
                .ForeignKey("dbo.Users", t => t.FanId)
                .Index(t => t.FanId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Role = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
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
                .PrimaryKey(t => t.VenueId)
                .ForeignKey("dbo.Users", t => t.VenueId)
                .Index(t => t.VenueId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.ArtistId)
                .ForeignKey("dbo.Venues", t => t.VenueId)
                .Index(t => t.ArtistId)
                .Index(t => t.VenueId);
            
            CreateTable(
                "dbo.NewsItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Text = c.String(),
                        PhotoUrl = c.String(),
                        Type = c.Int(nullable: false),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.ArtistId)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.FanArtists",
                c => new
                    {
                        Fan_FanId = c.Int(nullable: false),
                        Artist_ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Fan_FanId, t.Artist_ArtistId })
                .ForeignKey("dbo.Fans", t => t.Fan_FanId, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.Artist_ArtistId, cascadeDelete: true)
                .Index(t => t.Fan_FanId)
                .Index(t => t.Artist_ArtistId);
            
            CreateTable(
                "dbo.FanConcerts",
                c => new
                    {
                        Fan_FanId = c.Int(nullable: false),
                        Concert_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Fan_FanId, t.Concert_Id })
                .ForeignKey("dbo.Fans", t => t.Fan_FanId, cascadeDelete: true)
                .ForeignKey("dbo.Concerts", t => t.Concert_Id, cascadeDelete: true)
                .Index(t => t.Fan_FanId)
                .Index(t => t.Concert_Id);
            
            CreateTable(
                "dbo.GenreUsers",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.User_Id })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.VenueArtists",
                c => new
                    {
                        Venue_VenueId = c.Int(nullable: false),
                        Artist_ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Venue_VenueId, t.Artist_ArtistId })
                .ForeignKey("dbo.Venues", t => t.Venue_VenueId, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.Artist_ArtistId, cascadeDelete: true)
                .Index(t => t.Venue_VenueId)
                .Index(t => t.Artist_ArtistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Artists", "ArtistId", "dbo.Users");
            DropForeignKey("dbo.NewsItems", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Venues", "VenueId", "dbo.Users");
            DropForeignKey("dbo.Messages", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.Messages", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Concerts", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.VenueArtists", "Artist_ArtistId", "dbo.Artists");
            DropForeignKey("dbo.VenueArtists", "Venue_VenueId", "dbo.Venues");
            DropForeignKey("dbo.Fans", "FanId", "dbo.Users");
            DropForeignKey("dbo.Notifications", "UserId", "dbo.Users");
            DropForeignKey("dbo.GenreUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.GenreUsers", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.FanConcerts", "Concert_Id", "dbo.Concerts");
            DropForeignKey("dbo.FanConcerts", "Fan_FanId", "dbo.Fans");
            DropForeignKey("dbo.FanArtists", "Artist_ArtistId", "dbo.Artists");
            DropForeignKey("dbo.FanArtists", "Fan_FanId", "dbo.Fans");
            DropForeignKey("dbo.Concerts", "ArtistId", "dbo.Artists");
            DropIndex("dbo.VenueArtists", new[] { "Artist_ArtistId" });
            DropIndex("dbo.VenueArtists", new[] { "Venue_VenueId" });
            DropIndex("dbo.GenreUsers", new[] { "User_Id" });
            DropIndex("dbo.GenreUsers", new[] { "Genre_Id" });
            DropIndex("dbo.FanConcerts", new[] { "Concert_Id" });
            DropIndex("dbo.FanConcerts", new[] { "Fan_FanId" });
            DropIndex("dbo.FanArtists", new[] { "Artist_ArtistId" });
            DropIndex("dbo.FanArtists", new[] { "Fan_FanId" });
            DropIndex("dbo.NewsItems", new[] { "ArtistId" });
            DropIndex("dbo.Messages", new[] { "VenueId" });
            DropIndex("dbo.Messages", new[] { "ArtistId" });
            DropIndex("dbo.Venues", new[] { "VenueId" });
            DropIndex("dbo.Notifications", new[] { "UserId" });
            DropIndex("dbo.Fans", new[] { "FanId" });
            DropIndex("dbo.Concerts", new[] { "VenueId" });
            DropIndex("dbo.Concerts", new[] { "ArtistId" });
            DropIndex("dbo.Artists", new[] { "ArtistId" });
            DropTable("dbo.VenueArtists");
            DropTable("dbo.GenreUsers");
            DropTable("dbo.FanConcerts");
            DropTable("dbo.FanArtists");
            DropTable("dbo.NewsItems");
            DropTable("dbo.Messages");
            DropTable("dbo.Venues");
            DropTable("dbo.Notifications");
            DropTable("dbo.Genres");
            DropTable("dbo.Users");
            DropTable("dbo.Fans");
            DropTable("dbo.Concerts");
            DropTable("dbo.Artists");
        }
    }
}
