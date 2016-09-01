using System.Web.Mvc;

namespace MyAccounts.Areas.MvcElmahDashboard.Controllers
{
    public class SharedController : Controller
    {
        protected override void HandleUnknownAction(string actionName)
        {
            this.View(actionName).ExecuteResult(this.ControllerContext);
        }
    }
}