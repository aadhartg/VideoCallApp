using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using VideoCallConsultant.Models;

namespace VideoCallConsultant.Services
{
    public static class Zoom
    {
      
        public static string getAccessToken(string code,string redirectUrlAction)
        {
            var client = new RestClient("https://zoom.us/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("code", code);
            request.AddParameter("redirect_uri", redirectUrlAction);
            request.AddHeader("Authorization", "Basic dk5OckttYk9UaEt4QTYwSl9CZzBzdzo3Y2NPcWJOS0ZIOFo3N2NWNFU0bjF6T2l4eFRsMTJUZw==");
            IRestResponse response = client.Execute(request);
            var jObject = JObject.Parse(response.Content);
            string token = jObject.GetValue("access_token").ToString();
            return token;
        }

        public static IRestResponse CreateZoomMeeting(string token,string startDate,string endDate,int duration )
        {
            var client = new RestClient("https://api.zoom.us/v2/users/"+ "VPAkCe2vR9mdy7AIi5n79w" + "/meetings");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + token);
            CreateMeeting createMeeting = new CreateMeeting();
            createMeeting.topic = "Test";
            createMeeting.type = 2;
            createMeeting.start_time = startDate;
            createMeeting.duration = duration;
            createMeeting.timezone = "";
            createMeeting.topic = "";
            createMeeting.topic = "";

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
            return response;
        }

        public static bool sendMail(string receiver, string subject, string message)
        {
            try
            {
                    var senderEmail = new MailAddress("VideoConsultancy@gmail.com", "Jamil");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "User@123";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static IRestResponse getZoomUsers(string token)
        {
            var client = new RestClient("https://api.zoom.us/v2/users");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Token", "Bearer " + token);
            IRestResponse response = client.Execute(request);
            return response;
        }

        public static async Task<Token> TesttokenAsync()
        {
            HttpClient client = new HttpClient();
            string baseAddress = @"https://zoom.us/oauth/token";

            string grant_type = "authorization_code";
            string redirect_uri = "ovCmQAUSBZ_2JpsTVi6Sdah6VlaIdHqLg";
            string code = "8NpGLQXYyV_2JpsTVi6Sdah6VlaIdHqLg";

            var form = new Dictionary<string, string>
                {
                    {"grant_type", grant_type},
                    {"code", code},
                    {"redirect_uri", redirect_uri},
                };

            HttpResponseMessage tokenResponse = await client.PostAsync(baseAddress, new FormUrlEncodedContent(form));
            var jsonContent = await tokenResponse.Content.ReadAsStringAsync();
            Token tok = JsonConvert.DeserializeObject<Token>(jsonContent);
            return tok;
        }


        public class Token
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }

            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; }
        }
    }
}