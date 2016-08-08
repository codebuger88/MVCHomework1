using System.Web.Mvc;
using MyAccounts.Data;
using MyAccounts.Services;

namespace MyAccounts.Controllers
{
    public class AccountsController : Controller
    {
        private readonly AccountsService _service;

        public AccountsController()
        {
            _service = new AccountsService();
        }

        public ActionResult Index()
        {
            return View(_service.GetLists());
        }

        [ChildActionOnly]
        public ActionResult ChildContent()
        {
            return View(DataHelper.Accounts());
        }
    }
}
