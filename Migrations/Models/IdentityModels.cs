using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using VideoCallConsultant.EntityModels;

namespace VideoCallConsultant.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LasttName { get; set; }
      //  public string UserRole { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("VideoCallConsultant", throwIfV1Schema: false)
        {
        }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<PaymentDetails> PaymentDetail { get; set; }
        public DbSet<Sessions> Session { get; set; }
        public DbSet<SessionTypes> SessionType { get; set; }
        public DbSet<UserAccountDetails> UserAccountDetail { get; set; }
        public DbSet<ZoomUser> ZoomUser { get; set; }
        

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}