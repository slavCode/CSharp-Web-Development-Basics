namespace BankSystem.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidEmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var valueAsString = value.ToString();

            var pattern = @"^[A-Za-z0-9][A-Za-z0-9._-]+[A-Za-z0-9]@([A-Za-z]+\.)+[A-Za-z]+$";
            if (!Regex.IsMatch(valueAsString, pattern))
            {
                this.ErrorMessage = "Email is not valid";
                return false;
            }

            return true;
        }
    }
}
