using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace MyAccounts.Validation
{
    public class OverDateAttribute : ValidationAttribute, IClientValidatable
    {
        public DateTime MaxDate { get; set; }

        public OverDateAttribute()
        {
            MaxDate = DateTime.Today;
        }

        public OverDateAttribute(string maxDate)
        {
            MaxDate = DateTime.ParseExact(maxDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
        }

        public override bool IsValid(object value)
        {
            if (value == null || !(value is DateTime))
            {
                return true;
            }

            return (DateTime)value <= MaxDate;
        }

        public override string FormatErrorMessage(string name)
        {
            return "超過日期限制";
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "overdate",
            };
            rule.ValidationParameters["max"] = MaxDate.ToString("yyyy/MM/dd");

            yield return rule;
        }
    }
}