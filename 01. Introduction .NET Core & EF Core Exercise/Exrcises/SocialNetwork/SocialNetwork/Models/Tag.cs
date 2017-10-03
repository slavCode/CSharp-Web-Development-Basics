namespace SocialNetwork.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using SocialNetwork.Attributes;

    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [ValidTag]
        public string Name { get; set; }

        public ICollection<AlbumTag> Albums { get; set; } = new List<AlbumTag>();
    }
}
