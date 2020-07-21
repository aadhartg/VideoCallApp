using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoCallConsultant.EntityModels;

namespace VideoCallConsultant.ViewModels
{
    public class BookingViewModel
    {
        public List<Booking> OneHourWebiner { get; set; }
        public List<Booking> ThreeHourWebiner { get; set; }
        public List<Booking> TenMinuteWebiner { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please Enter Correct Email Address")]
         [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Type { get; set; }
        public int BookingID { get; set; }

        public string UserID { get; set; }
        [Required]
        [Display(Name = "Owner Name")]
        public string Owner { get; set; }
        [Required]
        [Display(Name = "CVV")]
        [Range(100, 999, ErrorMessage = "Please enter valid cvv number")]
        public int cvv { get; set; }
        [Display(Name = "CraditCardNumber")]
        [Required]
        [Range(100000000000, 9999999999999999999, ErrorMessage = "must be between 12 and 19 digits")]
        public long CraditCardNumber   { get; set; }
        [Display(Name = "Expiry Month")]
        [Range(1, 12, ErrorMessage = "Please enter month in mm format")]
        [Required]
        public int ValidTillMonth { get; set; }
        [Range(2020, 2099, ErrorMessage = "Please enter Year in yyyy format")]
        [Required]
        [Display(Name = "Expiry Year")]
        public int ValidTillYear { get; set; }
        public int BookingHour { get; set; }
        public string date { get; set; }
    }
}