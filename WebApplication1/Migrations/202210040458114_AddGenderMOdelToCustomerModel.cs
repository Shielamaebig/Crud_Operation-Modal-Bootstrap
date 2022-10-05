namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddGenderMOdelToCustomerModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "GenderId", c => c.Int(nullable: true));
            CreateIndex("dbo.Customers", "GenderId");
            AddForeignKey("dbo.Customers", "GenderId", "dbo.Genders", "Id", cascadeDelete: false);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Customers", "GenderId", "dbo.Genders");
            DropIndex("dbo.Customers", new[] { "GenderId" });
            DropColumn("dbo.Customers", "GenderId");
        }
    }
}
