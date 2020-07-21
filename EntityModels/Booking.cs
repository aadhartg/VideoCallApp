using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoCallConsultant.Models;

namespace VideoCallConsultant.EntityModels
{
    public class Booking
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public int URL { get; set; }
        public DateTime UTCStartTime { get; set; }
        public DateTime UTCEndTime { get; set; }
        public int SessionType { get; set; }
        public bool SessionExpired { get; set; }
        public bool SessionAttended { get; set; }
        public string Description { get; set; }

       public string ZoomURL { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}