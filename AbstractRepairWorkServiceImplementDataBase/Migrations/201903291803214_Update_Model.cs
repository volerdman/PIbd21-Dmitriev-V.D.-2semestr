namespace AbstractRepairWorkServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Model : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MaterialRepair1", "Material_Id", "dbo.Materials");
            DropForeignKey("dbo.MaterialRepair1", "Repair_Id", "dbo.Repairs");
            DropForeignKey("dbo.StorageMaterial1", "Storage_Id", "dbo.Storages");
            DropForeignKey("dbo.StorageMaterial1", "Material_Id", "dbo.Materials");
            DropForeignKey("dbo.MaterialRepairs", "RepairId", "dbo.Repairs");
            DropIndex("dbo.MaterialRepairs", new[] { "RepairId" });
            DropIndex("dbo.MaterialRepair1", new[] { "Material_Id" });
            DropIndex("dbo.MaterialRepair1", new[] { "Repair_Id" });
            DropIndex("dbo.StorageMaterial1", new[] { "Storage_Id" });
            DropIndex("dbo.StorageMaterial1", new[] { "Material_Id" });
            DropTable("dbo.MaterialRepair1");
            DropTable("dbo.StorageMaterial1");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StorageMaterial1",
                c => new
                    {
                        Storage_Id = c.Int(nullable: false),
                        Material_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Storage_Id, t.Material_Id });
            
            CreateTable(
                "dbo.MaterialRepair1",
                c => new
                    {
                        Material_Id = c.Int(nullable: false),
                        Repair_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Material_Id, t.Repair_Id });
            
            CreateIndex("dbo.StorageMaterial1", "Material_Id");
            CreateIndex("dbo.StorageMaterial1", "Storage_Id");
            CreateIndex("dbo.MaterialRepair1", "Repair_Id");
            CreateIndex("dbo.MaterialRepair1", "Material_Id");
            CreateIndex("dbo.MaterialRepairs", "RepairId");
            AddForeignKey("dbo.MaterialRepairs", "RepairId", "dbo.Repairs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StorageMaterial1", "Material_Id", "dbo.Materials", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StorageMaterial1", "Storage_Id", "dbo.Storages", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MaterialRepair1", "Repair_Id", "dbo.Repairs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MaterialRepair1", "Material_Id", "dbo.Materials", "Id", cascadeDelete: true);
        }
    }
}
