namespace SocialNetwork.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidPasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var valueAsString = value.ToString();

            var length = valueAsString.Length;
            if (length < 6)
            {
                this.ErrorMessage = "Password is too short.";
                return false;
            }

            if (length > 50)
            {
                this.ErrorMessage = "Password is too long.";
                return false;
            }

            if (!Regex.IsMatch(valueAsString, @"[A-Z]+"))
            {
                this.ErrorMessage = "Password does not contain uppercase letter.";
                return false;
            }

            if (!Regex.IsMatch(valueAsString, @"[a-z]+"))
            {
                this.ErrorMessage = "Password does not contain lowercase letter.";
                return false;
            }

            if (!Regex.IsMatch(valueAsString, @"\d+"))
            {
                this.ErrorMessage = "Password does not contain a digit.";
                return false;
            }

            if (!Regex.IsMatch(valueAsString, @"[[!@#$%^&*()_+<>?]+"))
            {
                this.ErrorMessage = "Password does not contain a special character.";
                return false;
            }

            return true;
        }
    }
}
