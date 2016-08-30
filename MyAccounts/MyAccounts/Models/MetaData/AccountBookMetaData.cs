using System.ComponentModel.DataAnnotations;
using MyAccounts.Validation;

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
            [Display(Name = "金額"), Range(1, int.MaxValue, ErrorMessage = "請輸入正整數")]
            public int Amounttt { get; set; }
            [Required]
            [Display(Name = "日期")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            //[DataType(DataType.Date)]
            [OverDate()]
            public System.DateTime Dateee { get; set; }
            [Display(Name = "備註")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.MultilineText)]
            [StringLength(100, ErrorMessage = "最多輸入100個字元")]
            public string Remarkkk { get; set; }
        }
    }
}