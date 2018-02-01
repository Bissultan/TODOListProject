namespace ElectronicTasks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaskTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        TaskName = c.String(),
                        TaskText = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                        TermDate = c.DateTime(nullable: false),
                        IsDone = c.Boolean(nullable: false),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.TaskId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tasks");
        }
    }
}
