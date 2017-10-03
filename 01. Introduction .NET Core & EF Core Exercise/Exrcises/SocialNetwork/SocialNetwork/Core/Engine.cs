namespace SocialNetwork.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using SocialNetwork.Data;
    using SocialNetwork.Models;
    using SocialNetwork.Models.Enums;

    public class Engine
    {
        private const int numberOfUsers = 50;
        private const int numberOfPhotos = 400;
        private const int numberOfAlbums = 100; // Task 3
        private const string upperLetters = "ABCDEFGIJKLMNOPQRSTWXYZ";
        private const string lowerLetters = "abcdefghijklmnopqrstuvwxyz";
        private const string digits = "0123456789";
        private const string specialCharacters = "[!@#$%^&*()_+<>";
        private static Random random = new Random();

        public void Run()
        {
            // InitializeDatabase();
            // SeedUsers();
            // SeedFriendships();
            // SeedPictures();
            // SeedAlbums();
            // SeedTags();
            // SeedAlbumTags();
            // SeedAlbumsInUsers();
            // AddUserRole();

            Console.WriteLine("Seeding done! Running queries...");

            // Queries
            using (var db = new SocialNetworkDbContext())
            {
                // PrintUsersAndFriendsCount(db); // Task 2
                // PrintActiveUsersWithMoreThenFiveFriends(db); // Task 2
                // PrintAllAlbumsWithOwnerAndPicturesCount(db); // Task 3
                // PrintPicturesThatAreIncludedInMoreThen2Albums(db); // Task 3
                // PrintAlbumsOfUser(db); // Task 3
                // PrintAlbumsWithAGivenTag(db); // Task 4
                // PrintSharedAlbumsUsers(db); // Task 5
                // PrintAlbumsSharedWithMoreThen2People(db); // Task 5
                // PrintSharedWithUserWithAGivenUser(db); // Task 5
                // PrintAlbumsWithTheirUsers(db); // Task 6
                // PrintAlbumsByAGivenUser(db); // Task 6
                // PrintUsersMoreThen1Album(db); // Task 6
            }
        }

        private void InitializeDatabase()
        {
            Console.WriteLine("Initializing database...");

            using (var db = new SocialNetworkDbContext())
            {
                db.Database.EnsureDeleted();

                db.Database.Migrate();

                Console.WriteLine("Database Ready.");
            }
        }

        private bool RandomBooleanGenerator(int truePercentage)
        {
            bool result = true;
            int prob = random.Next(100);
            if (prob > truePercentage) result = false;

            return result;
        }

        private static string RandomStringGenerator(string chars, int stringLength, Random random)
        {

            char[] stringChars = new char[stringLength];
            for (int i = 0; i < stringLength; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        private static DateTime RandomDay(Random random, DateTime startDate, DateTime endDate)
        {
            int range = (endDate - startDate).Days;

            return startDate.AddDays(random.Next(range));
        }

        private void SeedUsers()
        {
            using (var db = new SocialNetworkDbContext())
            {
                Console.WriteLine("Adding Users...");

                var users = db.Users.ToList();

                for (int i = 0; i < numberOfUsers; i++)
                {
                    var registerOn = RandomDay(random, new DateTime(2005, 1, 1), DateTime.Now);
                    var user = new User
                    {
                        Age = random.Next(12, 60),
                        Email = $"{RandomStringGenerator(lowerLetters, random.Next(3, 10), random)}" +
                                $"@{RandomStringGenerator(lowerLetters, random.Next(3, 6), random)}" +
                                $".{RandomStringGenerator(lowerLetters, random.Next(2, 4), random)}",
                        IsDeleted = RandomBooleanGenerator(20),
                        Password = $"{RandomStringGenerator(lowerLetters, random.Next(3, 5), random)}" +
                                   $"{RandomStringGenerator(upperLetters, random.Next(1, 2), random)}" +
                                   $"{RandomStringGenerator(specialCharacters, 1, random)}" +
                                   $"{RandomStringGenerator(digits, random.Next(1, 3), random)}",
                        RegisterOn = registerOn,
                        LastTimeLoggedIn = RandomDay(random, registerOn, DateTime.Now),
                        Username = $"User {i}",
                        ProfilePicture = new byte[] { 1, 0, 1, 1, 0 }

                    };

                    users.Add(user);
                    db.Users.Add(user);
                }

                db.SaveChanges();
                Console.WriteLine($"{numberOfUsers} users added to the database.");
            }
        }

        private void SeedFriendships()
        {
            using (var db = new SocialNetworkDbContext())
            {
                Console.Write("Adding friends.");

                var users = db.Users.ToList();

                int friendshipsCounter = 0;
                for (int i = 0; i < numberOfUsers; i++)
                {
                    int numberOfFriends = random.Next(15, numberOfUsers);
                    var user = users[i];
                    for (int j = 0; j < numberOfFriends; j++)
                    {

                        var friend = users[random.Next(0, numberOfFriends)];
                        var friendIds = users.Select(u => u.Id).ToList();
                        friendIds.Remove(user.Id);
                        if (user.FriendshipsMade.All(fm => fm.FriendId != friend.Id)
                            && user.FriendshipsAccepted.All(fa => fa.FriendId != user.Id))
                        {
                            var friendshipsMade = new UserFriend
                            {
                                UserId = user.Id,
                                FriendId = friend.Id
                            };

                            var friendshipsAccepted = new UserFriend
                            {
                                UserId = friend.Id,
                                FriendId = user.Id
                            };

                            try
                            {
                                user.FriendshipsAccepted.Add(friendshipsAccepted);
                                user.FriendshipsMade.Add(friendshipsMade);

                                friendshipsCounter++;
                                db.SaveChanges();

                            }
                            catch (Exception)
                            {
                            }
                        }
                    }

                    if (i % 15 == 0)
                    {
                        Console.Write(".");
                    }
                }

                Console.WriteLine();
                Console.WriteLine($"Total of {friendshipsCounter} friendships has been made.");
            }
        }

        private void PrintUsersAndFriendsCount(SocialNetworkDbContext db)
        {
            var result = db
                .Users
                .Select(u => new
                {
                    u.Username,
                    u.IsDeleted,
                    Friends = u.FriendshipsMade.Count + u.FriendshipsAccepted.Count
                })
                .OrderByDescending(u => u.Friends)
                .ThenBy(u => u.Username)
                .ToList();

            foreach (var user in result)
            {
                string status;

                if (user.IsDeleted) status = "Inactive";
                else status = "Active";

                Console.WriteLine($"Name: {user.Username}" +
                                  $"{Environment.NewLine}Number of friends: {user.Friends}" +
                                  $"{Environment.NewLine}Status: {status}");
            }
        }

        private void PrintActiveUsersWithMoreThenFiveFriends(SocialNetworkDbContext db)
        {
            var result = db
                .Users
                .Where(u => u.IsDeleted == false)
                .Where(u => u.FriendshipsMade.Count + u.FriendshipsAccepted.Count > 5)
                .Select(u => new
                {
                    Name = u.Username,
                    Friends = u.FriendshipsMade.Count + u.FriendshipsAccepted.Count,
                    u.RegisterOn,
                    ActivePeriod = u.LastTimeLoggedIn.Subtract(u.RegisterOn).Days
                })
                .OrderBy(u => u.RegisterOn)
                .ThenByDescending(u => u.Friends)
                .ToList();

            foreach (var user in result)
            {
                Console.WriteLine($"Name: {user.Name}" +
                                  $"{Environment.NewLine}Number of friends: {user.Friends}" +
                                  $"{Environment.NewLine}Active period: {user.ActivePeriod} days");
            }
        }

        private void SeedPictures()
        {
            Console.WriteLine("Adding Pictures...");
            using (var db = new SocialNetworkDbContext())
            {
                for (int i = 0; i < numberOfPhotos; i++)
                {
                    var picture = new Picture
                    {
                        Title = RandomStringGenerator(lowerLetters, random.Next(3, 10), random),
                        Caption = $"Caption {i} {RandomStringGenerator(lowerLetters, random.Next(3, 6), random)}",
                        Path = $"C:\\Users\\{RandomStringGenerator(lowerLetters, random.Next(3, 6), random)}" +
                               $"\\{RandomStringGenerator(lowerLetters, random.Next(3, 6), random)}" +
                               $"\\{RandomStringGenerator(lowerLetters, random.Next(3, 6), random)}",

                    };

                    db.Pictures.Add(picture);
                }

                db.SaveChanges();
            }
        }

        //For Task 3 - Seed

        //private void SeedAlbums()
        //{
        //    Console.WriteLine("Seeding Albums...");
        //    using (var db = new SocialNetworkDbContext())
        //    {
        //        var pictures = db.Pictures.ToList();

        //        for (int i = 0; i < numberOfAlbums; i++)
        //        {
        //            var users = db.Users.ToList();
        //            var user = users[random.Next(0, users.Count)];

        //            var album = new Album
        //            {
        //                BackgroundColor = (BackgroundColor)random.Next(0, 7),
        //                IsPublic = RandomBooleanGenerator(50),
        //                Name = $"Album {RandomStringGenerator(lowerLetters, random.Next(3, 6), random)}",
        //                UserId = user.Id
        //            };

        //            var numberOfPictures = random.Next(3, 20);
        //            for (int j = 0; j < numberOfPictures; j++)
        //            {
        //                var pictureId = pictures[random.Next(0, pictures.Count)].Id;

        //                if (album.Pictures.All(p => p.PictureId != pictureId))
        //                {
        //                    var picturesInAlbum = new PictureAlbum
        //                    {
        //                        PictureId = pictureId
        //                    };

        //                    album.Pictures.Add(picturesInAlbum);
        //                }

        //                db.SaveChanges();
        //            }

        //            db.Albums.Add(album);
        //            db.SaveChanges();
        //        }
        //    }
        //}

        // For Task 4 - Queries

        //private void PrintAllAlbumsWithOwnerAndPicturesCount(SocialNetworkDbContext db)
        //{
        //    var result = db
        //        .Albums
        //        .Select(a => new
        //        {
        //            a.User.Username,
        //            a.Pictures.Count,
        //            a.Name
        //        })
        //        .OrderByDescending(a => a.Count)
        //        .ThenBy(a => a.Username)
        //        .ToList();

        //    foreach (var album in result)
        //    {
        //        Console.WriteLine($"Album Title: {album.Name}" +
        //                          $"{Environment.NewLine}Owner: {album.Username}" +
        //                          $"{Environment.NewLine}Total of {album.Count} pictures");
        //    }
        //}

        //For Task 3 - Queries

        //private void PrintPicturesThatAreIncludedInMoreThen2Albums(SocialNetworkDbContext db)
        //{
        //    var result = db
        //        .Pictures
        //        .Where(p => p.Albums.Count > 2)
        //        .Select(p => new
        //        {
        //            p.Title,
        //            AlbumNames = p.Albums.Select(a => a.Album.Name),
        //            OwnerNames = p.Albums.Select(a => a.Album.User.Username)
        //        })
        //        .OrderByDescending(p => p.AlbumNames.Count())
        //        .ThenBy(p => p.Title)
        //        .ToList();

        //    foreach (var picture in result)
        //    {
        //        Console.WriteLine($"Picture Title: {picture.Title}");
        //        Console.WriteLine($"--Picture Albums: {string.Join(", ", picture.AlbumNames)}");
        //        Console.WriteLine($"--Album Owners: {string.Join(", ", picture.OwnerNames)}");
        //    }
        //}

        //For Task 3 - Queries

        //private void PrintAlbumsOfUser(SocialNetworkDbContext db)
        //{
        //    Console.Write("Enter User ID: ");
        //    var pictures = db.Pictures.ToList();
        //    var userId = int.Parse(Console.ReadLine());
        //    var username = db.Users.ToList()[userId].Username;
        //    var result = db
        //        .Albums
        //        .Where(a => a.UserId == userId)
        //        .Select(a => new
        //        {
        //            a.Name,
        //            a.Pictures,
        //            a.IsPublic
        //        })
        //        .OrderBy(a => a.Name)
        //        .ToList();

        //    Console.WriteLine(username);
        //    foreach (var album in result)
        //    {
        //        Console.WriteLine($"-Album: {album.Name}");
        //        if (album.IsPublic)
        //        {
        //            Console.WriteLine("-Pictures:");

        //            foreach (var picture in album.Pictures)
        //            {
        //                Console.WriteLine($"--Title: {pictures[picture.PictureId].Title}");
        //                Console.WriteLine($"--Path: {pictures[picture.PictureId].Path}");
        //                Console.WriteLine();
        //            }
        //        }

        //        else
        //        {
        //            Console.WriteLine("Private content!");
        //        }
        //    }
        //}

        private void SeedTags()
        {
            Console.WriteLine("Seeding Tags...");

            using (var db = new SocialNetworkDbContext())
            {
                Console.Write("Add Tag: ");

                var text = Console.ReadLine();
                while (text != "end")
                {
                    var tagName = TagTransformer.Transformer(text);
                    var tag = new Tag { Name = tagName };

                    db.Tags.Add(tag);
                    db.SaveChanges();

                    Console.WriteLine($"{tagName} was added to database.");
                    Console.WriteLine("Type \"end\" if you want to exit");
                    Console.Write("Add Tag: ");
                    text = Console.ReadLine();
                }
            }
        }

        private void SeedAlbumTags()
        {
            Console.WriteLine("Add tags to albums...");
            using (var db = new SocialNetworkDbContext())
            {
                var tags = db.Tags.ToList();
                var albums = db.Albums.ToList();

                for (int i = 0; i < tags.Count; i++)
                {
                    var tag = tags[i];

                    var albumsCount = random.Next(3, 10);
                    for (int j = 0; j < albumsCount; j++)
                    {
                        var album = albums[random.Next(0, albums.Count)];
                        if (tag.Albums.All(a => a.AlbumId != album.Id))
                        {
                            var albumInTag = new AlbumTag { AlbumId = album.Id };

                            tag.Albums.Add(albumInTag);
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        // For Task 4 - Queries

        //private void PrintAlbumsWithAGivenTag(SocialNetworkDbContext db)
        //{
        //    var tagText = Console.ReadLine();

        //    var result = db
        //        .Albums
        //        .Select(a => new
        //        {
        //            a.Name,
        //            a.User.Username,
        //            a.Tags
        //        })
        //        .Where(a => a.Tags.All(t => t.Tag.Name == tagText))
        //        .OrderByDescending(a => a.Tags.Count)
        //        .ThenBy(a => a.Name)
        //        .ToList();

        //    foreach (var album in result)
        //    {
        //        Console.WriteLine($"Album: {album.Name}");
        //        Console.WriteLine($"--Owner: {album.Username}");
        //    }
        //}

        private void SeedAlbumsInUsers()
        {
            Console.WriteLine("Adding Albums to User...");
            using (var db = new SocialNetworkDbContext())
            {
                var users = db.Users.ToList();
                var albums = db.Albums.ToList();

                for (int i = 0; i < users.Count; i++)
                {
                    var user = users[i];

                    var albumsInUserCount = random.Next(5, 15);
                    for (int j = 0; j < albumsInUserCount; j++)
                    {
                        var album = albums[random.Next(0, albums.Count)];
                        if (user.Albums.All(a => a.AlbumId != album.Id))
                        {
                            var albumInUser = new UserAlbum { AlbumId = album.Id };
                            user.Albums.Add(albumInUser);
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        private void PrintSharedAlbumsUsers(SocialNetworkDbContext db)
        {
            var result = db
                .Users
                .Select(u => new
                {
                    u.Username,
                    Friends = u.FriendshipsMade.Select(f => f.Friend.Username),
                    SharedAlbums = u.Albums.Select(a => new
                    {
                        a.Album.Name,
                        SharedWithUsers = a.Album
                            .Users
                            .Where(au => au.User.Username != u.Username)
                            .Select(au => au.User.Username)
                    })
                })
                .OrderBy(u => u.Username)
                .ToList();

            foreach (var user in result)
            {
                Console.WriteLine($"{user.Username}");
                Console.WriteLine("Friends:");

                foreach (var friend in user.Friends)
                {
                    Console.WriteLine($"--{friend}");
                }

                Console.WriteLine("Albums:");
                foreach (var album in user.SharedAlbums)
                {
                    Console.WriteLine($"--Name: {album.Name}");

                    Console.WriteLine("--Shared with Users:");
                    foreach (var withUser in album.SharedWithUsers)
                    {
                        Console.WriteLine($"----{withUser}");
                    }
                }
            }
        }

        private void PrintAlbumsSharedWithMoreThen2People(SocialNetworkDbContext db)
        {
            var result = db
                .Albums
                .Where(a => a.Users.Count > 2)
                .Select(a => new
                {
                    a.Name,
                    a.Users.Count,
                    a.IsPublic
                })
                .OrderByDescending(a => a.Count)
                .ThenBy(a => a.Name)
                .ToList();

            foreach (var album in result)
            {
                Console.WriteLine("Album:");
                Console.WriteLine($"--Name: {album.Name}");
                Console.WriteLine($"--Number of people: {album.Count}");
                Console.WriteLine($"--Is public: : {album.IsPublic}");
            }
        }

        private void PrintSharedWithUserWithAGivenUser(SocialNetworkDbContext db)
        {
            var username = Console.ReadLine();

            var result = db
                .Albums
                .Where(a => a.Users.Any(u => u.User.Username == username))
                .Select(a => new
                {
                    a.Name,
                    Pictures = a.Pictures.Count,
                })
                .OrderByDescending(a => a.Pictures)
                .ThenBy(a => a.Name)
                .ToList();

            foreach (var album in result)
            {
                Console.WriteLine("Album:");
                Console.WriteLine($"--Name: {album.Name}");
                Console.WriteLine($"--Pictures Count: {album.Pictures}");
            }
        }

        private void AddUserRole()
        {
            Console.WriteLine("Adding user role...");
            using (var db = new SocialNetworkDbContext())
            {
                var userAlbums = db.UserAlbum.ToList();
                var albumsIdsWithOwners = new List<int>();
                for (int i = 0; i < userAlbums.Count; i++)
                {
                    var userAlbum = userAlbums[i];

                    if (!albumsIdsWithOwners.Contains(userAlbum.AlbumId))
                    {
                        userAlbum.UserRole = UserRole.Owner;
                        albumsIdsWithOwners.Add(userAlbum.AlbumId);
                        db.SaveChanges();
                    }

                    else
                    {
                        userAlbum.UserRole = UserRole.Viewer;
                        db.SaveChanges();
                    }
                }
            }
        }

        private void PrintAlbumsWithTheirUsers(SocialNetworkDbContext db)
        {
            var result = db
                .Albums
                .Select(a => new
                {
                    a.Name,
                    Users = a.Users.Select(u => new
                    {
                        Name = u.User.Username,
                        Role = u.UserRole
                    }),
                    Owners = a.Users.Where(u => u.UserRole == UserRole.Owner).Select(o => o.User).Count(),
                    Viewers = a.Users.Count(u => u.UserRole == UserRole.Viewer),
                })
                .OrderBy(a => a.Owners)
                .ThenByDescending(a => a.Viewers)
                .ToList();

            foreach (var album in result)
            {
                Console.WriteLine($"Album: {album.Name}");
                Console.WriteLine($"Users:");

                foreach (var user in album.Users)
                {
                    Console.WriteLine($"--Name: {user.Name}");
                    Console.WriteLine($"--Role: {user.Role}");
                }
            }
        }

        private void PrintAlbumsByAGivenUser(SocialNetworkDbContext db)
        {
            var username = Console.ReadLine();
            var userId = db
                .Users
                .FirstOrDefault(u => u.Username == username)
                .Id;

            var result = db
                .Users
                .Where(u => u.Id == userId)
                .Select(u => new
                {
                    OwnerCount = u.Albums.Count(a => a.UserRole == UserRole.Owner),
                    ViewerCount = u.Albums.Count(a => a.UserRole == UserRole.Viewer)
                })
                .FirstOrDefault();

            Console.WriteLine($"{username} is owner of {result.OwnerCount} albums.");
            Console.WriteLine($"{username} is viewer in {result.ViewerCount} albums.");
        }

        private void PrintUsersMoreThen1Album(SocialNetworkDbContext db)
        {
            var result = db
                .Users
                .Where(u => u.Albums.Count(a => a.UserRole == UserRole.Viewer) > 0)
                .Select(u => new
                {
                    u.Username,
                    PublicAlbums = u.Albums
                    .Where(a => a.UserRole == UserRole.Viewer)
                    .Count(a => a.Album.IsPublic)
                })
                .OrderByDescending(u => u.PublicAlbums)
                .ToList();

            foreach (var user in result)
            {
                Console.WriteLine($"{user.Username} Public albums: {user.PublicAlbums}");
            }
        }
    }
}
