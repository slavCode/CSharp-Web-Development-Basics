namespace FootballBetting.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidFullNameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var valueAsString = value.ToString();

            if (!Regex.IsMatch(valueAsString, @"^[A-Z]([a-z]){1,10} [A-Z]([a-z]){1,10}"))
            {
                this.ErrorMessage = "Invalid Full Name.";
                return false;
            }

            return true;
        }
    }
}
