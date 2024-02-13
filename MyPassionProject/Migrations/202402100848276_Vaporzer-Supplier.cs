namespace MyPassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VaporzerSupplier : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Vaporizers", "Profit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vaporizers", "Profit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
