namespace GameStoreApplication.Infrastructure
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class PasswordAttribute : ValidationAttribute
    {
        public PasswordAttribute()
        {
            this.ErrorMessage =
                "Password length must be at least 6 symbols and must contain at least 1 uppercase, 1 lowercase letter and 1 digit";
        }

        public override bool IsValid(object value)
        {
            var password = value as string;
            if (password == null)
            {
                return false;
            }

            var regex = Regex.Match(password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$");
            if (!regex.Success)
            {
                return false;
            }

            return true;
        }
    }
}
