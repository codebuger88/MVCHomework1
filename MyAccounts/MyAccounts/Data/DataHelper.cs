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
                    AccountTypes = (i % 2 == 1 ? EnumData.AccountTypes.支出 : EnumData.AccountTypes.收入).ToString(),
                    AccountDate = today.AddDays(-new Random().Next(1, 365)),
                    Amount = new Random().Next(1, 99999),
                    Msg = string.Empty
                });
            }

            return accounts;
        }
    }
}