using System.Collections.Generic;
using System.Linq;
using AniBLL.Models;
using AniDAL.Repositories;
using FluentAssertions;
using Moq;
using Xunit;
using AniBLL.Services;
using AniDAL.DataBaseClasses;

namespace Service
{
    public class AddedAnimeServiceTests
    {
        [Fact]
        public void GetAddedAnimesForUser_ReturnsCorrectAnimes()
        {
            // Arrange
            var addedAnimeRepositoryMock = new Mock<IAddedAnimeRepository>();
            var service = new AddedAnimeService(addedAnimeRepositoryMock.Object);

            var userId = 1;

            var expectedAnimes = new List<AniDAL.DataBaseClasses.Anime>
        {
            new AniDAL.DataBaseClasses.Anime { Id = 101, Name = "Anime 1", Imdbrate = 0.0, Photo = "Path", Text = "aaa", Year = 2023 },
            new AniDAL.DataBaseClasses.Anime { Id = 102, Name = "Anime 2", Imdbrate = 0.0, Photo = "Path", Text = "aaa", Year = 2023 }
        };

            addedAnimeRepositoryMock.Setup(repo => repo.GetAddedAnimesForUser(userId))
                .Returns(expectedAnimes);

            // Act
            var result = service.GetAddedAnimesForUser(userId);

            // Assert
            var expectedAnimeModels = expectedAnimes
                .Select(anime => new AnimeModel
                {
                    Id = anime.Id,
                    Name = anime.Name,
                    Text = anime.Text,
                    Imdbrate = anime.Imdbrate,
                    Photo = anime.Photo,
                    Year = anime.Year
                })
                .ToList();

            result.Should().BeEquivalentTo(expectedAnimeModels, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void Add_CallsRepositoryInsert()
        {
            // Arrange
            var addedAnimeRepositoryMock = new Mock<IAddedAnimeRepository>();
            var service = new AddedAnimeService(addedAnimeRepositoryMock.Object);

            var addedAnimeModel = new AddedAnimeModel
            {
                Id = 1,
                UserId = 1001,
                AnimeId = 101
                // Add other properties as needed
            };

            // Act
            service.Insert(addedAnimeModel);

            // Assert
            addedAnimeRepositoryMock.Verify(repo => repo.Insert(addedAnimeModel), Times.Once);
        }

        [Fact]
        public void Delete_CallsRepositoryDelete()
        {
            // Arrange
            var addedAnimeRepositoryMock = new Mock<IAddedAnimeRepository>();
            var service = new AddedAnimeService(addedAnimeRepositoryMock.Object);

            var animeIdToDelete = 101;

            // Act
            service.Delete(animeIdToDelete);

            // Assert
            addedAnimeRepositoryMock.Verify(repo => repo.Delete(animeIdToDelete), Times.Once);
        }
    }

    public class AnimeGenreServiceTests
    {
        [Fact]
        public void GetGenresForAnime_ReturnsCorrectGenres()
        {
            // Arrange
            var animeGenreRepositoryMock = new Mock<IAnimeGenreRepository>();
            var service = new AnimeGenreService(animeGenreRepositoryMock.Object);

            var animeId = 1;

            var expectedGenres = new List<string> { "Genre1", "Genre2" };

            animeGenreRepositoryMock.Setup(repo => repo.GetGenresForAnime(animeId))
                .Returns(expectedGenres);

            // Act
            var result = service.GetGenresForAnime(animeId);

            // Assert
            result.Should().BeEquivalentTo(expectedGenres);
        }

        [Fact]
        public void GetAnimesForGenre_ReturnsCorrectAnimes()
        {
            // Arrange
            var animeGenreRepositoryMock = new Mock<IAnimeGenreRepository>();
            var service = new AnimeGenreService(animeGenreRepositoryMock.Object);

            var genreId = 1;


            var expectedAnimes = new List<Anime>
        {
            new Anime { Id = 101, Name = "Anime 1", Imdbrate = 0.0, Photo = "Path", Text = "aaa", Year = 2023 },
            new Anime { Id = 102, Name = "Anime 2", Imdbrate = 0.0, Photo = "Path", Text = "aaa", Year = 2023 }
        };

            animeGenreRepositoryMock.Setup(repo => repo.GetAnimesForGenre(genreId))
                .Returns(expectedAnimes);

            // Act
            var result = service.GetAnimesForGenre(genreId);

            // Assert
            var expectedAnimeModels = expectedAnimes
                .Select(anime => new AnimeModel
                {
                    Id = anime.Id,
                    Name = anime.Name,
                    Text = anime.Text,
                    Imdbrate = anime.Imdbrate,
                    Photo = anime.Photo,
                    Year = anime.Year
                })
                .ToList();

            result.Should().BeEquivalentTo(expectedAnimeModels, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void Insert_CallsRepositoryInsert()
        {
            // Arrange
            var animeGenreRepositoryMock = new Mock<IAnimeGenreRepository>();
            var service = new AnimeGenreService(animeGenreRepositoryMock.Object);

            var animeGenreModel = new AnimeGenreModel
            {
                AnimeId = 101,
                GenreId = 1
                // Add other properties as needed
            };

            // Act
            service.Insert(animeGenreModel);

            // Assert
            animeGenreRepositoryMock.Verify(repo => repo.Insert(animeGenreModel), Times.Once);
        }

        [Fact]
        public void Delete_CallsRepositoryDelete()
        {
            // Arrange
            var animeGenreRepositoryMock = new Mock<IAnimeGenreRepository>();
            var service = new AnimeGenreService(animeGenreRepositoryMock.Object);

            var animeGenreIdToDelete = 1;

            // Act
            service.Delete(animeGenreIdToDelete);

            // Assert
            animeGenreRepositoryMock.Verify(repo => repo.Delete(animeGenreIdToDelete), Times.Once);
        }
    }

    public class AnimeServiceTests
    {
        [Fact]
        public void GetAll_ReturnsAllAnimeModels()
        {
            // Arrange
            var animeRepositoryMock = new Mock<IAnimeRepository>();
            var service = new AnimeService(animeRepositoryMock.Object);

            var expectedAnimeList = new List<Anime>
        {
            new Anime { Id = 1, Name = "Anime 1", Imdbrate = 8.5, Photo = "Path1", Text = "Description 1", Year = 2022 },
            new Anime { Id = 2, Name = "Anime 2", Imdbrate = 7.8, Photo = "Path2", Text = "Description 2", Year = 2021 }
        };

            animeRepositoryMock.Setup(repo => repo.GetAll()).Returns(expectedAnimeList);

            // Act
            var result = service.GetAll();

            // Assert
            var expectedAnimeModels = expectedAnimeList
                .Select(anime => new AnimeModel
                {
                    Id = anime.Id,
                    Name = anime.Name,
                    Text = anime.Text,
                    Imdbrate = anime.Imdbrate,
                    Photo = anime.Photo,
                    Year = anime.Year
                })
                .ToList();

            result.Should().BeEquivalentTo(expectedAnimeModels, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void GetById_ReturnsCorrectAnimeModel()
        {
            // Arrange
            var animeRepositoryMock = new Mock<IAnimeRepository>();
            var service = new AnimeService(animeRepositoryMock.Object);

            var targetAnimeId = 1;
            var expectedAnime = new Anime
            {
                Id = targetAnimeId,
                Name = "Anime 1",
                Imdbrate = 8.5,
                Photo = "Path1",
                Text = "Description 1",
                Year = 2022
            };


            animeRepositoryMock.Setup(repo => repo.GetById(targetAnimeId)).Returns(expectedAnime);

            // Act
            var result = service.GetById(targetAnimeId);

            // Assert
            var expectedAnimeModel = new AnimeModel
            {
                Id = expectedAnime.Id,
                Name = expectedAnime.Name,
                Text = expectedAnime.Text,
                Imdbrate = expectedAnime.Imdbrate,
                Photo = expectedAnime.Photo,
                Year = expectedAnime.Year
            };

            result.Should().BeEquivalentTo(expectedAnimeModel, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void GetById_WithNonExistingAnime_ReturnsNull()
        {
            // Arrange
            var animeRepositoryMock = new Mock<IAnimeRepository>();
            var service = new AnimeService(animeRepositoryMock.Object);

            var nonExistingAnimeId = 100;

            animeRepositoryMock.Setup(repo => repo.GetById(nonExistingAnimeId)).Returns((Anime)null);

            // Act
            var result = service.GetById(nonExistingAnimeId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void Insert_CallsRepositoryInsert()
        {
            // Arrange
            var animeRepositoryMock = new Mock<IAnimeRepository>();
            var service = new AnimeService(animeRepositoryMock.Object);

            var animeModelToAdd = new AnimeModel
            {
                Name = "New Anime",
                Imdbrate = 9.0,
                Photo = "NewPath",
                Text = "New Description",
                Year = 2023
            };

            // Act
            service.Insert(animeModelToAdd);

            // Assert
            animeRepositoryMock.Verify(repo => repo.Insert(animeModelToAdd), Times.Once);
        }

        [Fact]
        public void Update_CallsRepositoryUpdate()
        {
            // Arrange
            var animeRepositoryMock = new Mock<IAnimeRepository>();
            var service = new AnimeService(animeRepositoryMock.Object);

            var existingAnimeModel = new AnimeModel
            {
                Id = 1,
                Name = "Old Anime",
                Imdbrate = 8.0,
                Photo = "OldPath",
                Text = "Old Description",
                Year = 2022
            };

            // Act
            service.Update(existingAnimeModel);

            // Assert
            animeRepositoryMock.Verify(repo => repo.Update(existingAnimeModel), Times.Once);
        }

        [Fact]
        public void Delete_CallsRepositoryDelete()
        {
            // Arrange
            var animeRepositoryMock = new Mock<IAnimeRepository>();
            var service = new AnimeService(animeRepositoryMock.Object);

            var animeIdToDelete = 1;

            // Act
            service.Delete(animeIdToDelete);

            // Assert
            animeRepositoryMock.Verify(repo => repo.Delete(animeIdToDelete), Times.Once);
        }
    }

    public class DislikedAnimeServiceTests
    {
        [Fact]
        public void GetDislikedAnimesForUser_ReturnsDislikedAnimeModels()
        {
            // Arrange
            var dislikedAnimeRepositoryMock = new Mock<IDislikedAnimeRepository>();
            var service = new DislikedAnimeService(dislikedAnimeRepositoryMock.Object);

            var userId = 1;

            var expectedDislikedAnimeList = new List<Anime>
        {
            new Anime { Id = 1, Name = "Disliked Anime 1", Imdbrate = 7.5, Photo = "Path1", Text = "Description 1", Year = 2022 },
            new Anime { Id = 2, Name = "Disliked Anime 2", Imdbrate = 6.8, Photo = "Path2", Text = "Description 2", Year = 2021 }
        };

            dislikedAnimeRepositoryMock.Setup(repo => repo.GetDislikedAnimesForUser(userId)).Returns(expectedDislikedAnimeList);

            // Act
            var result = service.GetDislikedAnimesForUser(userId);


            // Assert
            var expectedDislikedAnimeModels = expectedDislikedAnimeList
                .Select(anime => new AnimeModel
                {
                    Id = anime.Id,
                    Name = anime.Name,
                    Text = anime.Text,
                    Imdbrate = anime.Imdbrate,
                    Photo = anime.Photo,
                    Year = anime.Year
                })
                .ToList();

            result.Should().BeEquivalentTo(expectedDislikedAnimeModels, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void Insert_CallsRepositoryInsert()
        {
            // Arrange
            var dislikedAnimeRepositoryMock = new Mock<IDislikedAnimeRepository>();
            var service = new DislikedAnimeService(dislikedAnimeRepositoryMock.Object);

            var dislikedAnimeModelToAdd = new DislikedAnimeModel
            {
                UserId = 1,
                AnimeId = 1
            };

            // Act
            service.Insert(dislikedAnimeModelToAdd);

            // Assert
            dislikedAnimeRepositoryMock.Verify(repo => repo.Insert(dislikedAnimeModelToAdd), Times.Once);
        }

        [Fact]
        public void Delete_CallsRepositoryDelete()
        {
            // Arrange
            var dislikedAnimeRepositoryMock = new Mock<IDislikedAnimeRepository>();
            var service = new DislikedAnimeService(dislikedAnimeRepositoryMock.Object);

            var dislikedAnimeIdToDelete = 1;

            // Act
            service.Delete(dislikedAnimeIdToDelete);

            // Assert
            dislikedAnimeRepositoryMock.Verify(repo => repo.Delete(dislikedAnimeIdToDelete), Times.Once);
        }

        [Fact]
        public void GetLastId_ReturnsLastIdFromRepository()
        {
            // Arrange
            var dislikedAnimeRepositoryMock = new Mock<IDislikedAnimeRepository>();
            var service = new DislikedAnimeService(dislikedAnimeRepositoryMock.Object);

            var expectedLastId = 42;

            dislikedAnimeRepositoryMock.Setup(repo => repo.GetLastId()).Returns(expectedLastId);

            // Act
            var result = service.GetLastId();

            // Assert
            result.Should().Be(expectedLastId);
        }
    }

    public class GenreServiceTests
    {
        [Fact]
        public void GetAll_ReturnsGenreModels()
        {
            // Arrange
            var genreRepositoryMock = new Mock<IGenreRepository>();
            var service = new GenreService(genreRepositoryMock.Object);

            var expectedGenres = new List<Genre>
        {
            new Genre { Id = 1, Name = "Genre 1" },
            new Genre { Id = 2, Name = "Genre 2" }
        };

            genreRepositoryMock.Setup(repo => repo.GetAll()).Returns(expectedGenres);

            // Act
            var result = service.GetAll();

            // Assert
            var expectedGenreModels = expectedGenres
                .Select(genre => new GenreModel
                {
                    Id = genre.Id,
                    Name = genre.Name
                })
                .ToList();

            result.Should().BeEquivalentTo(expectedGenreModels, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void GetById_ReturnsGenreModel()
        {
            // Arrange
            var genreRepositoryMock = new Mock<IGenreRepository>();
            var service = new GenreService(genreRepositoryMock.Object);

            var targetGenreId = 1;
            var expectedGenre = new Genre { Id = targetGenreId, Name = "Genre 1" };

            genreRepositoryMock.Setup(repo => repo.GetById(targetGenreId)).Returns(expectedGenre);

            // Act
            var result = service.GetById(targetGenreId);

            // Assert
            var expectedGenreModel = new GenreModel
            {
                Id = expectedGenre.Id,
                Name = expectedGenre.Name
            };


            result.Should().BeEquivalentTo(expectedGenreModel, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void Insert_CallsRepositoryInsert()
        {
            // Arrange
            var genreRepositoryMock = new Mock<IGenreRepository>();
            var service = new GenreService(genreRepositoryMock.Object);

            var genreModelToAdd = new GenreModel { Name = "New Genre" };

            // Act
            service.Insert(genreModelToAdd);

            // Assert
            genreRepositoryMock.Verify(repo => repo.Insert(genreModelToAdd), Times.Once);
        }

        [Fact]
        public void Update_CallsRepositoryUpdate()
        {
            // Arrange
            var genreRepositoryMock = new Mock<IGenreRepository>();
            var service = new GenreService(genreRepositoryMock.Object);

            var genreModelToUpdate = new GenreModel { Id = 1, Name = "Updated Genre" };

            // Act
            service.Update(genreModelToUpdate);

            // Assert
            genreRepositoryMock.Verify(repo => repo.Update(genreModelToUpdate), Times.Once);
        }

        [Fact]
        public void Delete_CallsRepositoryDelete()
        {
            // Arrange
            var genreRepositoryMock = new Mock<IGenreRepository>();
            var service = new GenreService(genreRepositoryMock.Object);

            var targetGenreIdToDelete = 1;

            // Act
            service.Delete(targetGenreIdToDelete);

            // Assert
            genreRepositoryMock.Verify(repo => repo.Delete(targetGenreIdToDelete), Times.Once);
        }
    }

    public class LikedAnimeServiceTests
    {
        [Fact]
        public void GetLikedAnimesForUser_ReturnsLikedAnimeModels()
        {
            // Arrange
            var likedAnimeRepositoryMock = new Mock<ILikedAnimeRepository>();
            var service = new LikedAnimeService(likedAnimeRepositoryMock.Object);

            var targetUserId = 1;
            var likedAnimesFromRepository = new List<Anime>
        {
            new Anime { Id = 1, Name = "Liked Anime 1" },
            new Anime { Id = 2, Name = "Liked Anime 2" }
        };

            likedAnimeRepositoryMock.Setup(repo => repo.GetLikedAnimesForUser(targetUserId)).Returns(likedAnimesFromRepository);

            // Act
            var result = service.GetLikedAnimesForUser(targetUserId);

            // Assert
            var expectedLikedAnimeModels = likedAnimesFromRepository
                .Select(anime => new AnimeModel
                {
                    Id = anime.Id,
                    Name = anime.Name,
                    Text = anime.Text,
                    Imdbrate = anime.Imdbrate,
                    Photo = anime.Photo,
                    Year = anime.Year
                })
                .ToList();

            result.Should().BeEquivalentTo(expectedLikedAnimeModels, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void GetLastUserId_ReturnsLastUserIdFromRepository()
        {
            // Arrange
            var likedAnimeRepositoryMock = new Mock<ILikedAnimeRepository>();
            var service = new LikedAnimeService(likedAnimeRepositoryMock.Object);

            var expectedLastUserId = 1;
            likedAnimeRepositoryMock.Setup(repo => repo.GetLastUserId()).Returns(expectedLastUserId);

            // Act
            var result = service.GetLastUserId();

            // Assert
            result.Should().Be(expectedLastUserId);
        }

        [Fact]
        public void Insert_CallsRepositoryInsert()
        {
            // Arrange
            var likedAnimeRepositoryMock = new Mock<ILikedAnimeRepository>();
            var service = new LikedAnimeService(likedAnimeRepositoryMock.Object);

            var likedAnimeModelToAdd = new LikedAnimeModel { UserId = 1, AnimeId = 1 };

            // Act
            service.Insert(likedAnimeModelToAdd);


            // Assert
            likedAnimeRepositoryMock.Verify(repo => repo.Insert(likedAnimeModelToAdd), Times.Once);
        }

        [Fact]
        public void Delete_CallsRepositoryDelete()
        {
            // Arrange
            var likedAnimeRepositoryMock = new Mock<ILikedAnimeRepository>();
            var service = new LikedAnimeService(likedAnimeRepositoryMock.Object);

            var targetLikedAnimeIdToDelete = 1;

            // Act
            service.Delete(targetLikedAnimeIdToDelete);

            // Assert
            likedAnimeRepositoryMock.Verify(repo => repo.Delete(targetLikedAnimeIdToDelete), Times.Once);
        }
    }

    public class WatchedAnimeServiceTests
    {
        [Fact]
        public void GetWatchedAnimesForUser_ReturnsWatchedAnimeModels()
        {
            // Arrange
            var watchedAnimeRepositoryMock = new Mock<IWatchedAnimeRepository>();
            var service = new WatchedAnimeService(watchedAnimeRepositoryMock.Object);

            var targetUserId = 1;
            var watchedAnimesFromRepository = new List<Anime>
        {
            new Anime { Id = 1, Name = "Watched Anime 1" },
            new Anime { Id = 2, Name = "Watched Anime 2" }
        };

            watchedAnimeRepositoryMock.Setup(repo => repo.GetWatchedAnimesForUser(targetUserId)).Returns(watchedAnimesFromRepository);

            // Act
            var result = service.GetWatchedAnimesForUser(targetUserId);

            // Assert
            var expectedWatchedAnimeModels = watchedAnimesFromRepository
                .Select(anime => new AnimeModel
                {
                    Id = anime.Id,
                    Name = anime.Name,
                    Text = anime.Text,
                    Imdbrate = anime.Imdbrate,
                    Photo = anime.Photo,
                    Year = anime.Year
                })
                .ToList();

            result.Should().BeEquivalentTo(expectedWatchedAnimeModels, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void GetLastId_ReturnsLastIdFromRepository()
        {
            // Arrange
            var watchedAnimeRepositoryMock = new Mock<IWatchedAnimeRepository>();
            var service = new WatchedAnimeService(watchedAnimeRepositoryMock.Object);

            var expectedLastId = 1;
            watchedAnimeRepositoryMock.Setup(repo => repo.GetLastId()).Returns(expectedLastId);

            // Act
            var result = service.GetLastId();

            // Assert
            result.Should().Be(expectedLastId);
        }

        [Fact]
        public void Insert_CallsRepositoryInsert()
        {
            // Arrange
            var watchedAnimeRepositoryMock = new Mock<IWatchedAnimeRepository>();
            var service = new WatchedAnimeService(watchedAnimeRepositoryMock.Object);

            var watchedAnimeModelToAdd = new WatchedAnimeModel { UserId = 1, AnimeId = 1 };

            // Act
            service.Insert(watchedAnimeModelToAdd);

            // Assert
            watchedAnimeRepositoryMock.Verify(repo => repo.Insert(watchedAnimeModelToAdd), Times.Once);
        }

        [Fact]
        public void Delete_CallsRepositoryDelete()
        {
            // Arrange
            var watchedAnimeRepositoryMock = new Mock<IWatchedAnimeRepository>();
            var service = new WatchedAnimeService(watchedAnimeRepositoryMock.Object);

            var targetWatchedAnimeIdToDelete = 1;

            // Act
            service.Delete(targetWatchedAnimeIdToDelete);

            // Assert
            watchedAnimeRepositoryMock.Verify(repo => repo.Delete(targetWatchedAnimeIdToDelete), Times.Once);
        }
    }

    public class ReviewServiceTests
    {
        [Fact]
        public void GetById_ReturnsReviewModel()
        {
            // Arrange
            var reviewRepositoryMock = new Mock<IReviewRepository>();
            var service = new ReviewService(reviewRepositoryMock.Object);


            var targetReviewId = 1;
            var reviewFromRepository = new Review
            {
                Id = targetReviewId,
                UserId = 1,
                AnimeId = 1,
                Text = "Sample Review Text",
                Rate = 4
            };

            reviewRepositoryMock.Setup(repo => repo.GetById(targetReviewId)).Returns(reviewFromRepository);

            // Act
            var result = service.GetById(targetReviewId);

            // Assert
            var expectedReviewModel = new ReviewModel
            {
                Id = reviewFromRepository.Id,
                UserId = reviewFromRepository.UserId,
                AnimeId = reviewFromRepository.AnimeId,
                Text = reviewFromRepository.Text,
                Rate = reviewFromRepository.Rate
            };

            result.Should().BeEquivalentTo(expectedReviewModel, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void GetReviewsForAnime_ReturnsReviewModels()
        {
            // Arrange
            var reviewRepositoryMock = new Mock<IReviewRepository>();
            var service = new ReviewService(reviewRepositoryMock.Object);

            var targetAnimeId = 1;
            var reviewsFromRepository = new List<Review>
        {
            new Review { Id = 1, UserId = 1, AnimeId = targetAnimeId, Text = "Review 1", Rate = 4 },
            new Review { Id = 2, UserId = 2, AnimeId = targetAnimeId, Text = "Review 2", Rate = 5 }
        };

            reviewRepositoryMock.Setup(repo => repo.GetReviewsForAnime(targetAnimeId)).Returns(reviewsFromRepository);

            // Act
            var result = service.GetReviewsForAnime(targetAnimeId);

            // Assert
            var expectedReviewModels = reviewsFromRepository
                .Select(review => new ReviewModel
                {
                    Id = review.Id,
                    UserId = review.UserId,
                    AnimeId = review.AnimeId,
                    Text = review.Text,
                    Rate = review.Rate
                })
                .ToList();

            result.Should().BeEquivalentTo(expectedReviewModels, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void GetLastUserId_ReturnsLastUserIdFromRepository()
        {
            // Arrange
            var reviewRepositoryMock = new Mock<IReviewRepository>();
            var service = new ReviewService(reviewRepositoryMock.Object);

            var expectedLastUserId = 1;
            reviewRepositoryMock.Setup(repo => repo.GetLastId()).Returns(expectedLastUserId);

            // Act
            var result = service.GetLastId();

            // Assert
            result.Should().Be(expectedLastUserId);
        }

        [Fact]
        public void Insert_CallsRepositoryInsert()
        {
            // Arrange
            var reviewRepositoryMock = new Mock<IReviewRepository>();
            var service = new ReviewService(reviewRepositoryMock.Object);

            var reviewModelToAdd = new ReviewModel { UserId = 1, AnimeId = 1, Text = "New Review", Rate = 5 };

            // Act
            service.Insert(reviewModelToAdd);

            // Assert
            reviewRepositoryMock.Verify(repo => repo.Insert(reviewModelToAdd), Times.Once);
        }

        [Fact]
        public void Update_CallsRepositoryUpdate()
        {
            // Arrange
            var reviewRepositoryMock = new Mock<IReviewRepository>();
            var service = new ReviewService(reviewRepositoryMock.Object);

            var reviewModelToUpdate = new ReviewModel { Id = 1, UserId = 1, AnimeId = 1, Text = "Updated Review", Rate = 4 };

            // Act
            service.Update(reviewModelToUpdate);

            // Assert
            reviewRepositoryMock.Verify(repo => repo.Update(reviewModelToUpdate), Times.Once);
        }


        [Fact]
        public void Delete_CallsRepositoryDelete()
        {
            // Arrange
            var reviewRepositoryMock = new Mock<IReviewRepository>();
            var service = new ReviewService(reviewRepositoryMock.Object);

            var targetReviewIdToDelete = 1;

            // Act
            service.Delete(targetReviewIdToDelete);

            // Assert
            reviewRepositoryMock.Verify(repo => repo.Delete(targetReviewIdToDelete), Times.Once);
        }
    }

    public class UserServiceTests
    {
        [Fact]
        public void GetById_ReturnsUserInfoModel()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var service = new UserService(userRepositoryMock.Object);

            var targetUserId = 1;
            var userFromRepository = new UserInfo
            {
                Id = targetUserId,
                Username = "sampleUser",
                Password = "password",
                Email = "user@example.com",
                Name = "John Doe",
                Level = 1,
                Text = "User description",
                Photo = "user.jpg",
                WatchedCount = 10
            };

            userRepositoryMock.Setup(repo => repo.GetById(targetUserId)).Returns(userFromRepository);

            // Act
            var result = service.GetById(targetUserId);

            // Assert
            var expectedUserInfoModel = new UserInfoModel
            {
                Id = userFromRepository.Id,
                Username = userFromRepository.Username,
                Password = userFromRepository.Password,
                Email = userFromRepository.Email,
                Name = userFromRepository.Name,
                Level = userFromRepository.Level,
                Text = userFromRepository.Text,
                Photo = userFromRepository.Photo,
                WatchedCount = userFromRepository.WatchedCount
            };

            result.Should().BeEquivalentTo(expectedUserInfoModel, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void GetByUsername_ReturnsUserInfoModel()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var service = new UserService(userRepositoryMock.Object);

            var targetUsername = "sampleUser";
            var userFromRepository = new UserInfo
            {
                Id = 1,
                Username = targetUsername,
                Password = "password",
                Email = "user@example.com",
                Name = "John Doe",
                Level = 1,
                Text = "User description",
                Photo = "user.jpg",
                WatchedCount = 10
            };

            userRepositoryMock.Setup(repo => repo.GetByUsername(targetUsername)).Returns(userFromRepository);

            // Act
            var result = service.GetByUsername(targetUsername);

            // Assert
            var expectedUserInfoModel = new UserInfoModel
            {
                Id = userFromRepository.Id,
                Username = userFromRepository.Username,
                Password = userFromRepository.Password,
                Email = userFromRepository.Email,
                Name = userFromRepository.Name,
                Level = userFromRepository.Level,
                Text = userFromRepository.Text,
                Photo = userFromRepository.Photo,
                WatchedCount = userFromRepository.WatchedCount
            };

            result.Should().BeEquivalentTo(expectedUserInfoModel, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void GetAll_ReturnsUserInfoModels()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var service = new UserService(userRepositoryMock.Object);


            var usersFromRepository = new List<UserInfo>
        {
            new UserInfo { Id = 1, Username = "user1", Password = "pass1", Email = "user1@example.com", Name = "User 1", Level = 1, Text = "Description 1", Photo = "user1.jpg", WatchedCount = 5 },
            new UserInfo { Id = 2, Username = "user2", Password = "pass2", Email = "user2@example.com", Name = "User 2", Level = 2, Text = "Description 2", Photo = "user2.jpg", WatchedCount = 10 }
        };

            userRepositoryMock.Setup(repo => repo.GetAll()).Returns(usersFromRepository);

            // Act
            var result = service.GetAll();

            // Assert
            var expectedUserInfoModels = usersFromRepository
                .Select(user => new UserInfoModel
                {
                    Id = user.Id,
                    Username = user.Username,
                    Password = user.Password,
                    Email = user.Email,
                    Name = user.Name,
                    Level = user.Level,
                    Text = user.Text,
                    Photo = user.Photo,
                    WatchedCount = user.WatchedCount
                })
                .ToList();

            result.Should().BeEquivalentTo(expectedUserInfoModels, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void Insert_CallsRepositoryInsert()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var service = new UserService(userRepositoryMock.Object);

            var userInfoModelToAdd = new UserInfoModel { Username = "newUser", Password = "newPass", Email = "newUser@example.com", Name = "New User", Level = 1, Text = "New Description", Photo = "newUser.jpg", WatchedCount = 0 };

            // Act
            service.Insert(userInfoModelToAdd);

            // Assert
            userRepositoryMock.Verify(repo => repo.Insert(userInfoModelToAdd), Times.Once);
        }

        [Fact]
        public void Update_CallsRepositoryUpdate()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var service = new UserService(userRepositoryMock.Object);

            var userInfoModelToUpdate = new UserInfoModel { Id = 1, Username = "updatedUser", Password = "updatedPass", Email = "updatedUser@example.com", Name = "Updated User", Level = 2, Text = "Updated Description", Photo = "updatedUser.jpg", WatchedCount = 15 };

            // Act
            service.Update(userInfoModelToUpdate);

            // Assert
            userRepositoryMock.Verify(repo => repo.Update(userInfoModelToUpdate), Times.Once);
        }

        [Fact]
        public void Delete_CallsRepositoryDelete()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var service = new UserService(userRepositoryMock.Object);

            var targetUserIdToDelete = 1;

            // Act
            service.Delete(targetUserIdToDelete);

            // Assert
            userRepositoryMock.Verify(repo => repo.Delete(targetUserIdToDelete), Times.Once);
        }

        [Fact]
        public void IsExistUsername_ReturnsTrueIfUsernameExists()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var service = new UserService(userRepositoryMock.Object);

            var existingUsername = "existingUser";
            userRepositoryMock.Setup(repo => repo.IsExistUsername(existingUsername)).Returns(true);

            // Act
            var result = service.IsExistUsername(existingUsername);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsExistUsername_ReturnsFalseIfUsernameDoesNotExist()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var service = new UserService(userRepositoryMock.Object);


            var nonExistingUsername = "nonExistingUser";
            userRepositoryMock.Setup(repo => repo.IsExistUsername(nonExistingUsername)).Returns(false);

            // Act
            var result = service.IsExistUsername(nonExistingUsername);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsExistEmail_ReturnsTrueIfEmailExists()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var service = new UserService(userRepositoryMock.Object);

            var existingEmail = "existingUser@example.com";
            userRepositoryMock.Setup(repo => repo.IsExistEmail(existingEmail)).Returns(true);

            // Act
            var result = service.IsExistEmail(existingEmail);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsExistEmail_ReturnsFalseIfEmailDoesNotExist()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var service = new UserService(userRepositoryMock.Object);

            var nonExistingEmail = "nonExistingUser@example.com";
            userRepositoryMock.Setup(repo => repo.IsExistEmail(nonExistingEmail)).Returns(false);

            // Act
            var result = service.IsExistEmail(nonExistingEmail);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void GetLastUserId_ReturnsLastUserIdFromRepository()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var service = new UserService(userRepositoryMock.Object);

            var expectedLastUserId = 1;
            userRepositoryMock.Setup(repo => repo.GetLastUserId()).Returns(expectedLastUserId);

            // Act
            var result = service.GetLastId();

            // Assert
            result.Should().Be(expectedLastUserId);
        }

        [Fact]
        public void UpdateTitleAndText_CallsRepositoryUpdateTitleAndText()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var service = new UserService(userRepositoryMock.Object);

            var targetUserId = 1;
            var newTitle = "New Title";
            var newText = "New Text";

            // Act
            service.UpdateTitleAndText(targetUserId, newTitle, newText);

            // Assert
            userRepositoryMock.Verify(repo => repo.UpdateTitleAndText(targetUserId, newTitle, newText), Times.Once);
        }

        [Fact]
        public void WatchAnime_CallsRepositoryWatchAnime()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserInfoRepository>();
            var service = new UserService(userRepositoryMock.Object);

            var targetUserId = 1;

            // Act
            service.WatchAnime(targetUserId);

            // Assert
            userRepositoryMock.Verify(repo => repo.WatchAnime(targetUserId), Times.Once);
        }
    }
}
