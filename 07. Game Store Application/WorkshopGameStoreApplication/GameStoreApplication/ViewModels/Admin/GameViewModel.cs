namespace GameStoreApplication.ViewModels.Admin
{
    using Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GameViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.Game.TitleMinLength, ErrorMessage = ValidationConstants.InvalidMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.Game.TitleMaxLength, ErrorMessage = ValidationConstants.InvalidMaxLengthErrorMessage)]
        public string Title { get; set; }

        [Display(Name = "Youtube Video URL")]
        [Required]
        [MinLength(ValidationConstants.Game.VideoIdLength, ErrorMessage = ValidationConstants.InvalidVideoLengthErrorMessage)]
        [MaxLength(ValidationConstants.Game.VideoIdLength, ErrorMessage = ValidationConstants.InvalidVideoLengthErrorMessage)]
        public string Trailer { get; set; }

        [Required]
        public string Image { get; set; }

        public double Size { get; set; }

        public decimal Price { get; set; }

        [MinLength(ValidationConstants.Game.DescriptionMinLength, ErrorMessage = ValidationConstants.InvalidMinLengthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
    }
}
