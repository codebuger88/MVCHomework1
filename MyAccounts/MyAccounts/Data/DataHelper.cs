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
    }
}