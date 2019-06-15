namespace AbstractRepairWorkServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Classes_Test1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MaterialRepair1",
                c => new
                    {
                        Material_Id = c.Int(nullable: false),
                        Repair_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Material_Id, t.Repair_Id })
                .ForeignKey("dbo.Materials", t => t.Material_Id, cascadeDelete: true)
                .ForeignKey("dbo.Repairs", t => t.Repair_Id, cascadeDelete: true)
                .Index(t => t.Material_Id)
                .Index(t => t.Repair_Id);
            
            AddColumn("dbo.Repairs", "MaterialID", c => c.Int(nullable: false));
            AddColumn("dbo.Materials", "RepairID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MaterialRepair1", "Repair_Id", "dbo.Repairs");
            DropForeignKey("dbo.MaterialRepair1", "Material_Id", "dbo.Materials");
            DropIndex("dbo.MaterialRepair1", new[] { "Repair_Id" });
            DropIndex("dbo.MaterialRepair1", new[] { "Material_Id" });
            DropColumn("dbo.Materials", "RepairID");
            DropColumn("dbo.Repairs", "MaterialID");
            DropTable("dbo.MaterialRepair1");
        }
    }
}
