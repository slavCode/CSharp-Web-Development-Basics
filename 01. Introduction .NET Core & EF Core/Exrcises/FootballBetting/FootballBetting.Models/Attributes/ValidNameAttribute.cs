namespace FootballBetting.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidNameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var valueAsString = value.ToString();

            if (!Regex.IsMatch(valueAsString, @"^[A-Z]([a-z]){1,15}"))
            {
                this.ErrorMessage = "Invalid Name.";
                return false;
            }

            return true;
        }
    }
}
