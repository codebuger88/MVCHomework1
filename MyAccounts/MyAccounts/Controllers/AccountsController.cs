using System.Web.Mvc;
using MyAccounts.Data;
using MyAccounts.Models;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                AccountBook model = new AccountBook();
                if (TryUpdateModel(model, "", collection.AllKeys))
                {
                    _service.Add(model);
                    _service.Save();
                }
            }

            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult ChildContent()
        {
            return View(DataHelper.Accounts());
        }
    }
}
