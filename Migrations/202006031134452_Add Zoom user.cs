namespace VideoCallConsultant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddZoomuser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ZoomUsers",
                c => new
                    {
                        ZoomUserID = c.String(nullable: false, maxLength: 128),
                        UserID = c.String(),
                    })
                .PrimaryKey(t => t.ZoomUserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ZoomUsers");
        }
    }
}
