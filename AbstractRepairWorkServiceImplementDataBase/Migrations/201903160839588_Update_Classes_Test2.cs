namespace AbstractRepairWorkServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Classes_Test2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StorageMaterial1",
                c => new
                    {
                        Storage_Id = c.Int(nullable: false),
                        Material_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Storage_Id, t.Material_Id })
                .ForeignKey("dbo.Storages", t => t.Storage_Id, cascadeDelete: true)
                .ForeignKey("dbo.Materials", t => t.Material_Id, cascadeDelete: true)
                .Index(t => t.Storage_Id)
                .Index(t => t.Material_Id);
            
            DropColumn("dbo.Materials", "RepairID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Materials", "RepairID", c => c.Int(nullable: false));
            DropForeignKey("dbo.StorageMaterial1", "Material_Id", "dbo.Materials");
            DropForeignKey("dbo.StorageMaterial1", "Storage_Id", "dbo.Storages");
            DropIndex("dbo.StorageMaterial1", new[] { "Material_Id" });
            DropIndex("dbo.StorageMaterial1", new[] { "Storage_Id" });
            DropTable("dbo.StorageMaterial1");
        }
    }
}
