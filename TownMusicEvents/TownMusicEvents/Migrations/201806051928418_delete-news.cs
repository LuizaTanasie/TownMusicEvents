namespace TownMusicEvents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletenews : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NewsItems", "ArtistId", "dbo.Artists");
            DropIndex("dbo.NewsItems", new[] { "ArtistId" });
            DropTable("dbo.NewsItems");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.NewsItems", "ArtistId");
            AddForeignKey("dbo.NewsItems", "ArtistId", "dbo.Artists", "ArtistId");
        }
    }
}
