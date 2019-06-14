namespace AbstractRepairWorkServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InfoMessages", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Bookings", "ExecutorId", "dbo.Executors");
            DropIndex("dbo.Bookings", new[] { "ExecutorId" });
            DropIndex("dbo.InfoMessages", new[] { "CustomerId" });
            DropColumn("dbo.Bookings", "ExecutorId");
            DropColumn("dbo.Customers", "Mail");
            DropTable("dbo.InfoMessages");
            DropTable("dbo.Executors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Executors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExecutorFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InfoMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageId = c.String(),
                        FromMailAddress = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        DateDelivery = c.DateTime(nullable: false),
                        CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Customers", "Mail", c => c.String());
            AddColumn("dbo.Bookings", "ExecutorId", c => c.Int());
            CreateIndex("dbo.InfoMessages", "CustomerId");
            CreateIndex("dbo.Bookings", "ExecutorId");
            AddForeignKey("dbo.Bookings", "ExecutorId", "dbo.Executors", "Id");
            AddForeignKey("dbo.InfoMessages", "CustomerId", "dbo.Customers", "Id");
        }
    }
}
