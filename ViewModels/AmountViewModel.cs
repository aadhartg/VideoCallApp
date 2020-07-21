using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoCallConsultant.ViewModels
{
    public class AmountViewModel
    {
        public int ID { get; set; }
        public float Amount { get; set; }
        public String  UserID { get; set; }
        public string CraditcardID { get; set; }
        public int BookingID { get; set; }
        public String Email { get; set; }
        public int BookingHour { get; set; }
        public int SessionType { get; set; }
        public int UserDetailID { get; set; }
    }
}