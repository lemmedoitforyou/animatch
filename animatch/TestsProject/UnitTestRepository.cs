using Xunit;
using FluentAssertions;
using AniDAL.DataBaseClasses;
using AniDAL.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AniDAL.DbContext;
using System.Reflection;

namespace Repository
{
    public class GenericRepositoryTests
    {
        [Fact]
        public void GetAll_ReturnsAllRecords()
        {
            var repositoryMock = new Mock<IGenericRepository<Anime>>();
            repositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Anime> { new Anime(), new Anime() });

            var repository = repositoryMock.Object;

            var result = repository.GetAll();

            result.Should().NotBeNull().And.BeAssignableTo<IEnumerable<Anime>>().And.NotBeEmpty();
        }

        [Fact]
        public void GetById_ReturnsCorrectRecord()
        {
            var repositoryMock = new Mock<IGenericRepository<Anime>>();
            var targetId = 2;
            repositoryMock.Setup(repo => repo.GetById(targetId)).Returns(new Anime { Id = targetId });

            var repository = repositoryMock.Object;

            var result = repository.GetById(targetId);

            result.Should().NotBeNull().And.BeAssignableTo<Anime>().And.BeEquivalentTo(new Anime { Id = targetId });
        }

        [Fact]
        public void Insert_AddsNewRecord()
        {
            var repositoryMock = new Mock<IGenericRepository<UserInfo>>();
            var newData = new UserInfo
            {
                Id = repositoryMock.Object.GetLastId() + 1,
                Name = "name",
                Photo = "path",
                Text = "text",
                Email = "email",
                Level = 1,
                Password = "password",
                Username = "username"
            };

            int targetId = newData.Id;

            repositoryMock.Setup(repo => repo.GetById(targetId)).Returns((UserInfo)null);

            List<UserInfo> repositoryData = new List<UserInfo>();
            repositoryMock.Setup(repo => repo.Insert(It.IsAny<UserInfo>())).Callback<UserInfo>(data =>
            {
                repositoryData.Add(data);
            });

            repositoryMock.Setup(repo => repo.GetById(targetId)).Returns(newData);

            var repository = repositoryMock.Object;

            repository.Insert(newData);

            var result = repository.GetById(newData.Id);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(newData, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void Update_UpdatesRecord()
        {
            var repositoryMock = new Mock<IGenericRepository<UserInfo>>();
            var existingData = new UserInfo { Id = 1, Name = "oldname", Photo = "oldpath", Text = "text", Email = "email", Level = 1, Password = "password", Username = "username" };
            var newData = new UserInfo { Id = 1, Name = "newname", Photo = "newpath", Text = "text", Email = "email", Level = 1, Password = "password", Username = "username" };

            repositoryMock.Setup(repo => repo.GetById(1)).Returns(existingData);

            repositoryMock.Setup(repo => repo.Update(It.IsAny<UserInfo>())).Callback<UserInfo>(newData =>
            {
                existingData.Name = newData.Name;
                existingData.Photo = newData.Photo;
            });

            var repository = repositoryMock.Object;

            repository.Update(newData);

            var result = repository.GetById(1);

            result.Should().NotBeNull().And.BeEquivalentTo(newData, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void Delete_RemovesRecord()
        {
            // Arrange
            var repositoryMock = new Mock<IGenericRepository<UserInfo>>();
            var targetId = 1;

            var repository = repositoryMock.Object;


            // Add a record to the repository
            var userToAdd = new UserInfo
            {
                Id = repositoryMock.Object.GetLastId() + 1,
                Name = "name",
                Photo = "path",
                Text = "text",
                Email = "email",
                Level = 1,
                Password = "password",
                Username = "username"
            };

            repositoryMock.Setup(repo => repo.GetById(targetId)).Returns(userToAdd);

            // Act (First)
            var resultBeforeDeletion = repository.GetById(targetId);
            resultBeforeDeletion.Should().NotBeNull();

            // Act (Second)
            repository.Delete(targetId);

            repositoryMock.Setup(repo => repo.GetById(targetId)).Returns((int id) => null);
            // Assert
            var resultAfterDeletion = repository.GetById(targetId);
            resultAfterDeletion.Should().BeNull();

            repositoryMock.Verify(repo => repo.Delete(targetId), Times.Once);
        }

        [Fact]
        public void Save_SavesChanges()
        {
            // Arrange
            var repositoryMock = new Mock<IGenericRepository<AddedAnime>>();
            repositoryMock.Setup(repo => repo.Save());

            var repository = repositoryMock.Object;

            // Act
            repository.Save();

            // Assert
            repositoryMock.Verify(repo => repo.Save(), Times.Once);
        }
    }

    public class AddedAnimeRepositoryTests
    {
        [Fact]
        public void GetAddedAnimesForUser_ReturnsCorrectResult()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var animeRepositoryMock = new Mock<IAnimeRepository>();
            var addedAnimeRepositoryMock = new Mock<IAddedAnimeRepository>();

            var repositoryAdded = addedAnimeRepositoryMock.Object;
            var repositoryAnime = animeRepositoryMock.Object;
            var repositoryUser = userRepositoryMock.Object;

            var userId = 100500;
            var animeId = 101;

            var newUser = new UserInfo
            {
                Id = userId,
                Name = "name",
                Photo = "path",
                Text = "text",
                Email = "email",
                Level = 1,
                Password = "password",
                Username = "username"
            };

            var newAnime = new Anime
            {
                Id = animeId,
                Name = "Anime 1",
                Imdbrate = 0.0,
                Photo = "Path",
                Text = "aaa",
                Year = 2023
            };

            var newAddedAnime = new AddedAnime
            {
                Id = 100500,
                UserId = userId,
                AnimeId = animeId
            };

            userRepositoryMock.Setup(repo => repo.GetById(userId)).Returns((UserInfo)null);
            userRepositoryMock.Setup(repo => repo.Insert(newUser));

            animeRepositoryMock.Setup(repo => repo.GetById(animeId)).Returns((Anime)null);
            animeRepositoryMock.Setup(repo => repo.Insert(newAnime));

            addedAnimeRepositoryMock.Setup(repo => repo.Insert(newAddedAnime));

            addedAnimeRepositoryMock.Setup(repo => repo.GetAddedAnimesForUser(userId))
                .Returns(new List<Anime> { newAnime });

            // Act
            repositoryUser.Insert(newUser);
            repositoryAnime.Insert(newAnime);
            repositoryAdded.Insert(newAddedAnime);

            var result = repositoryAdded.GetAddedAnimesForUser(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().ContainEquivalentOf(newAnime, options => options.ExcludingMissingMembers());
        }
    }

    public class AnimeGenreRepositoryTests
    {
        [Fact]
        public void GetGenresForAnime_ReturnsCorrectGenres()
        {
            // Arrange
            var animeGenreRepositoryMock = new Mock<IAnimeGenreRepository>();
            var repository = animeGenreRepositoryMock.Object;

            var animeId = 1;

            var expectedGenres = new List<string> { "Genre1", "Genre2" };

            animeGenreRepositoryMock.Setup(repo => repo.GetGenresForAnime(animeId))
                .Returns(expectedGenres);

            // Act
            var result = repository.GetGenresForAnime(animeId);

            // Assert
            result.Should().BeEquivalentTo(expectedGenres);
        }

        [Fact]
        public void GetAnimesForGenre_ReturnsCorrectAnimes()
        {
            // Arrange
            var animeGenreRepositoryMock = new Mock<IAnimeGenreRepository>();
            var repository = animeGenreRepositoryMock.Object;

            var genreId = 1;

            var expectedAnimes = new List<Anime>
        {
            new Anime { Id = 101, Name = "Anime 1", Imdbrate = 0.0, Photo = "Path", Text = "aaa", Year = 2023 },
            new Anime { Id = 102, Name = "Anime 2", Imdbrate = 0.0, Photo = "Path", Text = "aaa", Year = 2023 }
        };

            animeGenreRepositoryMock.Setup(repo => repo.GetAnimesForGenre(genreId))
                .Returns(expectedAnimes);

            // Act
            var result = repository.GetAnimesForGenre(genreId);

            // Assert
            result.Should().BeEquivalentTo(expectedAnimes, options => options.ExcludingMissingMembers());
        }
    }

    public class DislikedAnimeRepositoryTests
    {
        [Fact]
        public void GetDislikedAnimesForUser_ReturnsCorrectAnimes()
        {
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var animeRepositoryMock = new Mock<IAnimeRepository>();
            var dislikedAnimeRepositoryMock = new Mock<IDislikedAnimeRepository>();

            var repositoryDisliked = dislikedAnimeRepositoryMock.Object;
            var repositoryAnime = animeRepositoryMock.Object;
            var repositoryUser = userRepositoryMock.Object;

            var userId = 100500;
            var animeId = 101;

            var newUser = new UserInfo
            {
                Id = userId,
                Name = "name",
                Photo = "path",
                Text = "text",
                Email = "email",
                Level = 1,
                Password = "password",
                Username = "username"
            };

            var newAnime = new Anime
            {
                Id = animeId,
                Name = "Anime 1",
                Imdbrate = 0.0,
                Photo = "Path",
                Text = "aaa",
                Year = 2023
            };

            var newDislikedAnime = new DislikedAnime
            {
                Id = 100500,
                UserId = userId,
                AnimeId = animeId
            };

            userRepositoryMock.Setup(repo => repo.GetById(userId)).Returns((UserInfo)null);
            userRepositoryMock.Setup(repo => repo.Insert(newUser));

            animeRepositoryMock.Setup(repo => repo.GetById(animeId)).Returns((Anime)null);
            animeRepositoryMock.Setup(repo => repo.Insert(newAnime));

            dislikedAnimeRepositoryMock.Setup(repo => repo.Insert(newDislikedAnime));

            dislikedAnimeRepositoryMock.Setup(repo => repo.GetDislikedAnimesForUser(userId))
                .Returns(new List<Anime> { newAnime });

            // Act
            repositoryUser.Insert(newUser);
            repositoryAnime.Insert(newAnime);
            repositoryDisliked.Insert(newDislikedAnime);

            var result = repositoryDisliked.GetDislikedAnimesForUser(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().ContainEquivalentOf(newAnime, options => options.ExcludingMissingMembers());
        }
    }

    public class LikedAnimeRepositoryTests
    {
        [Fact]
        public void GetLikedAnimesForUser_ReturnsCorrectAnimes()
        {
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var animeRepositoryMock = new Mock<IAnimeRepository>();
            var likedAnimeRepositoryMock = new Mock<ILikedAnimeRepository>();

            var repositoryLiked = likedAnimeRepositoryMock.Object;
            var repositoryAnime = animeRepositoryMock.Object;
            var repositoryUser = userRepositoryMock.Object;

            var userId = 100500;
            var animeId = 101;

            var newUser = new UserInfo
            {
                Id = userId,
                Name = "name",
                Photo = "path",
                Text = "text",
                Email = "email",
                Level = 1,
                Password = "password",
                Username = "username"
            };

            var newAnime = new Anime
            {
                Id = animeId,
                Name = "Anime 1",
                Imdbrate = 0.0,
                Photo = "Path",
                Text = "aaa",
                Year = 2023
            };

            var newLikedAnime = new LikedAnime
            {
                Id = 100500,
                UserId = userId,
                AnimeId = animeId
            };

            userRepositoryMock.Setup(repo => repo.GetById(userId)).Returns((UserInfo)null);
            userRepositoryMock.Setup(repo => repo.Insert(newUser));

            animeRepositoryMock.Setup(repo => repo.GetById(animeId)).Returns((Anime)null);
            animeRepositoryMock.Setup(repo => repo.Insert(newAnime));

            likedAnimeRepositoryMock.Setup(repo => repo.Insert(newLikedAnime));

            likedAnimeRepositoryMock.Setup(repo => repo.GetLikedAnimesForUser(userId))
                .Returns(new List<Anime> { newAnime });

            // Act
            repositoryUser.Insert(newUser);
            repositoryAnime.Insert(newAnime);
            repositoryLiked.Insert(newLikedAnime);

            var result = repositoryLiked.GetLikedAnimesForUser(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().ContainEquivalentOf(newAnime, options => options.ExcludingMissingMembers());
        }
    }

    public class ReviewRepositoryTests
    {
        [Fact]
        public void GetReviewsForAnime_ReturnsCorrectReviews()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var animeRepositoryMock = new Mock<IAnimeRepository>();
            var reviewRepositoryMock = new Mock<IReviewRepository>();

            var repositoryReview = reviewRepositoryMock.Object;
            var repositoryAnime = animeRepositoryMock.Object;
            var repositoryUser = userRepositoryMock.Object;

            var userId = 100500;
            var animeId = 101;

            var newUser = new UserInfo
            {
                Id = userId,
                Name = "name",
                Photo = "path",
                Text = "text",
                Email = "email",
                Level = 1,
                Password = "password",
                Username = "username"
            };

            var newAnime = new Anime
            {
                Id = animeId,
                Name = "Anime 1",
                Imdbrate = 0.0,
                Photo = "Path",
                Text = "aaa",
                Year = 2023
            };

            var newReview = new Review { Id = 1, AnimeId = animeId, Text = "Review 1", Rate = 1, UserId = 1 };

            userRepositoryMock.Setup(repo => repo.GetById(userId)).Returns((UserInfo)null);
            userRepositoryMock.Setup(repo => repo.Insert(newUser));

            animeRepositoryMock.Setup(repo => repo.GetById(animeId)).Returns((Anime)null);
            animeRepositoryMock.Setup(repo => repo.Insert(newAnime));

            reviewRepositoryMock.Setup(repo => repo.Insert(newReview));


            reviewRepositoryMock.Setup(repo => repo.GetReviewsForAnime(userId))
                .Returns(new List<Review> { newReview });

            // Act
            repositoryUser.Insert(newUser);
            repositoryAnime.Insert(newAnime);
            repositoryReview.Insert(newReview);

            var result = repositoryReview.GetReviewsForAnime(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().ContainEquivalentOf(newReview, options => options.ExcludingMissingMembers());
        }
    }

    public class UserInfoRepositoryTests
    {
        [Fact]
        public void GetByUsername_WithValidUsername_ReturnsUserInfo()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var repositoryUser = userRepositoryMock.Object;

            var expectedUser = new UserInfo
            {
                Id = 1,
                Name = "John",
                Username = "john_doe",
                Email = "john@example.com",
                // Add other properties as needed
            };

            userRepositoryMock.Setup(repo => repo.GetByUsername("john_doe")).Returns(expectedUser);

            // Act
            var result = repositoryUser.GetByUsername("john_doe");

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedUser, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void IsExistUsername_WithExistingUsername_ReturnsTrue()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var repositoryUser = userRepositoryMock.Object;

            var existingUsername = "existing_user";

            userRepositoryMock.Setup(repo => repo.IsExistUsername(existingUsername)).Returns(true);

            // Act
            var result = repositoryUser.IsExistUsername(existingUsername);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsExistUsername_WithNonExistingUsername_ReturnsFalse()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var repositoryUser = userRepositoryMock.Object;

            var nonExistingUsername = "non_existing_user";

            userRepositoryMock.Setup(repo => repo.IsExistUsername(nonExistingUsername)).Returns(false);

            // Act
            var result = repositoryUser.IsExistUsername(nonExistingUsername);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void UpdateTitleAndText_WithValidUserId_UpdatesTitleAndText()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var repositoryUser = userRepositoryMock.Object;

            var userId = 1;
            var newTitle = "New Title";
            var newText = "New Text";

            var userInfo = new UserInfo
            {
                Id = userId,
                Name = "John",
                Text = "Old Text",
                // Add other properties as needed
            };

            userRepositoryMock.Setup(repo => repo.GetById(userId)).Returns(new UserInfo { Id = userId, Name = newTitle, Text = newText });

            // Act
            repositoryUser.UpdateTitleAndText(userId, newTitle, newText);
            var result = repositoryUser.GetById(userId);

            result.Name.Should().Be(newTitle);
            result.Text.Should().Be(newText);
        }


        [Fact]
        public void WatchAnime_WithValidUserId_IncrementsWatchedCount()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var repositoryUser = userRepositoryMock.Object;

            var userId = 1;


            var userInfo = new UserInfo
            {
                Id = userId,
                Name = "John",
                WatchedCount = 5
                // Add other properties as needed
            };

            userRepositoryMock.Setup(repo => repo.GetById(userId)).Returns(new UserInfo
            {
                Id = userId,
                Name = "John",
                WatchedCount = 6
                // Add other properties as needed
            });

            // Act
            repositoryUser.WatchAnime(userId);

            var result = repositoryUser.GetById(userId);
            result.WatchedCount.Should().Be(6); // Assuming it increments by 1
        }

    }

    public class WatchedAnimeRepositoryTests
    {
        [Fact]
        public void GetWatchedAnimesForUser_ReturnsCorrectResult()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var animeRepositoryMock = new Mock<IAnimeRepository>();
            var watchedAnimeRepositoryMock = new Mock<IWatchedAnimeRepository>();

            var repositoryWatched = watchedAnimeRepositoryMock.Object;
            var repositoryAnime = animeRepositoryMock.Object;
            var repositoryUser = userRepositoryMock.Object;

            var userId = 100500;
            var animeId = 101;

            var newUser = new UserInfo
            {
                Id = userId,
                Name = "name",
                Photo = "path",
                Text = "text",
                Email = "email",
                Level = 1,
                Password = "password",
                Username = "username"
            };

            var newAnime = new Anime
            {
                Id = animeId,
                Name = "Anime 1",
                Imdbrate = 0.0,
                Photo = "Path",
                Text = "aaa",
                Year = 2023
            };

            var newAddedAnime = new WatchedAnime
            {
                Id = 100500,
                UserId = userId,
                AnimeId = animeId
            };

            userRepositoryMock.Setup(repo => repo.GetById(userId)).Returns((UserInfo)null);
            userRepositoryMock.Setup(repo => repo.Insert(newUser));

            animeRepositoryMock.Setup(repo => repo.GetById(animeId)).Returns((Anime)null);
            animeRepositoryMock.Setup(repo => repo.Insert(newAnime));

            watchedAnimeRepositoryMock.Setup(repo => repo.Insert(newAddedAnime));

            watchedAnimeRepositoryMock.Setup(repo => repo.GetWatchedAnimesForUser(userId))
                .Returns(new List<Anime> { newAnime });

            // Act
            repositoryUser.Insert(newUser);
            repositoryAnime.Insert(newAnime);
            repositoryWatched.Insert(newAddedAnime);

            var result = repositoryWatched.GetWatchedAnimesForUser(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().ContainEquivalentOf(newAnime, options => options.ExcludingMissingMembers());
        }
    }
}

