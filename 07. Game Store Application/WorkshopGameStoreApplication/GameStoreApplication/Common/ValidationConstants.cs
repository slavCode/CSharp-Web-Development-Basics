namespace GameStoreApplication.Common
{
    public class ValidationConstants
    {
        public const string InvalidMinLengthErrorMessage = "The field {0} must be with minimum length of {1} symbols.";
        public const string InvalidMaxLengthErrorMessage = "The field {0} must be with maximum length of {1} symbols.";
        public const string InvalidVideoLengthErrorMessage = "{0} must be exactly {1} symbols.";


        public class Account
        {
            public const int EmailMaxLength = 30;
            public const int NameMinLength = 3;
            public const int NameMaxLength = 30;
            public const string Password = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$";
        }

        public class Game
        {
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 100;
            public const int VideoIdLength = 11;
            public const int DescriptionMinLength = 20;
        }
    }
}
