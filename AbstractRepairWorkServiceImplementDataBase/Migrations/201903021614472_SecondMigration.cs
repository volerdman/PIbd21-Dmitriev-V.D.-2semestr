namespace AbstractRepairWorkServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Materials", "MaterialName", c => c.String(nullable: false));
            AlterColumn("dbo.Storages", "StorageName", c => c.String(nullable: false));
            CreateIndex("dbo.MaterialRepairs", "RepairId");
            CreateIndex("dbo.MaterialRepairs", "MaterialId");
            CreateIndex("dbo.StorageMaterials", "StorageId");
            CreateIndex("dbo.StorageMaterials", "MaterialId");
            AddForeignKey("dbo.MaterialRepairs", "MaterialId", "dbo.Materials", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MaterialRepairs", "RepairId", "dbo.Repairs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StorageMaterials", "MaterialId", "dbo.Materials", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StorageMaterials", "StorageId", "dbo.Storages", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StorageMaterials", "StorageId", "dbo.Storages");
            DropForeignKey("dbo.StorageMaterials", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.MaterialRepairs", "RepairId", "dbo.Repairs");
            DropForeignKey("dbo.MaterialRepairs", "MaterialId", "dbo.Materials");
            DropIndex("dbo.StorageMaterials", new[] { "MaterialId" });
            DropIndex("dbo.StorageMaterials", new[] { "StorageId" });
            DropIndex("dbo.MaterialRepairs", new[] { "MaterialId" });
            DropIndex("dbo.MaterialRepairs", new[] { "RepairId" });
            AlterColumn("dbo.Storages", "StorageName", c => c.String());
            AlterColumn("dbo.Materials", "MaterialName", c => c.String());
        }
    }
}
