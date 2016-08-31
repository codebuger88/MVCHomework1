using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using MyAccounts.CustomResults;
using MyAccounts.Data;
using MyAccounts.Repositories;
using MyAccounts.Services;

namespace MyAccounts.Controllers
{
    public class FeedController : Controller
    {
        private readonly AccountsService _service;

        public FeedController()
        {
            var unitOfWork = new EFUnitOfWork();
            _service = new AccountsService(unitOfWork);
        }

        // GET: Orders
        public ActionResult Index()
        {
            var feed = this.GetFeedData();
            return new RssResult(feed);
        }

        private SyndicationFeed GetFeedData()
        {
            var feed = new SyndicationFeed(
                "我的記帳本",
                "記帳本RSS！",
                new Uri(Url.Action("Index", "Feed", null, "http")));

            var items = new List<SyndicationItem>();

            var datas = _service.GetLists(null, null).Take(10);

            foreach (var data in datas)
            {
                var item = new SyndicationItem(
                    data.AccountDate.ToString("yyyy/MM/dd"),
                    $"{WebHelper.GetAccountType(data.AccountTypes)} ${data.Amount}",
                    new Uri(Url.Action("Detail", "Accounts", new { id = data.ID }, "http")),
                    "ID",
                    DateTime.Now);

                items.Add(item);
            }

            feed.Items = items;
            return feed;
        }
    }
}