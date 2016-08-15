using System.Web.Mvc;
using MyAccounts.Data;
using MyAccounts.Models;
using MyAccounts.Repositories;
using MyAccounts.Services;
using System.Collections.Generic;

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
            ViewData["DDLCategory"] = Categories();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Categoryyy,Amounttt,Dateee,Remarkkk")] AccountBook model)
        {
            if (ModelState.IsValid)
            {
                _service.Add(model);
                _service.Save();

                return RedirectToAction("Index");
            }

            ViewData["DDLCategory"] = Categories();

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult ChildContent()
        {
            return View(_service.GetLists());
        }

        private List<SelectListItem> Categories()
        {
            var categories = new List<SelectListItem>();
            categories.Add(new SelectListItem { Text = "請選擇", Value = "" });
            categories.Add(new SelectListItem { Text = "收入", Value = "0" });
            categories.Add(new SelectListItem { Text = "支出", Value = "1" });

            return categories;
        }
    }
}
