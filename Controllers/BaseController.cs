using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoCallConsultant.Models;

namespace VideoCallConsultant.Controllers
{
    public class BaseController : Controller
    {
        private ApplicationDbContext dbContext;

        private ApplicationDbContext GetScope()
        {
            if (dbContext == null)
            {
                dbContext = new ApplicationDbContext();
            }
            return dbContext;
        }
        virtual public ApplicationDbContext GetDb()
        {
            return GetScope();
        }
    }
}