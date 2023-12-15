using AniDAL.DataBaseClasses;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Moq;

namespace DataTable
{
    public class AddedAnimeTests
    {
        [Fact]
        public void AddedAnime_Class_HasKeyAttributeOnIdProperty()
        {
            // Arrange
            var idProperty = typeof(AddedAnime).GetProperty("Id");

            // Act
            var keyAttribute = idProperty.GetCustomAttributes(typeof(KeyAttribute), true);

            // Assert
            Assert.Single(keyAttribute);
        }

        [Fact]
        public void AddedAnime_UserId_Property_IsInt()
        {
            // Arrange
            var addedAnimeMock = new Mock<AddedAnime>();

            // Act
            var propertyType = addedAnimeMock.Object.UserId.GetType();

            // Assert
            propertyType.Should().Be(typeof(int));
        }

        [Fact]
        public void AddedAnime_AnimeId_Property_IsInt()
        {
            // Arrange
            var addedAnimeMock = new Mock<AddedAnime>();

            // Act
            var propertyType = addedAnimeMock.Object.AnimeId.GetType();

            // Assert
            propertyType.Should().Be(typeof(int));
        }
    }

    public class AnimeTests
    {
        [Fact]
        public void Anime_Class_HasKeyAttributeOnIdProperty()
        {
            // Arrange
            var idProperty = typeof(Anime).GetProperty("Id");

            // Act
            var keyAttribute = idProperty.GetCustomAttributes(typeof(KeyAttribute), true);

            // Assert
            Assert.Single(keyAttribute);
        }

        [Fact]
        public void Anime_Name_Property_HasRequiredAttribute()
        {
            // Arrange
            var animeMock = new Mock<Anime>();

            // Act
            var requiredAttribute = animeMock.Object.GetType().GetProperty("Name").GetCustomAttributes(typeof(RequiredAttribute), true);

            // Assert
            requiredAttribute.Should().HaveCount(1);
        }

        [Fact]
        public void Anime_Name_Property_MaxLengthAttributeIs255()
        {
            // Arrange
            var animeMock = new Mock<Anime>();

            // Act
            var maxLengthAttribute = (MaxLengthAttribute)animeMock.Object.GetType().GetProperty("Name").GetCustomAttributes(typeof(MaxLengthAttribute), true)[0];

            // Assert
            maxLengthAttribute.Length.Should().Be(255);
        }

        [Fact]
        public void Anime_Text_Property_DoesNotHaveRequiredAttribute()
        {
            // Arrange
            var animeMock = new Mock<Anime>();

            // Act
            var requiredAttribute = animeMock.Object.GetType().GetProperty("Text").GetCustomAttributes(typeof(RequiredAttribute), true);

            // Assert
            requiredAttribute.Should().BeEmpty();
        }

        [Fact]
        public void Anime_Imdbrate_Property_IsDouble()
        {
            // Arrange
            var animeMock = new Mock<Anime>();

            // Act
            var propertyType = animeMock.Object.GetType().GetProperty("Imdbrate").PropertyType;

            // Assert
            propertyType.Should().Be(typeof(double));
        }

        [Fact]
        public void Anime_Photo_Property_MaxLengthAttributeIs255()
        {
            // Arrange
            var animeMock = new Mock<Anime>();

            // Act
            var maxLengthAttribute = (MaxLengthAttribute)animeMock.Object.GetType().GetProperty("Photo").GetCustomAttributes(typeof(MaxLengthAttribute), true)[0];

            // Assert
            maxLengthAttribute.Length.Should().Be(255);
        }

        [Fact]
        public void Anime_Year_Property_IsInt()
        {
            // Arrange
            var animeMock = new Mock<Anime>();

            // Act
            var propertyType = animeMock.Object.GetType().GetProperty("Year").PropertyType;

            // Assert
            propertyType.Should().Be(typeof(int));
        }
    }


    public class AnimeGenreTests
    {
        [Fact]
        public void AnimeGenre_Class_HasKeyAttributeOnIdProperty()
        {
            // Arrange
            var idProperty = typeof(AnimeGenre).GetProperty("Id");

            // Act
            var keyAttribute = idProperty.GetCustomAttributes(typeof(KeyAttribute), true);

            // Assert
            Assert.Single(keyAttribute);
        }

        [Fact]
        public void AnimeGenre_AnimeId_Property_IsInt()
        {
            // Arrange
            var animeGenreMock = new Mock<AnimeGenre>();

            // Act
            var propertyType = animeGenreMock.Object.GetType().GetProperty("AnimeId").PropertyType;

            // Assert
            propertyType.Should().Be(typeof(int));
        }

        [Fact]
        public void AnimeGenre_GenreId_Property_IsInt()
        {
            // Arrange
            var animeGenreMock = new Mock<AnimeGenre>();

            // Act
            var propertyType = animeGenreMock.Object.GetType().GetProperty("GenreId").PropertyType;

            // Assert
            propertyType.Should().Be(typeof(int));
        }
    }

    public class DislikedAnimeTests
    {
        [Fact]
        public void DislikedAnime_Class_HasKeyAttributeOnIdProperty()
        {
            // Arrange
            var idProperty = typeof(DislikedAnime).GetProperty("Id");

            // Act
            var keyAttribute = idProperty.GetCustomAttributes(typeof(KeyAttribute), true);

            // Assert
            Assert.Single(keyAttribute);
        }

        [Fact]
        public void DislikedAnime_UserId_Property_IsInt()
        {
            // Arrange
            var dislikedAnimeMock = new Mock<DislikedAnime>();

            // Act
            var propertyType = dislikedAnimeMock.Object.GetType().GetProperty("UserId").PropertyType;

            // Assert
            propertyType.Should().Be(typeof(int));
        }

        [Fact]
        public void DislikedAnime_AnimeId_Property_IsInt()
        {
            // Arrange
            var dislikedAnimeMock = new Mock<DislikedAnime>();

            // Act
            var propertyType = dislikedAnimeMock.Object.GetType().GetProperty("AnimeId").PropertyType;

            // Assert
            propertyType.Should().Be(typeof(int));
        }
    }

    public class GenreTests
    {
        [Fact]
        public void Genre_Class_HasKeyAttributeOnIdProperty()
        {
            // Arrange
            var idProperty = typeof(Genre).GetProperty("Id");

            // Act
            var keyAttribute = idProperty.GetCustomAttributes(typeof(KeyAttribute), true);

            // Assert
            Assert.Single(keyAttribute);
        }

        [Fact]
        public void Genre_Name_Property_HasRequiredAttribute()
        {
            // Arrange
            var genreMock = new Mock<Genre>();

            // Act
            var requiredAttribute = genreMock.Object.GetType().GetProperty("Name").GetCustomAttributes(typeof(RequiredAttribute), true);

            // Assert
            requiredAttribute.Should().HaveCount(1);
        }

        [Fact]
        public void Genre_Name_Property_MaxLengthAttributeIs255()
        {
            // Arrange
            var genreMock = new Mock<Genre>();

            // Act
            var maxLengthAttribute = (MaxLengthAttribute)genreMock.Object.GetType().GetProperty("Name").GetCustomAttributes(typeof(MaxLengthAttribute), true)[0];

            // Assert
            maxLengthAttribute.Length.Should().Be(255);
        }
    }

    public class LikedAnimeTests
    {
        [Fact]
        public void LikedAnime_Class_HasKeyAttributeOnIdProperty()
        {
            // Arrange
            var idProperty = typeof(LikedAnime).GetProperty("Id");

            // Act
            var keyAttribute = idProperty.GetCustomAttributes(typeof(KeyAttribute), true);

            // Assert
            Assert.Single(keyAttribute);
        }


        [Fact]
        public void LikedAnime_UserId_Property_IsInt()
        {
            // Arrange
            var likedAnimeMock = new Mock<LikedAnime>();

            // Act
            var propertyType = likedAnimeMock.Object.GetType().GetProperty("UserId").PropertyType;

            // Assert
            propertyType.Should().Be(typeof(int));
        }

        [Fact]
        public void LikedAnime_AnimeId_Property_IsInt()
        {
            // Arrange
            var likedAnimeMock = new Mock<LikedAnime>();

            // Act
            var propertyType = likedAnimeMock.Object.GetType().GetProperty("AnimeId").PropertyType;

            // Assert
            propertyType.Should().Be(typeof(int));
        }
    }

    public class ReviewTests
    {
        [Fact]
        public void Review_Class_HasKeyAttributeOnIdProperty()
        {
            // Arrange
            var idProperty = typeof(Review).GetProperty("Id");

            // Act
            var keyAttribute = idProperty.GetCustomAttributes(typeof(KeyAttribute), true);

            // Assert
            Assert.Single(keyAttribute);
        }

        [Fact]
        public void Review_UserId_Property_IsInt()
        {
            // Arrange
            var reviewMock = new Mock<Review>();

            // Act
            var propertyType = reviewMock.Object.GetType().GetProperty("UserId").PropertyType;

            // Assert
            propertyType.Should().Be(typeof(int));
        }

        [Fact]
        public void Review_AnimeId_Property_IsInt()
        {
            // Arrange
            var reviewMock = new Mock<Review>();

            // Act
            var propertyType = reviewMock.Object.GetType().GetProperty("AnimeId").PropertyType;

            // Assert
            propertyType.Should().Be(typeof(int));
        }

        [Fact]
        public void Review_Text_Property_CanBeNull()
        {
            // Arrange
            var reviewMock = new Mock<Review>();

            // Act
            var requiredAttribute = reviewMock.Object.GetType().GetProperty("Text").GetCustomAttributes(typeof(RequiredAttribute), true);

            // Assert
            requiredAttribute.Should().BeEmpty();
        }

        [Fact]
        public void Review_Rate_Property_IsInt()
        {
            // Arrange
            var reviewMock = new Mock<Review>();

            // Act
            var propertyType = reviewMock.Object.GetType().GetProperty("Rate").PropertyType;

            // Assert
            propertyType.Should().Be(typeof(int));
        }
    }

    public class UserInfoTests
    {
        [Fact]
        public void UserInfo_Class_HasKeyAttributeOnIdProperty()
        {
            // Arrange
            var idProperty = typeof(UserInfo).GetProperty("Id");

            // Act
            var keyAttribute = idProperty.GetCustomAttributes(typeof(KeyAttribute), true);

            // Assert
            Assert.Single(keyAttribute);
        }

        [Fact]
        public void UserInfo_Username_Property_HasRequiredAttribute()
        {
            // Arrange
            var userInfoMock = new Mock<UserInfo>();

            // Act
            var requiredAttribute = userInfoMock.Object.GetType().GetProperty("Username").GetCustomAttributes(typeof(RequiredAttribute), true);

            // Assert
            requiredAttribute.Should().HaveCount(1);
        }

        [Fact]
        public void UserInfo_Username_Property_MaxLengthAttributeIs255()
        {
            // Arrange
            var userInfoMock = new Mock<UserInfo>();

            // Act
            var maxLengthAttribute = (MaxLengthAttribute)userInfoMock.Object.GetType().GetProperty("Username").GetCustomAttributes(typeof(MaxLengthAttribute), true)[0];

            // Assert
            maxLengthAttribute.Length.Should().Be(255);
        }

        [Fact]
        public void UserInfo_Password_Property_HasRequiredAttribute()
        {
            // Arrange
            var userInfoMock = new Mock<UserInfo>();


            // Act
            var requiredAttribute = userInfoMock.Object.GetType().GetProperty("Password").GetCustomAttributes(typeof(RequiredAttribute), true);

            // Assert
            requiredAttribute.Should().HaveCount(1);
        }

        [Fact]
        public void UserInfo_Password_Property_MaxLengthAttributeIs255()
        {
            // Arrange
            var userInfoMock = new Mock<UserInfo>();

            // Act
            var maxLengthAttribute = (MaxLengthAttribute)userInfoMock.Object.GetType().GetProperty("Password").GetCustomAttributes(typeof(MaxLengthAttribute), true)[0];

            // Assert
            maxLengthAttribute.Length.Should().Be(255);
        }

        [Fact]
        public void UserInfo_Email_Property_HasRequiredAttribute()
        {
            // Arrange
            var userInfoMock = new Mock<UserInfo>();

            // Act
            var requiredAttribute = userInfoMock.Object.GetType().GetProperty("Email").GetCustomAttributes(typeof(RequiredAttribute), true);

            // Assert
            requiredAttribute.Should().HaveCount(1);
        }

        [Fact]
        public void UserInfo_Email_Property_MaxLengthAttributeIs255()
        {
            // Arrange
            var userInfoMock = new Mock<UserInfo>();

            // Act
            var maxLengthAttribute = (MaxLengthAttribute)userInfoMock.Object.GetType().GetProperty("Email").GetCustomAttributes(typeof(MaxLengthAttribute), true)[0];

            // Assert
            maxLengthAttribute.Length.Should().Be(255);
        }

        [Fact]
        public void UserInfo_Name_Property_CanBeNull()
        {
            // Arrange
            var userInfoMock = new Mock<UserInfo>();

            // Act
            var requiredAttribute = userInfoMock.Object.GetType().GetProperty("Name").GetCustomAttributes(typeof(RequiredAttribute), true);

            // Assert
            requiredAttribute.Should().BeEmpty();
        }

        [Fact]
        public void UserInfo_Level_Property_IsInt()
        {
            // Arrange
            var userInfoMock = new Mock<UserInfo>();

            // Act
            var propertyType = userInfoMock.Object.GetType().GetProperty("Level").PropertyType;

            // Assert
            propertyType.Should().Be(typeof(int));
        }

        [Fact]
        public void UserInfo_Text_Property_CanBeNull()
        {
            // Arrange
            var userInfoMock = new Mock<UserInfo>();

            // Act
            var requiredAttribute = userInfoMock.Object.GetType().GetProperty("Text").GetCustomAttributes(typeof(RequiredAttribute), true);

            // Assert
            requiredAttribute.Should().BeEmpty();
        }

        [Fact]
        public void UserInfo_Photo_Property_MaxLengthAttributeIs255()
        {
            // Arrange
            var userInfoMock = new Mock<UserInfo>();

            // Act
            var maxLengthAttribute = (MaxLengthAttribute)userInfoMock.Object.GetType().GetProperty("Photo").GetCustomAttributes(typeof(MaxLengthAttribute), true)[0];

            // Assert
            maxLengthAttribute.Length.Should().Be(255);
        }

        [Fact]
        public void UserInfo_WatchedCount_Property_IsInt()
        {
            // Arrange
            var userInfoMock = new Mock<UserInfo>();

            // Act
            var propertyType = userInfoMock.Object.GetType().GetProperty("WatchedCount").PropertyType;

            // Assert
            propertyType.Should().Be(typeof(int));
        }
    }

    public class WatchedAnimeTests
    {
        [Fact]
        public void WatchedAnime_Class_HasKeyAttributeOnIdProperty()
        {
            // Arrange
            var idProperty = typeof(WatchedAnime).GetProperty("Id");

            // Act
            var keyAttribute = idProperty.GetCustomAttributes(typeof(KeyAttribute), true);

            // Assert
            Assert.Single(keyAttribute);
        }


        [Fact]
        public void WatchedAnime_UserId_Property_IsInt()
        {
            // Arrange
            var watchedAnimeMock = new Mock<WatchedAnime>();

            // Act
            var propertyType = watchedAnimeMock.Object.GetType().GetProperty("UserId").PropertyType;

            // Assert
            propertyType.Should().Be(typeof(int));
        }

        [Fact]
        public void WatchedAnime_AnimeId_Property_IsInt()
        {
            // Arrange
            var watchedAnimeMock = new Mock<WatchedAnime>();

            // Act
            var propertyType = watchedAnimeMock.Object.GetType().GetProperty("AnimeId").PropertyType;

            // Assert
            propertyType.Should().Be(typeof(int));
        }
    }
}
