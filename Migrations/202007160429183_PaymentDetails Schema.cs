namespace VideoCallConsultant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentDetailsSchema : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentDetails", "UserDetailID", c => c.Int(nullable: false));
            AddColumn("dbo.PaymentDetails", "ResponseID", c => c.String());
            AddColumn("dbo.PaymentDetails", "TotalAmount", c => c.String());
            AddColumn("dbo.PaymentDetails", "Createddate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PaymentDetails", "Updateddate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PaymentDetails", "Status", c => c.String());
            AddColumn("dbo.PaymentDetails", "Intent", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentDetails", "Intent");
            DropColumn("dbo.PaymentDetails", "Status");
            DropColumn("dbo.PaymentDetails", "Updateddate");
            DropColumn("dbo.PaymentDetails", "Createddate");
            DropColumn("dbo.PaymentDetails", "TotalAmount");
            DropColumn("dbo.PaymentDetails", "ResponseID");
            DropColumn("dbo.PaymentDetails", "UserDetailID");
        }
    }
}
