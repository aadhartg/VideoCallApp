using PayPal;
using PayPal.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoCallConsultant.Models
{
    public static class PaypalConfiguration
    {
        //Variables for storing the clientID and clientSecret key
        public readonly static string ClientId;
        public readonly static string ClientSecret;

        //Constructor
        static PaypalConfiguration()
        {
            // var config = GetConfig();
            ClientId = "ASruoAku7oJbb-4zt4sLVmSSBThKIV1VZVRqMYP93_sBa8Liur4vHGP06oW52sWPUg83rk0aMQqRJP5D";
           ClientSecret = "EHCjwj5usvcil1wcwfSt3KVEG4KKuIs6ZQeX4wCT3I3D51DGr5jI9WpLJiF9jPGFaLafH2axYye6g1uL";
           

//            ClientId = "ARI8JfhFcMf7NUIQOqr2Ektn9dLW_WFpA_zb_NFW-VWHgDusl-2mbc1tiA6M4KNfmp_G1NMj0ce4jyAz";
  //          ClientSecret = "EPLvG6hcNSkF4FppCtTFtJ_g4h27aL_j1Kbo6a89o_wobfSRJw0g74FZp0vhwiWzb2KNC8dWtN9SQB_g";
          }

        // getting properties from the web.configs
        public static Dictionary<string, string> GetConfig()
        {
            return ConfigManager.Instance.GetProperties();
        }

        public static string GetAccessToken()
        {
            // getting accesstocken from paypal               
            Dictionary<string, string> payPalConfig = new Dictionary<string, string>();
            payPalConfig.Add("mode", "sandbox");
            OAuthTokenCredential tokenCredential = new OAuthTokenCredential("ASruoAku7oJbb-4zt4sLVmSSBThKIV1VZVRqMYP93_sBa8Liur4vHGP06oW52sWPUg83rk0aMQqRJP5D", "EHCjwj5usvcil1wcwfSt3KVEG4KKuIs6ZQeX4wCT3I3D51DGr5jI9WpLJiF9jPGFaLafH2axYye6g1uL", payPalConfig);
            string accessToken = tokenCredential.GetAccessToken();

            //    string accessToken = new OAuthTokenCredential
            //(ClientId, ClientSecret, GetConfig()).GetAccessToken();

            return accessToken;
        }

        public static APIContext GetAPIContext()
        {
            // return apicontext object by invoking it with the accesstoken
            Dictionary<string, string> payPalConfig = new Dictionary<string, string>();
            payPalConfig.Add("mode", "sandbox");

            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = payPalConfig;
           
            return apiContext;
        }
    }
}