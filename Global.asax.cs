using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace VideoCallConsultant
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //using (var ctx = new VideoCallsContext())
            //{
            //    var booking = new Booking();
            //    var paymentDetail = new PaymentDetail();
            //    var session = new Session();
            //    var sessionType = new SessionType();
            //    var userAccountDetail = new UserAccountDetail();
            //    ctx.bookings.Add(booking);
            //    ctx.PaymentDetails.Add(paymentDetail);
            //    ctx.Sessions.Add(session);
            //    ctx.SessionTypes.Add(sessionType);
            //    ctx.UserAccountDetails.Add(userAccountDetail);
            //    ctx.SaveChanges();
            //}

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
