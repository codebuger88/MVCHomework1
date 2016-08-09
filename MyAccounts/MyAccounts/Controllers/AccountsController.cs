using System.Web.Mvc;
using MyAccounts.Data;
using MyAccounts.Models;
using MyAccounts.Repositories;
using MyAccounts.Services;

namespace MyAccounts.Controllers
{
    public class AccountsController : Controller
    {
        private readonly AccountsService _service;

        public AccountsController()
        {
            var unitOfWork = new EFUnitOfWork();
            _service = new AccountsService(unitOfWork);
        }

        public ActionResult Index()
        {
            return View(_service.GetLists());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Categoryyy,Amounttt,Dateee,Remarkkk")] AccountBook model)
        {
            if (ModelState.IsValid)
            {
                _service.Add(model);
                _service.Save();
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
