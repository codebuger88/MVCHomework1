using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAccounts.Data
{
    public static class WebHelper
    {
        public static string GetAccountType(string type)
        {
            return type == "1" ? "支出" : "收入";
        }
    }
}