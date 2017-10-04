namespace FootballBetting.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidUsernameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var valueAsString = value.ToString();

            if (!Regex.IsMatch(valueAsString, @"[a-zA-Z0-9]{4,10}"))
            {
                this.ErrorMessage = "Invalid Username.";
                return false;
            }

            return true;
        }
    }
}
