namespace VideoCallConsultant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserdetilsSchema : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAccountDetails", "FirstName", c => c.String());
            AddColumn("dbo.UserAccountDetails", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAccountDetails", "LastName");
            DropColumn("dbo.UserAccountDetails", "FirstName");
        }
    }
}
