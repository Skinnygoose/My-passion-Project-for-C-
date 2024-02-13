namespace MyPassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vaporizer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vaporizers", "Profit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vaporizers", "Profit");
        }
    }
}
