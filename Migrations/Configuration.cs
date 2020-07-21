namespace VideoCallConsultant.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<VideoCallConsultant.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true ;
            ContextKey = "VideoCallConsultant.Models.ApplicationDbContext";
        }

        protected override void Seed(VideoCallConsultant.Models.ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Id,
              new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Id = "1", Name = "Consultant" },
              new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Id = "2", Name = "User" }
              );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
