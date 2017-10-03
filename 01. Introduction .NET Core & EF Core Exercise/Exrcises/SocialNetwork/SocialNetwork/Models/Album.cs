using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SocialNetwork.Models
{
    using SocialNetwork.Models.Enums;

    public class Album
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public BackgroundColor BackgroundColor { get; set; }

        public Boolean IsPublic { get; set; }

        // public int UserId { get; set; } // Task 3 Create; Task 5 Delete;
        // 
        // public User User { get; set; } // Task 3 Create; Task 5 Delete;

        public ICollection<PictureAlbum> Pictures { get; set; } = new List<PictureAlbum>(); 

        public ICollection<AlbumTag> Tags { get; set; } = new List<AlbumTag>();

        public ICollection<UserAlbum> Users { get; set; } = new List<UserAlbum>(); 
    }
}
