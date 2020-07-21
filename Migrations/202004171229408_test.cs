namespace VideoCallConsultant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        URL = c.Int(nullable: false),
                        UTCStartTime = c.DateTime(nullable: false),
                        UTCEndTime = c.DateTime(nullable: false),
                        SessionType = c.Int(nullable: false),
                        SessionExpired = c.Boolean(nullable: false),
                        SessionAttended = c.Boolean(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PaymentDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        BookingID = c.Int(nullable: false),
                        PaymentProcessed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        UTCStartTime = c.DateTime(nullable: false),
                        UTCEndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)   
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SessionTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SessionDuration = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserAccountDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        CardVerified = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAccountDetails", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sessions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentDetails", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookings", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserAccountDetails", new[] { "UserId" });
            DropIndex("dbo.Sessions", new[] { "UserId" });
            DropIndex("dbo.PaymentDetails", new[] { "UserId" });
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropTable("dbo.UserAccountDetails");
            DropTable("dbo.SessionTypes");
            DropTable("dbo.Sessions");
            DropTable("dbo.PaymentDetails");
            DropTable("dbo.Bookings");
        }
    }
}
