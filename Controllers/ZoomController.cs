using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using VideoCallConsultant.Models;
using VideoCallConsultant.EntityModels;
using VideoCallConsultant.Helpers;
using VideoCallConsultant.Migrations;

namespace VideoCallConsultant.Controllers
{
    public class ZoomController : BaseController
    {
        // GET: Zoom
        public ActionResult Index(String Code)
        {

            try
               {
              String Userid = User.Identity.GetUserId();
                // Get Zoom Token through Code
              String Token = GetToken(Code);
              ZoomUserResponse ZoomUserResponse = GetUserInfo(Token);
              int bookingID=    Convert.ToInt32(TempData["BookID"]);
              var db = GetDb();
                var booking = db.Booking.ToList();
                foreach(var item in booking)
                {
                    var startdate = String.Format("{0:s}", item.UTCStartTime) + "Z";
                    var enddate = String.Format("{0:s}", item.UTCEndTime) + "Z";
                    CreateZoomMeeting(Token, startdate, enddate, 40, ZoomUserResponse , item);

                }
               
              return RedirectToAction("Index", "Consultant");
                
            }
            catch (Exception ex)
             {
             }
             return View();
        }


        public ActionResult Notification()
        {

            try
            {
                String Userid = User.Identity.GetUserId();
                
            }
            catch (Exception ex)
            {
            }
            return View();
        }



        public string GetToken( String Code)
        {
            String AccessToken = null;
            string URl = "https://localhost:44328/Zoom/Index";
            byte[] byte1 = Encoding.ASCII.GetBytes("grant_type=authorization_code&code=" + Code + "&redirect_uri=" + URl + "");
            HttpWebRequest bearerReq = WebRequest.Create("https://zoom.us/oauth/token") as HttpWebRequest;
            bearerReq.Accept = "application/json";
            bearerReq.Method = "POST";
            bearerReq.ContentType = "application/x-www-form-urlencoded";
            bearerReq.ContentLength = byte1.Length;
            bearerReq.KeepAlive = false;
            bearerReq.UseDefaultCredentials = true;
            bearerReq.PreAuthenticate = true;
            bearerReq.Credentials = CredentialCache.DefaultCredentials;
            // bearerReq.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            bearerReq.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("UdeBtrjSp6gVUpwlwWL3w" + ":" + "mpLop8166lIcCnZH7UYahjtVXf4n5PU7")));
            Stream newStream = bearerReq.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);
            WebResponse bearerResp = bearerReq.GetResponse();
            using (var reader = new StreamReader(bearerResp.GetResponseStream(), Encoding.UTF8))
            {
                var response = reader.ReadToEnd();
                ZoomTokenResponse ZoomTokenResponse = JsonConvert.DeserializeObject<ZoomTokenResponse>(response);
                AccessToken = ZoomTokenResponse.access_token;
            }

            return AccessToken;

        }

        public string RefreshToken(String Code, string Token )
        {
            String AccessToken = null;
            byte[] byte1 = Encoding.ASCII.GetBytes("grant_type=refresh_token&refresh_token=" + Token + "");
            HttpWebRequest bearerReq = WebRequest.Create("https://zoom.us/oauth/token") as HttpWebRequest;
            bearerReq.Accept = "application/json";
            bearerReq.Method = "POST";
            bearerReq.ContentType = "application/x-www-form-urlencoded";
            bearerReq.ContentLength = byte1.Length;
            bearerReq.KeepAlive = false;
            bearerReq.UseDefaultCredentials = true;
            bearerReq.PreAuthenticate = true;
            bearerReq.Credentials = CredentialCache.DefaultCredentials;
            // bearerReq.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            bearerReq.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("xYQX5LqmSmmgs399u4cmXg" + ":" + "EGgWU3APKH0IVEZDxJYKDTRgb5q6kvfN")));
            Stream newStream = bearerReq.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);
            WebResponse bearerResp = bearerReq.GetResponse();
            using (var reader = new StreamReader(bearerResp.GetResponseStream(), Encoding.UTF8))
            {
                var response = reader.ReadToEnd();
                ZoomTokenResponse ZoomTokenResponse = JsonConvert.DeserializeObject<ZoomTokenResponse>(response);
                AccessToken = ZoomTokenResponse.access_token;
            }

            return AccessToken;

        }
        public ZoomUserResponse GetUserInfo(String accessToken)
        {

            ZoomUserResponse ZoomUserResponse = new ZoomUserResponse();
            try
            {

                HttpWebRequest bearerReq1 = WebRequest.Create("https://api.zoom.us/v2/users/me") as HttpWebRequest;
                bearerReq1.Accept = "application/json";
                bearerReq1.Method = "Get";
                bearerReq1.ContentType = "application/json";
                // bearerReq1.ContentLength = input;
                bearerReq1.KeepAlive = false;
                //bearerReq1.UseDefaultCredentials = true;
                //bearerReq1.PreAuthenticate = true;
                //bearerReq1.Credentials = CredentialCache.DefaultCredentials;
                bearerReq1.Headers.Add("Authorization", "Bearer " + accessToken);
                
                WebResponse bearerResp1 = bearerReq1.GetResponse();
                using (var reader1 = new StreamReader(bearerResp1.GetResponseStream()))
                {
                    var result = reader1.ReadToEnd();
                    ZoomUserResponse = JsonConvert.DeserializeObject<ZoomUserResponse>(result);
                }

                return ZoomUserResponse;
            }
            catch (Exception ex)
            {
                //
                
            }
            return ZoomUserResponse;
        }

        //public String GetWebnarInfo(String accessToken, ZoomUserResponse ZoomUserResponse)
        //{

        //    String Status = "false";
        //    try
        //    {
        //        //var client = new RestClient("https://api.zoom.us/v2/users/me/webinars");
        //        //var request = new RestRequest(Method.POST);
        //        //request.AddHeader("content-type", "application/json");
        //        //request.AddHeader("authorization", "Bearer "+accessToken);
        //        //request.AddParameter("application/json", "{\"topic\":\"Test Webinar\",\"type\":5,\"start_time\":\"2020-09-20T06:59:00Z\",\"duration\":\"60\",\"timezone\":\"America/Los_Angeles\",\"password\":\"avfhfgh\",\"agenda\":\"Test Webinar\",\"recurrence\":{\"type\":1,\"repeat_interval\":1,\"end_date_time\":\"2020-09-22T06:59:00Z\"},\"settings\":{\"host_video\":\"true\",\"panelists_video\":\"true\",\"practice_session\":\"true\",\"hd_video\":\"true\",\"approval_type\":0,\"registration_type\":2,\"audio\":\"both\",\"auto_recording\":\"none\",\"enforce_login\":\"false\",\"close_registration\":\"true\",\"show_share_button\":\"true\",\"allow_multiple_devices\":\"false\",\"registrants_email_notification\":\"true\"}}", ParameterType.RequestBody);
        //        //IRestResponse response = client.Execute(request);


        //        String Userid = User.Identity.GetUserId();
        //        HttpWebRequest bearerReq1 = WebRequest.Create("https://api.zoom.us/v2/users/" + ZoomUserResponse.id + "/meetings") as HttpWebRequest;

        //        // HttpWebRequest bearerReq1 = WebRequest.Create("https://api.zoom.us/v2/accounts/"+ ZoomUserResponse.account_id + "/users/" + ZoomUserResponse.id + "/webinars") as HttpWebRequest;
        //        bearerReq1.Accept = "application/json";
        //        bearerReq1.Method = "Post";
        //        bearerReq1.ContentType = "application/json";
        //        // bearerReq1.ContentLength = input;
        //        bearerReq1.KeepAlive = false;   
        //        bearerReq1.UseDefaultCredentials = true;
        //        bearerReq1.PreAuthenticate = true;
        //        bearerReq1.Credentials = CredentialCache.DefaultCredentials;
        //        bearerReq1.Headers.Add("Authorization", "Bearer " + accessToken);
            
        //        using (var streamWriter = new StreamWriter(bearerReq1.GetRequestStream()))
        //        {   
        //            string json = new JavaScriptSerializer().Serialize(new
        //            {
        //               topic="Test Webinar",
        //               type=5,
        //               start_time="2020-09-20T06:59:00Z",
        //               duration="60",
        //               timezone= "America/Los_Angeles",
        //               password="Matrid@12$$",
        //               agenda="Test Webinar",
        //                recurrence= new
        //                {
        //                    type = 1,
        //                    repeat_interval = 1,
        //                    weekly_days="Monday",
        //                    monthly_day=20,
        //                     monthly_week= 1,
        //                    monthly_week_day = 1,
        //                    end_times =1,
        //                    end_date_time = "2020-09-22T06:59:00Z"
        //                },
        //                settings = new {

        //                    host_video = "true",
        //                    panelists_video = "true",
        //                    practice_session = "true",
        //                    hd_video = "true",
        //                    approval_type = 0,
        //                    registration_type = 2,
        //                    audio = "both",
        //                    auto_recording = "none",
        //                    enforce_login = "false",
        //                    close_registration = "true",
        //                    show_share_button = "true",
        //                    allow_multiple_devices = "false",
        //                    registrants_email_notification = "true"
        //                },
        //            });

        //            streamWriter.Write(json);
        //        }

        //        WebResponse bearerResp1 = bearerReq1.GetResponse();
        //        using (var reader1 = new StreamReader(bearerResp1.GetResponseStream()))
        //        {
        //            var result = reader1.ReadToEnd();
                   
        //        }
        //        Status = "true";
        //        return Status;

        //    }
        //    catch (Exception ex)
        //    {

        //        return Status;
        //    }

        //}


        public  IRestResponse CreateZoomMeeting(string token, string startDate, string endDate, int duration, ZoomUserResponse ZoomUserResponse, Booking booking)
        {
            
            var client = new RestClient("https://api.zoom.us/v2/users/"+ ZoomUserResponse .id+"/meetings");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + token);
            CreateMeeting createMeeting = new CreateMeeting();
            createMeeting.topic = "abc";
            createMeeting.type = 2;
            createMeeting.start_time = startDate;
            createMeeting.duration = duration;
            createMeeting.timezone = "";
             recurrence recurrence = new recurrence();
            recurrence.type = 1;
            recurrence.repeat_interval = 0;
            recurrence.weekly_days = 1;
            recurrence.monthly_day = 0;
            recurrence.monthly_week = -1;
            recurrence.monthly_week_day = 1;
            recurrence.end_times = 0;
            recurrence.end_date_time = endDate;
            settings settings = new settings();
            settings.host_video = true;
            settings.participant_video = true;
            settings.cn_meeting = false;
            settings.in_meeting = false;
            settings.join_before_host = false;
            settings.mute_upon_entry = false;
            settings.watermark = false;
            settings.use_pmi = false;
            settings.approval_type = 2;
            settings.registration_type = 1;
            settings.audio = "both";
            settings.auto_recording = "none";
            settings.enforce_login = true;
            settings.enforce_login_domains = "";

            createMeeting.recurrence = recurrence;
            createMeeting.settings = settings;

            string json = JsonConvert.SerializeObject(createMeeting);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResponseMeeting ResponseMeeting = JsonConvert.DeserializeObject<ResponseMeeting>(response.Content.ToString());
            var db = GetDb();
            booking.ZoomURL = ResponseMeeting.join_url;
            db.SaveChanges();
            return response;
        }



        public String GetUserList(String accessToken, ZoomUserResponse ZoomUserResponse)
        {

            String Status = "false";
            try
            {
                String Userid = User.Identity.GetUserId();


                HttpWebRequest bearerReq1 = WebRequest.Create("https://api.zoom.us/v2/accounts/" + ZoomUserResponse.account_id + "/users") as HttpWebRequest;
                bearerReq1.Accept = "application/json";
                bearerReq1.Method = "Post";
                bearerReq1.ContentType = "application/json";
                // bearerReq1.ContentLength = input;
                bearerReq1.KeepAlive = false;
                bearerReq1.UseDefaultCredentials = true;
                bearerReq1.PreAuthenticate = true;
                bearerReq1.Credentials = CredentialCache.DefaultCredentials;
                bearerReq1.Headers.Add("Authorization", "Bearer " + accessToken);

                WebResponse bearerResp1 = bearerReq1.GetResponse();
                using (var reader1 = new StreamReader(bearerResp1.GetResponseStream()))
                {
                    var result = reader1.ReadToEnd();

                }
                Status = "true";
                return Status;

            }
            catch (Exception ex)
            {

                return Status;
            }

        }


    }
    public class ZoomTokenResponse
    {
        public string scope { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string access_token { get; set; }
    }

    public class ZoomUserResponse
    {
        public string id { get; set; }
        public string account_id { get; set; }
        public string phone_number { get; set; }
        public string personal_meeting_url { get; set; }
        public string email { get; set; }
       public string role_name { get; set; }
    }

    public class ResponseMeeting { 

        public string join_url { get; set; }


    }
}