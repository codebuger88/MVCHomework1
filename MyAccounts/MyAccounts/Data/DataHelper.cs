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
                //先把資料撈回至記憶體後再重新select給定ID，
                //會這樣做是想嘗試當view也不能更動時該如何給流水號
                accounts = db.AccountBook.Take(10).ToList()
                    .Select((s, index) => new AccountsModels()
                    {
                        ID = index + 1,
                        AccountTypes = s.Categoryyy.ToString(),
                        AccountDate = s.Dateee,
                        Amount = s.Amounttt,
                        Msg = s.Remarkkk
                    }).ToList();
            }

            return accounts;
        }
    }
}