using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyAccounts.Models
{
    [MetadataType(typeof(AccountBookMetaData))]

    public partial class AccountBook
    {
        public class AccountBookMetaData
        {
            [Display(Name = "#")]
            public System.Guid Id { get; set; }
            [Display(Name = "類別")]
            public int Categoryyy { get; set; }
            [Display(Name = "日期")]
            public int Amounttt { get; set; }
            [Display(Name = "金額")]
            public System.DateTime Dateee { get; set; }
            [Display(Name = "備註"), DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Remarkkk { get; set; }
        }
    }
}