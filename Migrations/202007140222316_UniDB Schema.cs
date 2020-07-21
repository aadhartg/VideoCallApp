namespace VideoCallConsultant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniDBSchema : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAccountDetails", "CVV", c => c.String());
            AddColumn("dbo.UserAccountDetails", "PhoneNumber", c => c.String());
            AddColumn("dbo.UserAccountDetails", "Email", c => c.String());
            AddColumn("dbo.UserAccountDetails", "CraditcardID", c => c.String());
            AddColumn("dbo.UserAccountDetails", "BookingID", c => c.Int(nullable: false));
            AddColumn("dbo.UserAccountDetails", "Createddate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserAccountDetails", "UpdatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserAccountDetails", "SessionComplete", c => c.Boolean(nullable: false));
      
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAccountDetails", "SessionComplete");
            DropColumn("dbo.UserAccountDetails", "UpdatedDate");
            DropColumn("dbo.UserAccountDetails", "Createddate");
            DropColumn("dbo.UserAccountDetails", "BookingID");
            DropColumn("dbo.UserAccountDetails", "CraditcardID");
            DropColumn("dbo.UserAccountDetails", "Email");
            DropColumn("dbo.UserAccountDetails", "PhoneNumber");
            DropColumn("dbo.UserAccountDetails", "CVV");

        }
    }
}
