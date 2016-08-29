using System;
using System.Globalization;
using System.Web.Mvc;

namespace MyAccounts.Controllers
{
    public class ValidationController : Controller
    {
        // GET: Validation
        public ActionResult Index([Bind(Prefix = "AccountBook.Dateee")]string dateee)
        {
            bool isValidate = DateTime.ParseExact(dateee, "yyyy-MM-dd", CultureInfo.InvariantCulture) <= DateTime.Today;

            return Json(isValidate, JsonRequestBehavior.AllowGet);
        }
    }
}