using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyAccounts.Models;

namespace MyAccounts.Data
{
    public static class DataHelper
    {
        public static List<AccountsModels> Accounts()
        {
            DateTime today = DateTime.Today;
            List<AccountsModels> accounts = new List<AccountsModels>();

            for (int i = 0; i < 3; i++)
            {
                accounts.Add(new AccountsModels()
                {
                    ID = (i + 1),
                    AccountTypes = (i % 2 == 1).ToString(),
                    AccountDate = today.AddDays(-(i + 1) * 15),
                    Amount = new Random(Guid.NewGuid().GetHashCode()).Next(1, 99999),
                    Msg = string.Empty
                });
            }

            return accounts;
        }

        public static List<AccountsModels> GetAccounts()
        {
            List<AccountsModels> accounts = new List<AccountsModels>();

            using (var db = new SkillTreeHomeworkEntities())
            {
                accounts = db.AccountBook.Select(s => new AccountsModels()
                {
                    //ID = s.Id // GUID 無法轉為int？ ID先暫不做設定。
                    AccountTypes = s.Categoryyy.ToString(),
                    AccountDate = s.Dateee,
                    Amount = s.Amounttt,
                    Msg = s.Remarkkk
                }).Take(10).ToList(); // 測試用抓個10筆資料就好
            }

            return accounts;
        }
    }
}