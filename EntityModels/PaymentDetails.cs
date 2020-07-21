using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoCallConsultant.Models;

namespace VideoCallConsultant.EntityModels
{
    public class PaymentDetails
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }

        public int UserDetailID { get; set; }

        public String ResponseID { get; set; }

        public String TotalAmount { get; set; }
        public DateTime Createddate { get; set; }

        public DateTime Updateddate { get; set; }
        public String Status { get; set; }
        public int BookingID { get; set; }
        public bool PaymentProcessed { get; set; }
        public String  Intent { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}