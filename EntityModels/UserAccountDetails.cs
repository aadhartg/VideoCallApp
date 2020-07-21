using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoCallConsultant.Models;

namespace VideoCallConsultant.EntityModels
{
    public class UserAccountDetails
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public string CraditcardID { get; set; }

        public int BookingID { get; set; }
        public DateTime Createddate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool CardVerified { get; set; }
        public string CVV { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BookingHour { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool SessionComplete { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}