namespace FootballBetting.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidPositionIdAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var valueAsString = value.ToString();
            if (!Regex.IsMatch(valueAsString, @"[A-Z]{2}"))
            {
                this.ErrorMessage = "Invalid Id!";

                return false;
            }

            return true;
        }
    }
}
