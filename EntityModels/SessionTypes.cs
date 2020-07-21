using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoCallConsultant.EntityModels
{
    public class SessionTypes
    {
        [Key]
        public int ID { get; set; }
        public int SessionDuration { get; set; }
        public float Price { get; set; }
    }
}