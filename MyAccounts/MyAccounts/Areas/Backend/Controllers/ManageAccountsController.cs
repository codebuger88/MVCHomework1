using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using MvcPaging;
using MyAccounts.Models;
using MyAccounts.Models.ViewModels;
using MyAccounts.Repositories;
using MyAccounts.Services;

namespace MyAccounts.Areas.Backend.Controllers
{
    public class ManageAccountsController : Controller
    {
        private readonly AccountsService _service;
        private readonly int pageSize = 10;

        public ManageAccountsController()
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

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = _service.GetSingleOrDefault(x => x.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }

            ViewData["DDLCategory"] = Categories(model.Categoryyy);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Categoryyy,Amounttt,Dateee,Remarkkk")] AccountBook model)
        {
            var oldData = _service.GetSingleOrDefault(x => x.Id == model.Id);
            if (oldData != null && ModelState.IsValid)
            {
                _service.Update(model, oldData);
                _service.Save();
                return RedirectToAction("Index");
            }

            ViewData["DDLCategory"] = Categories();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxPost([Bind(Include = "Categoryyy,Amounttt,Dateee,Remarkkk")] AccountBook accountBook)
        {
            _service.Add(accountBook);
            _service.Save();

            return PartialView("_AccountsPartial", _service.GetLists().ToPagedList(0, pageSize));
        }

        private List<SelectListItem> Categories(int val = 0)
        {
            var categories = new List<SelectListItem>();
            categories.Add(new SelectListItem { Text = "請選擇", Value = "" });
            categories.Add(new SelectListItem { Text = "收入", Value = "0", Selected = (val == 0) });
            categories.Add(new SelectListItem { Text = "支出", Value = "1", Selected = (val == 1) });

            return categories;
        }
    }
}