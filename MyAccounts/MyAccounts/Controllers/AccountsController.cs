using System.Collections.Generic;
using System.Web.Mvc;
using MvcPaging;
using MyAccounts.Models;
using MyAccounts.Models.ViewModels;
using MyAccounts.Repositories;
using MyAccounts.Services;

namespace MyAccounts.Controllers
{
    public class AccountsController : Controller
    {
        private readonly AccountsService _service;
        private readonly int pageSize = 10;

        public AccountsController()
        {
            var unitOfWork = new EFUnitOfWork();
            _service = new AccountsService(unitOfWork);
        }

        public ActionResult Index(int? page)
        {
            ViewData["DDLCategory"] = Categories();

            int pageIndex = page.HasValue ? page.Value - 1 : 0;

            AccountsViewModels model = new AccountsViewModels()
            {
                AccountsModels = _service.GetLists().ToPagedList(pageIndex, pageSize)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Categoryyy,Amounttt,Dateee,Remarkkk")] AccountBook accountBook)
        {
            if (ModelState.IsValid)
            {
                _service.Add(accountBook);
                _service.Save();

                return RedirectToAction("Index");
            }

            ViewData["DDLCategory"] = Categories();

            AccountsViewModels viewModel = new AccountsViewModels()
            {
                AccountsModels = _service.GetLists().ToPagedList(0, pageSize)
            };

            return View("Index", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxPost([Bind(Include = "Categoryyy,Amounttt,Dateee,Remarkkk")] AccountBook accountBook)
        {
            _service.Add(accountBook);
            _service.Save();

            return PartialView("_AccountsPartial", _service.GetLists().ToPagedList(0, pageSize));
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
