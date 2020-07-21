namespace VideoCallConsultant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookingSchema : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "ZoomURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "ZoomURL");
        }
    }
}
