namespace GameStoreApplication.ViewModels.Account
{
    using Common;
    using Infrastructure;
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserViewModel
    {
        [Display(Name = "E-Mail")]
        [Required]
        [EmailAddress]
        [MaxLength(ValidationConstants.Account.EmailMaxLength, ErrorMessage = ValidationConstants.InvalidMaxLengthErrorMessage)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Full Title")]
        [MinLength(ValidationConstants.Account.NameMinLength, ErrorMessage = ValidationConstants.InvalidMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.Account.NameMaxLength, ErrorMessage = ValidationConstants.InvalidMaxLengthErrorMessage)]
        public string FullName { get; set; }

        [Password]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
