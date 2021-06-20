namespace ActivitiesManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ActivityTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ActivityTypes", t => t.ActivityTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerID)
                .Index(t => t.CustomerID)
                .Index(t => t.ActivityTypeID);
            
            CreateTable(
                "dbo.ActivityTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CustomerTypeID = c.Int(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CustomerTypes", t => t.CustomerTypeID, cascadeDelete: true)
                .Index(t => t.CustomerTypeID);
            
            CreateTable(
                "dbo.CustomerTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activities", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Customers", "CustomerTypeID", "dbo.CustomerTypes");
            DropForeignKey("dbo.Activities", "ActivityTypeID", "dbo.ActivityTypes");
            DropIndex("dbo.Customers", new[] { "CustomerTypeID" });
            DropIndex("dbo.Activities", new[] { "ActivityTypeID" });
            DropIndex("dbo.Activities", new[] { "CustomerID" });
            DropTable("dbo.CustomerTypes");
            DropTable("dbo.Customers");
            DropTable("dbo.ActivityTypes");
            DropTable("dbo.Activities");
        }
    }
}
