using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace VideoCallConsultant.Helpers
{
    public class EmailSender
    {
       

        public static void SendEmail(string UserName, string emailaddress, string firstName, string hostingURl, String URL, string Startdate, string Enddate)
        {
            //Fetching Settings from cWEB.CONFIG file.  
            string emailSender = "talentelgia3608141@gmail.com";
            string emailSenderPassword = "(MIR@2010)";
            string emailSenderHost = "smtp.gmail.com";
            int emailSenderPort = 587;
            Boolean emailIsSSL = true;
             //Fetching Email Body Text from EmailTemplate File.  
            StreamReader str = new StreamReader(hostingURl);
            string MailText = str.ReadToEnd();
            str.Close();
            //Repalce [geturl]  to  forgetpassword link page 
            //MailText = MailText.Replace("[geturl]", hostingURl+$@"\forgetpassword\" + Linkid.ToString());
            MailText = MailText.Replace("[geturl]",  ""+ URL.ToString());
            MailText = MailText.Replace("{FirstName}", "" + firstName.ToString());
            MailText = MailText.Replace("{Start}", "" + Startdate.ToString());
            MailText = MailText.Replace("{end}", "" + Enddate.ToString());
            string subject = "VideoCallApp Webnar Metting::";


            //Base class for sending email  
            MailMessage _mailmsg = new MailMessage();

           
            //Make TRUE because our body text is html  
            _mailmsg.IsBodyHtml = true;

            //Set From Email ID  
            _mailmsg.From = new MailAddress(emailSender);

            //Set To Email ID  
            _mailmsg.To.Add(emailaddress.ToString());

            //Set Subject  
            _mailmsg.Subject = subject;

            //Set Body Text of Email   
            _mailmsg.Body = MailText;
           
            //Now set your SMTP   
            SmtpClient _smtp = new SmtpClient();

            //Set HOST server SMTP detail  
            _smtp.Host = emailSenderHost;

            //Set PORT number of SMTP  
            _smtp.Port = emailSenderPort;

            //Set SSL --> True / False  
            _smtp.EnableSsl = emailIsSSL;
          //  _smtp.UseDefaultCredentials = true;
          //  _smtp.UseDefaultCredentials = true;
          //  _smtp.UseDefaultCredentials = true;
          
            //Set Sender UserEmailID, Password  
            NetworkCredential _network = new NetworkCredential(emailSender, emailSenderPassword);
            _smtp.Credentials = _network;

            //Send Method will send your MailMessage create above.  
            _smtp.Send(_mailmsg);
        }


        public static void SendEmail(string emailaddress, String RootPath, string RandomNumber, string Mode)
        {
            //Fetching Settings from WEB.CONFIG file.  
            string emailSender = "qachd15@gmail.com";
            string emailSenderPassword = "Welcome@2019";
            string emailSenderHost = "smtp.gmail.com";
            int emailSenderPort = 587;
            Boolean emailIsSSL = true;

            //Fetching Email Body Text from EmailTemplate File.  


            // StreamReader str = new StreamReader();
            //string MailText = str.ReadToEnd();
            //str.Close();

            //Repalce [geturl]  to  forgetpassword link page 
            //MailText = MailText.Replace("[geturl]", hostingURl+$@"\forgetpassword\" + Linkid.ToString());
            //  MailText = MailText.Replace("[geturl]", $@"http://localhost:53687\forgetpassword\" + Linkid.ToString());
            string subject = "AssignQ :: Email Verification";


            //Base class for sending email  
            MailMessage _mailmsg = new MailMessage();

            //Make TRUE because our body text is html  
            _mailmsg.IsBodyHtml = true;

            //Set From Email ID  
            _mailmsg.From = new MailAddress(emailSender);

            //Set To Email ID  
            _mailmsg.To.Add(emailaddress.ToString());

            //Set Subject  
            _mailmsg.Subject = subject;

            //Set Body Text of Email   
            _mailmsg.Body = "<a target='_blank' href = 'http://localhost:53687/settings/" + Mode + "/" + RandomNumber + "'>Verify Communication Channel</a>";


            //Now set your SMTP   
            SmtpClient _smtp = new SmtpClient();

            //Set HOST server SMTP detail  
            _smtp.Host = emailSenderHost;

            //Set PORT number of SMTP  
            _smtp.Port = emailSenderPort;

            //Set SSL --> True / False  
            _smtp.EnableSsl = emailIsSSL;

            //Set Sender UserEmailID, Password  
            NetworkCredential _network = new NetworkCredential(emailSender, emailSenderPassword);
            _smtp.Credentials = _network;

            //Send Method will send your MailMessage create above.  
            _smtp.Send(_mailmsg);
        }

         





    }
}
