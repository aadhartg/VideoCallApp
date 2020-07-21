namespace VideoCallConsultant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserDetailsSchema : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAccountDetails", "BookingHour", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAccountDetails", "BookingHour");
        }
    }
}
