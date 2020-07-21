using Newtonsoft.Json;
using PayPal;
using PayPal.Api.Payments;
using PayPal.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using VideoCallConsultant.Models;
using VideoCallConsultant.ViewModels;

namespace VideoCallConsultant.Migrations.Models
{
    public class PayPalbusinessLogic
    {
        public class PaypalTokenResponse
        {
            public string scope { get; set; }
            public string token_type { get; set; }
            public string expires_in { get; set; }
            public string refresh_token { get; set; }
            public string access_token { get; set; }
        }

        public class PapalvalutInsertResponse
        {
            public string id { get; set; }
            public string state { get; set; }
            public string expires_in { get; set; }
            public string external_customer_id { get; set; }
            public string type { get; set; }
        }

        public class GetPapalvalutResponse
        {
            public string id { get; set; }
            public string number { get; set; }
            public string state { get; set; }
            public int expire_year { get; set; }
            public int expire_month { get; set; }
            public string cvv2 { get; set; }
            public string external_customer_id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string type { get; set; }
        }



        public class PapalvalutAmountDeductResponse
        {
            public string id { get; set; }
            public string state { get; set; }
            public string intent { get; set; }
            public string create_time { get; set; }
            public int BookingID { get; set; }
            public string UserDetailID { get; set; }
            public string Amount { get; set; }
        }



        public string GetPaypalToken()
        {

             string accessToken;
            byte[] byte1 = Encoding.ASCII.GetBytes("grant_type=client_credentials");
            HttpWebRequest bearerReq = WebRequest.Create("https://api.sandbox.paypal.com/v1/oauth2/token") as HttpWebRequest;
            bearerReq.Accept = "application/json";
            bearerReq.Method = "POST";
            bearerReq.ContentType = "application/x-www-form-urlencoded";
            bearerReq.ContentLength = byte1.Length;
            bearerReq.KeepAlive = false;
            bearerReq.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(PaypalConfiguration.ClientId + ":" + PaypalConfiguration.ClientSecret)));
            Stream newStream = bearerReq.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);
            WebResponse bearerResp = bearerReq.GetResponse();
            using (var reader = new StreamReader(bearerResp.GetResponseStream(), Encoding.UTF8))
            {
                var response = reader.ReadToEnd();
                PaypalTokenResponse bearer = JsonConvert.DeserializeObject<PaypalTokenResponse>(response);
                accessToken = bearer.access_token;
            }

            return accessToken;
        }


        /// <summary>
        /// Paypal valut cradit card Information Save
        /// </summary>
        /// <returns></returns>
        public PapalvalutInsertResponse SaveinfoinPaypalValut(String accessToken, BookingViewModel BookingViewModel)
        {
            PapalvalutInsertResponse Response = new PapalvalutInsertResponse();
            String Status = "false";
            try
            {

                HttpWebRequest bearerReq1 = WebRequest.Create("https://api.sandbox.paypal.com/v1/vault/credit-cards/") as HttpWebRequest;
                bearerReq1.Accept = "application/json";
                bearerReq1.Method = "POST";
                bearerReq1.ContentType = "application/json";
                // bearerReq1.ContentLength = input;
                bearerReq1.KeepAlive = false;
                bearerReq1.Headers.Add("Authorization", "Bearer " + accessToken);
                using (var streamWriter = new StreamWriter(bearerReq1.GetRequestStream()))
                {
                    var billingaddress = new
                    {
                            line1 = "52 N Main St",
                            city = "Johnstown",
                            country_code = "US",
                            postal_code = "43210",
                            state = "OH",
                           // phone = BookingViewModel.PhoneNumber
                    };

                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        number = BookingViewModel.CraditCardNumber,
                        type = BookingViewModel.Type.ToLower(),
                        expire_month = BookingViewModel.ValidTillMonth,
                        expire_year = BookingViewModel.ValidTillYear,
                        cvv2 = BookingViewModel.cvv,
                        first_name = BookingViewModel.FirstName,
                        last_name = BookingViewModel.LastName,
                        billing_address = billingaddress,
                        external_customer_id = BookingViewModel.UserID


                    });

                    streamWriter.Write(json);
                }
                WebResponse bearerResp1 = bearerReq1.GetResponse();
                using (var reader1 = new StreamReader(bearerResp1.GetResponseStream()))
                {
                    var result = reader1.ReadToEnd();
                    Response = JsonConvert.DeserializeObject<PapalvalutInsertResponse>(result);

                }
                return Response;

            }
            catch (Exception ex)
            {
                Status = "false";
                Response.state = Status;
                return Response;
            }

        }



        public GetPapalvalutResponse GetCraditCardPaypalValut(String accessToken, String cradit_card_ID)
        {
            GetPapalvalutResponse Response = new GetPapalvalutResponse();
            String Status = "false";
            try
            {

                HttpWebRequest bearerReq1 = WebRequest.Create("https://api.sandbox.paypal.com/v1/vault/credit-cards/"+ cradit_card_ID +"") as HttpWebRequest;
                bearerReq1.Accept = "application/json";
                bearerReq1.Method = "Get";
                bearerReq1.ContentType = "application/json";
                // bearerReq1.ContentLength = input;
                bearerReq1.KeepAlive = false;
                bearerReq1.Headers.Add("Authorization",  accessToken);
                WebResponse bearerResp1 = bearerReq1.GetResponse();
                using (var reader1 = new StreamReader(bearerResp1.GetResponseStream()))
                {
                    var result = reader1.ReadToEnd();
                    Response = JsonConvert.DeserializeObject<GetPapalvalutResponse>(result);

                }
                return Response;

            }
            catch (Exception ex)
            {
                Status = "false";
                Response.state = Status;
                return Response;
            }

        }

        public PapalvalutAmountDeductResponse AmountDeduct(AmountViewModel AmountViewModel)
        {
            PapalvalutAmountDeductResponse PapalvalutAmountDeductResponse = new PapalvalutAmountDeductResponse();
            try
            {
                  var payer = new Payer
                  {
                    payment_method = "credit_card",
                    funding_instruments = new List<FundingInstrument>(),
                    payer_info = new PayerInfo
                    {
                        email = AmountViewModel.Email,
                        
                    }
                };

                var creditCard = new CreditCard();

                if (!string.IsNullOrEmpty(AmountViewModel.CraditcardID))
                {
                    payer.funding_instruments.Add(new FundingInstrument()
                    {
                        credit_card_token = new CreditCardToken()
                        {
                            credit_card_id = AmountViewModel.CraditcardID
                        }
                    });
                }
                String Description = null;
                if (AmountViewModel.SessionType == 1)
                {
                    Description = "Payment Deduction for 3Hourwebnar Sessions";
                }
                else
                {
                    Description = "Payment Deduction for 1Hourwebnar Sessions";
                }
               
                var transaction = new Transaction
                {
                    amount = new Amount
                    {
                        currency = "USD",
                        total = AmountViewModel.Amount.ToString()
                    },
                    description = Description,

                };

                var payment = new Payment()
                {
                    intent = "sale",
                    payer = payer,
                    transactions = new List<Transaction>() { transaction }
                };
                APIContext apiContext = PaypalConfiguration.GetAPIContext();
                var createdPayment = payment.Create(apiContext);
                //    TempData["info"] = createdPayment.id;
                bool Status = false;
                if (createdPayment.state == "approved")
                {
                    foreach (var item in createdPayment.transactions)
                    {
                        var amot = item.amount;
                        PapalvalutAmountDeductResponse.Amount = amot.total;

                    };
                    PapalvalutAmountDeductResponse.id = createdPayment.id;
                    PapalvalutAmountDeductResponse.create_time = createdPayment.create_time;
                    PapalvalutAmountDeductResponse.state = createdPayment.state;
                    PapalvalutAmountDeductResponse.intent = createdPayment.intent;
                   
                }



                else
                {
                    PapalvalutAmountDeductResponse.state ="false";
                }
                    return PapalvalutAmountDeductResponse;
                }
            catch (Exception ex)
            {
                
            }

            return PapalvalutAmountDeductResponse;
        }





    }
}