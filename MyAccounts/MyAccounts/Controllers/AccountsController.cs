using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyAccounts.Data;

namespace MyAccounts.Controllers
{
    public class AccountsController : Controller
    {
        public ActionResult Index()
        {
            return View(DataHelper.GetAccounts());
        }

        [ChildActionOnly]
        public ActionResult ChildContent()
        {
            return View(DataHelper.Accounts());
        }
    }
}
