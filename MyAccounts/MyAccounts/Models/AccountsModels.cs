﻿using System;
using System.ComponentModel;

namespace MyAccounts.Models
{
    public class AccountsModels
    {
        [DisplayName("#")]
        public int ID { get; set; }

        [DisplayName("類別")]
        public string AccountTypes { get; set; }

        [DisplayName("日期")]
        public DateTime AccountDate { get; set; }

        [DisplayName("金額")]
        public int Amount { get; set; }

        [DisplayName("備註")]
        public string Msg { get; set; }

        public Guid GId { get; set; }

    }
}