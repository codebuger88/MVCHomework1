using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyAccounts.Controllers
{
    public class ValidationController : Controller
    {
        // GET: Validation
        public ActionResult Index(string dateee, string Categoryyy, string Amounttt)
        {
            bool isValidate = DateTime.ParseExact(dateee, "yyyy-MM-dd", CultureInfo.InvariantCulture) <= DateTime.Today;

            return Json(isValidate, JsonRequestBehavior.AllowGet);
        }
    }
}