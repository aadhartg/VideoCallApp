

using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoCallConsultant.Models;
using System.Security; 
namespace VideoCallConsultant.ViewModels
{
    public class CustumValidationBooking: ValidationAttribute
    {
        ApplicationDbContext ApplicationDbContext = new ApplicationDbContext();
    
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null) {

               
                int BookingID = Convert.ToInt32(value);
                var Exist = ApplicationDbContext.UserAccountDetail.Where(x => x.BookingID == BookingID).FirstOrDefault();
                if (Exist == null)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("This Booking is already booked.");
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " is required");
            }
        }



    }
}