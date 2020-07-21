using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoCallConsultant.Models;

namespace VideoCallConsultant.EntityModels
{
    public class Sessions
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public DateTime UTCStartTime { get; set; }
        public DateTime UTCEndTime { get; set; }
        //public SessionType SessionType { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}