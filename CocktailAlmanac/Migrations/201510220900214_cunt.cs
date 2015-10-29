namespace CocktailAlmanac.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cunt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Gender", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserLanguage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserLanguage");
            DropColumn("dbo.AspNetUsers", "Gender");
        }
    }
}
