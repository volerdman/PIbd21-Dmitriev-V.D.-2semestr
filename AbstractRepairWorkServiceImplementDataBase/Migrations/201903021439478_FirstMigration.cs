namespace AbstractRepairWorkServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        RepairId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        ImplementDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Repairs", t => t.RepairId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.RepairId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Repairs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RepairName = c.String(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MaterialRepairs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RepairId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaterialName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StorageMaterials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "RepairId", "dbo.Repairs");
            DropForeignKey("dbo.Bookings", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Bookings", new[] { "RepairId" });
            DropIndex("dbo.Bookings", new[] { "CustomerId" });
            DropTable("dbo.Storages");
            DropTable("dbo.StorageMaterials");
            DropTable("dbo.Materials");
            DropTable("dbo.MaterialRepairs");
            DropTable("dbo.Repairs");
            DropTable("dbo.Customers");
            DropTable("dbo.Bookings");
        }
    }
}
