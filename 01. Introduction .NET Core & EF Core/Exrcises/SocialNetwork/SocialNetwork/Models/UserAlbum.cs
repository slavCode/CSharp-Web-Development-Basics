namespace SocialNetwork.Models
{
    using SocialNetwork.Models.Enums;

    public class UserAlbum
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int AlbumId { get; set; }

        public Album Album { get; set; }

        public UserRole UserRole { get; set; }
    }
}
