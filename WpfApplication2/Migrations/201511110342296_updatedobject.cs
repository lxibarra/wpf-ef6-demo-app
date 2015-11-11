namespace WpfApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedobject : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Empleadoes", "Departamento_Id", "dbo.Departamentoes");
            DropIndex("dbo.Empleadoes", new[] { "Departamento_Id" });
            RenameColumn(table: "dbo.Empleadoes", name: "Departamento_Id", newName: "DepartamentoId");
            AlterColumn("dbo.Empleadoes", "DepartamentoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Empleadoes", "DepartamentoId");
            AddForeignKey("dbo.Empleadoes", "DepartamentoId", "dbo.Departamentoes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Empleadoes", "DepartamentoId", "dbo.Departamentoes");
            DropIndex("dbo.Empleadoes", new[] { "DepartamentoId" });
            AlterColumn("dbo.Empleadoes", "DepartamentoId", c => c.Int());
            RenameColumn(table: "dbo.Empleadoes", name: "DepartamentoId", newName: "Departamento_Id");
            CreateIndex("dbo.Empleadoes", "Departamento_Id");
            AddForeignKey("dbo.Empleadoes", "Departamento_Id", "dbo.Departamentoes", "Id");
        }
    }
}
