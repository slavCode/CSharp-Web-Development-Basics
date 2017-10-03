namespace SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Attributes;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [ValidPassword]
        public string Password { get; set; }

        [Required]
        [ValidEmail]
        public string Email { get; set; }

        public byte[] ProfilePicture { get; set; }

        public DateTime RegisterOn { get; set; }

        public DateTime LastTimeLoggedIn { get; set; }
        
        [Range(1, 120)]
        public int Age { get; set; }

        public Boolean IsDeleted { get; set; }

        public ICollection<UserFriend> FriendshipsMade { get; set; } = new List<UserFriend>();

        public ICollection<UserFriend> FriendshipsAccepted { get; set; } = new List<UserFriend>();

        // public ICollection<Album> Albums { get; set; } = new List<Album>(); // Task 3 Create; Task 5 Delete

        public ICollection<UserAlbum> Albums { get; set; } = new List<UserAlbum>();
    }
}
