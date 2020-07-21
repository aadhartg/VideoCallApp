using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using VideoCallConsultant.EntityModels;
using VideoCallConsultant.Helpers;
using VideoCallConsultant.Migrations.Models;
using VideoCallConsultant.Models;
using VideoCallConsultant.Services;
using VideoCallConsultant.ViewModels;
using static VideoCallConsultant.Services.Zoom;

namespace VideoCallConsultant.Controllers
{
    [Authorize]
    public class ConsultantController : BaseController
    {
        // GET: Consultant
        /// <summary>
        /// Consulant Index
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Consultant")]
        public ActionResult  Index()
        {
            try
            {
                DateTime week = DateTime.Now.Date.AddDays(7);
                var todaydate = DateTime.UtcNow.Date;
                BookingViewModel bookingViewModels = new BookingViewModel();
                List<Booking> bookingList = new List<Booking>();
                using (var db = GetDb())
                {
                    bookingList = db.Booking.ToList();
                }
                bookingViewModels.ThreeHourWebiner = bookingList.Where(z => z.UTCStartTime.Date >= todaydate && z.UTCStartTime.Date <= week && z.SessionType == 1 && z.UserId == User.Identity.GetUserId()).ToList();
                bookingViewModels.OneHourWebiner = bookingList.Where(z => z.UTCStartTime.Date >= todaydate && z.UTCStartTime.Date <= week && z.SessionType == 2 && z.UserId == User.Identity.GetUserId()).ToList();
                bookingViewModels.TenMinuteWebiner = bookingList.Where(z => z.UTCStartTime.Date >= todaydate && z.UTCStartTime.Date <= week && z.SessionType == 3 && z.UserId == User.Identity.GetUserId()).ToList();
                return View(bookingViewModels);
            }
            catch (Exception ex)
            {
                return View("Its due to backend process issue " + ex.ToString());
            }
        }


        [Authorize(Roles = "Consultant")]
        public ActionResult CreteZoomWebNar()
        {
            try
            {
              return Redirect("https://zoom.us/oauth/authorize?response_type=code&client_id=UdeBtrjSp6gVUpwlwWL3w&redirect_uri=https%3A%2F%2Flocalhost%3A44328%2FZoom%2FIndex");
            }
            catch (Exception ex)
            {
                return View("Its due to backend process issue " + ex.ToString());
            }
        }





       


        [Authorize(Roles = "User")]
        public ActionResult Booking()
        {
           

            DateTime week = DateTime.Now.Date.AddDays(7);
            var todaydate = DateTime.UtcNow.Date;
            BookingViewModel bookingViewModels = new BookingViewModel();
            List<Booking> bookingList = new List<Booking>();
            var db = GetDb();
            bookingList = db.Booking.ToList();
            string UserID = User.Identity.GetUserId();
            var UserAccountDetail = db.UserAccountDetail.Where(x => x.UserId == UserID).Select(x => x.BookingID).ToList();
            if (UserAccountDetail.Count()>0)
            {
            
                bookingViewModels.ThreeHourWebiner = bookingList.Where(z => z.UTCStartTime.Date >= todaydate && z.UTCStartTime.Date <= week && z.SessionType == 1 && z.UserId == "67cb4926-6397-42b8-8bc5-ec85dd3ce2a7"  && !UserAccountDetail.Contains(z.ID)).ToList();
                bookingViewModels.OneHourWebiner = bookingList.Where(z => z.UTCStartTime.Date >= todaydate && z.UTCStartTime.Date <= week && z.SessionType == 2 && z.UserId == "67cb4926-6397-42b8-8bc5-ec85dd3ce2a7" && !UserAccountDetail.Contains(z.ID)).ToList();
                bookingViewModels.TenMinuteWebiner = bookingList.Where(z => z.UTCStartTime.Date >= todaydate && z.UTCStartTime.Date <= week && z.SessionType == 3 && z.UserId == "67cb4926-6397-42b8-8bc5-ec85dd3ce2a7" && !UserAccountDetail.Contains(z.ID)).ToList();
                return View(bookingViewModels);
            }

            bookingViewModels.ThreeHourWebiner = bookingList.Where(z => z.UTCStartTime.Date >= todaydate && z.UTCStartTime.Date <= week && z.SessionType == 1 && z.UserId== "67cb4926-6397-42b8-8bc5-ec85dd3ce2a7").ToList();
            bookingViewModels.OneHourWebiner = bookingList.Where(z => z.UTCStartTime.Date >= todaydate && z.UTCStartTime.Date <= week && z.SessionType == 2 && z.UserId== "67cb4926-6397-42b8-8bc5-ec85dd3ce2a7").ToList();
            bookingViewModels.TenMinuteWebiner = bookingList.Where(z => z.UTCStartTime.Date >= todaydate && z.UTCStartTime.Date <= week && z.SessionType == 3 && z.UserId == "67cb4926-6397-42b8-8bc5-ec85dd3ce2a7").ToList();
            return View(bookingViewModels);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult Booking(BookingViewModel bookingViewModel)
        {
            DateTime week = DateTime.Now.Date.AddDays(7);
            var todaydate = DateTime.UtcNow.Date;
            List<Booking> bookingList = new List<Booking>();
            string UserID= User.Identity.GetUserId();
            var db = GetDb();
           if (ModelState.IsValid)
            {
                var UserAccountDetail = db.UserAccountDetail.Where(x => x.UserId == UserID && x.BookingID == bookingViewModel.BookingID).FirstOrDefault();
                if (UserAccountDetail == null)
                {
                    PayPalbusinessLogic obj = new PayPalbusinessLogic();
                    String Token = obj.GetPaypalToken();
                    bookingViewModel.UserID = UserID;
                    var Response = obj.SaveinfoinPaypalValut(Token, bookingViewModel);
                    if (Response.state == "ok")
                    {
                        var booking= db.Booking.Where(x =>  x.ID == bookingViewModel.BookingID).FirstOrDefault();
                        UserAccountDetails UserAccountDetails = new UserAccountDetails();
                        UserAccountDetails.UserId = UserID;
                        UserAccountDetails.BookingHour = bookingViewModel.BookingHour.ToString();
                        UserAccountDetails.CraditcardID = Response.id;
                        UserAccountDetails.BookingID = bookingViewModel.BookingID;
                        UserAccountDetails.FirstName = bookingViewModel.FirstName;
                        UserAccountDetails.LastName = bookingViewModel.LastName;
                        UserAccountDetails.Createddate = DateTime.Now;
                        UserAccountDetails.PhoneNumber = bookingViewModel.PhoneNumber;
                        UserAccountDetails.CVV = bookingViewModel.cvv.ToString();
                        UserAccountDetails.Email = bookingViewModel.Email;
                        UserAccountDetails.UpdatedDate = DateTime.Now;
                        UserAccountDetails.SessionComplete = false;
                        db.UserAccountDetail.Add(UserAccountDetails);
                        int recordsinserted = db.SaveChanges();
                        String Path = System.Web.HttpContext.Current.Server.MapPath(@"~/App_Data/Template/emailtemplate.html");
                        EmailSender.SendEmail(bookingViewModel.FirstName, bookingViewModel.Email, bookingViewModel.FirstName, Path, booking.ZoomURL, booking.UTCStartTime.ToString(), booking.UTCEndTime.ToString());
                        ViewBag.Success = "Session Booked Succesfully.";
                    }
                }
                else
                {
                    ViewBag.ErrMessage = "You have already booking this Sessions.";
                    bookingList = db.Booking.ToList();
                    bookingViewModel.ThreeHourWebiner = bookingList.Where(z => z.UTCStartTime.Date >= todaydate && z.UTCStartTime.Date <= week && z.SessionType == 1 && z.UserId == "67cb4926-6397-42b8-8bc5-ec85dd3ce2a7").ToList();
                    bookingViewModel.OneHourWebiner = bookingList.Where(z => z.UTCStartTime.Date >= todaydate && z.UTCStartTime.Date <= week && z.SessionType == 2 && z.UserId == "67cb4926-6397-42b8-8bc5-ec85dd3ce2a7").ToList();
                    bookingViewModel.TenMinuteWebiner = bookingList.Where(z => z.UTCStartTime.Date >= todaydate && z.UTCStartTime.Date <= week && z.SessionType == 3 && z.UserId == "67cb4926-6397-42b8-8bc5-ec85dd3ce2a7").ToList();
                    return View(bookingViewModel);
                }
            }
            bookingList = db.Booking.ToList();
            bookingViewModel.ThreeHourWebiner = bookingList.Where(z => z.UTCStartTime.Date >= todaydate && z.UTCStartTime.Date <= week && z.SessionType == 1 && z.UserId == "67cb4926-6397-42b8-8bc5-ec85dd3ce2a7").ToList();
            bookingViewModel.OneHourWebiner = bookingList.Where(z => z.UTCStartTime.Date >= todaydate && z.UTCStartTime.Date <= week && z.SessionType == 2 && z.UserId == "67cb4926-6397-42b8-8bc5-ec85dd3ce2a7").ToList();
            bookingViewModel.TenMinuteWebiner = bookingList.Where(z => z.UTCStartTime.Date >= todaydate && z.UTCStartTime.Date <= week && z.SessionType == 3 && z.UserId == "67cb4926-6397-42b8-8bc5-ec85dd3ce2a7").ToList();
            return View(bookingViewModel);
        
        }


      
        /// <summary>
        /// Payment Deduct through Carditcard ID
        /// </summary>
        /// <param name="BookingID"></param>
        /// <returns></returns>
        public ActionResult PaypalValutthroughDeductPayment(int BookingID=0)
        {
            if (BookingID != 0)
            {
                var db = GetDb();
                String UserID = User.Identity.GetUserId();
                PayPalbusinessLogic obj = new PayPalbusinessLogic();
                // Token Get;
                string Token = PaypalConfiguration.GetAccessToken();
                var UserAccountDetail = db.UserAccountDetail.Where(x => x.UserId == UserID && x.BookingID == BookingID).FirstOrDefault();
                var response = obj.GetCraditCardPaypalValut(Token, UserAccountDetail.CraditcardID);
                var _AmountViewModel = (from ep in db.UserAccountDetail
                                        join e in db.Booking on ep.BookingID equals e.ID
                                        join see in db.SessionType on e.SessionType equals see.ID
                                        where ep.UserId == UserID && ep.BookingID == BookingID
                                        select new AmountViewModel
                                        {
                                            ID = ep.ID,
                                            Amount = see.Price,
                                            UserID = ep.UserId,
                                            CraditcardID = ep.CraditcardID,
                                            Email = ep.Email,
                                            BookingID = ep.BookingID,
                                            SessionType = e.SessionType,
                                            UserDetailID = ep.ID
                                        }).FirstOrDefault();
                if (_AmountViewModel != null)
                {
                    var PapalvalutAmountDeductResponse = obj.AmountDeduct(_AmountViewModel);
                    if (PapalvalutAmountDeductResponse.state != "created")
                    {

                        PaymentDetails PaymentDetails = new PaymentDetails();
                        PaymentDetails.BookingID = _AmountViewModel.BookingID;
                        PaymentDetails.Createddate = DateTime.Now;
                        PaymentDetails.Updateddate = DateTime.Now;
                        PaymentDetails.Status = "Success";
                        PaymentDetails.UserId = _AmountViewModel.UserID;
                        PaymentDetails.TotalAmount = PapalvalutAmountDeductResponse.Amount;
                        PaymentDetails.Intent = PapalvalutAmountDeductResponse.intent;
                        PaymentDetails.ResponseID = PapalvalutAmountDeductResponse.id;
                        PaymentDetails.UserDetailID = _AmountViewModel.UserDetailID;
                        PaymentDetails.PaymentProcessed = true;
                        db.PaymentDetail.Add(PaymentDetails);
                    }



                }
            }
             return View();
           }




        /// <summary>
        /// Session Booking
        /// </summary>
        /// <param name="bookingdate"></param>
        /// <param name="card"></param>
        public ActionResult SessionBooking()
        {
            try
            {

                DateTime week = DateTime.Now.Date.AddDays(7);
                var todaydate = DateTime.UtcNow.Date;
                BookingViewModel bookingViewModels = new BookingViewModel();
                List<Booking> bookingList = new List<Booking>();
                String UserID= User.Identity.GetUserId();
                var db = GetDb();
                bookingList = db.Booking.ToList();
                var UserAccountDetail = db.UserAccountDetail.Where(x => x.UserId == UserID).Select(x => x.BookingID).ToList();
                bookingViewModels.ThreeHourWebiner = bookingList.Where(z => z.UTCStartTime.Date >= todaydate && z.UTCStartTime.Date <= week && z.SessionType == 1 && z.UserId == "67cb4926-6397-42b8-8bc5-ec85dd3ce2a7" && UserAccountDetail.Contains(z.ID)).ToList();
                bookingViewModels.OneHourWebiner = bookingList.Where(z => z.UTCStartTime.Date >= todaydate && z.UTCStartTime.Date <= week && z.SessionType == 2 && z.UserId == "67cb4926-6397-42b8-8bc5-ec85dd3ce2a7" && UserAccountDetail.Contains(z.ID)).ToList();
                bookingViewModels.TenMinuteWebiner = bookingList.Where(z => z.UTCStartTime.Date >= todaydate && z.UTCStartTime.Date <= week && z.SessionType == 3 && z.UserId == "67cb4926-6397-42b8-8bc5-ec85dd3ce2a7" && UserAccountDetail.Contains(z.ID)).ToList();
                
                return View(bookingViewModels);
            }
            catch (Exception ex)
            {
                return View("Its due to backend process issue " + ex.ToString());
            }
        }







        public void AddBooking3Hour(string bookingdate,CardDetail card)
        {
            string code = Session["code"].ToString();
            string redirectUrlAction = Session["url"].ToString();
            string token = Zoom.getAccessToken(code, redirectUrlAction);
            char[] delimiters = new char[] { '\n' };
            string[] datestr = bookingdate.Split(delimiters);
            DateTime bookingDate = Convert.ToDateTime(datestr[2] + " " + datestr[3]);
            bool result = false;
            Booking model = new Booking();
            model.UserId = User.Identity.GetUserId();
            model.SessionAttended = false;
            model.SessionExpired = false;
            model.SessionType = 1;
            model.URL = 1;
            model.UTCStartTime = bookingDate;
            model.UTCEndTime = bookingDate.AddHours(3);
            using (var db = GetDb())
            {
                db.Booking.Add(model);
                int recordsinserted = db.SaveChanges();
                if (recordsinserted > 0)
                    result = true;
            }
            DateTime strenddate = bookingDate;
            string startDate = bookingDate.ToString("s") + "Z";
            string endDate = strenddate.AddHours(3).ToString("s") + "Z";
            var response = Zoom.CreateZoomMeeting(token, startDate, endDate,180);
            var jObject = JObject.Parse(response.Content);
            string join_url = jObject.GetValue("join_url").ToString();
            string start_time = jObject.GetValue("start_time").ToString();
            string start_url = jObject.GetValue("start_url").ToString();
            Zoom.sendMail(User.Identity.GetUserName(),"Zoom Meeting","Your meeting is scheduled successfully. Here is url to Join " +start_url + ". It will be start on dated "+start_time+".");

        }

        public void AddBooking1Hour(string datetime)
        {
            string code = Session["code"].ToString();
            string redirectUrlAction = Session["url"].ToString();
            string token = Zoom.getAccessToken(code, redirectUrlAction);
            char[] delimiters = new char[] { '\n' };
            string[] datestr = datetime.Split(delimiters);
            DateTime bookingDate = Convert.ToDateTime(datestr[2] + " " + datestr[3]);
            bool result = false;
            Booking model = new Booking();
            model.UserId = User.Identity.GetUserId();
            model.SessionAttended = false;
            model.SessionExpired = false;
            model.SessionType = 2;
            model.URL = 1;
            model.UTCStartTime = bookingDate;
            model.UTCEndTime = bookingDate.AddHours(3);
            using (var db = GetDb())
            {
                db.Booking.Add(model);
                int recordsinserted = db.SaveChanges();
                if (recordsinserted > 0)
                    result = true;
            }
            DateTime strenddate = bookingDate;
            string startDate = bookingDate.ToString("s") + "Z";
            string endDate = strenddate.AddHours(3).ToString("s") + "Z";
            Zoom.CreateZoomMeeting(token, startDate, endDate,60);
        }

        public ActionResult Sessions()
        {
            return View();
        }

        public ActionResult UserBookings()
        {

            return View();
        }
    }
}