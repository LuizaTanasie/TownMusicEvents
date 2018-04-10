namespace TownMusicEvents.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastfm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artists", "LastFmId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "LastFmId");
        }
    }
}
