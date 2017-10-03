namespace SocialNetwork.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property)]
    public class ValidTagAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var valueAsString = value.ToString();

            if (valueAsString.Length > 20)
            {
                this.ErrorMessage = "Tag length is too long.";
                return false;
            }

            if (!Regex.IsMatch(valueAsString, @"^#\w+"))
            {
                this.ErrorMessage = "Invalid Tag.";
                return false;
            }

            return true;
        }
    }
}
