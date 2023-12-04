using AniDAL.DataBaseClasses;
using System.ComponentModel.DataAnnotations;

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
        var userIdProperty = typeof(AddedAnime).GetProperty("UserId");

        // Act
        var propertyType = userIdProperty.PropertyType;

        // Assert
        Assert.Equal(typeof(int), propertyType);
    }

    [Fact]
    public void AddedAnime_AnimeId_Property_IsInt()
    {
        // Arrange
        var animeIdProperty = typeof(AddedAnime).GetProperty("AnimeId");

        // Act
        var propertyType = animeIdProperty.PropertyType;

        // Assert
        Assert.Equal(typeof(int), propertyType);
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
        var nameProperty = typeof(Anime).GetProperty("Name");

        // Act
        var requiredAttribute = nameProperty.GetCustomAttributes(typeof(RequiredAttribute), true);

        // Assert
        Assert.Single(requiredAttribute);
    }

    [Fact]
    public void Anime_Name_Property_MaxLengthAttributeIs255()
    {
        // Arrange
        var nameProperty = typeof(Anime).GetProperty("Name");

        // Act
        var maxLengthAttribute = (MaxLengthAttribute)nameProperty.GetCustomAttributes(typeof(MaxLengthAttribute), true)[0];

        // Assert
        Assert.Equal(255, maxLengthAttribute.Length);
    }

    [Fact]
    public void Anime_Text_Property_DoesNotHaveRequiredAttribute()
    {
        // Arrange
        var textProperty = typeof(Anime).GetProperty("Text");

        // Act
        var requiredAttribute = textProperty.GetCustomAttributes(typeof(RequiredAttribute), true);

        // Assert
        Assert.Empty(requiredAttribute);
    }

    [Fact]
    public void Anime_Imdbrate_Property_IsDouble()
    {
        // Arrange
        var imdbrateProperty = typeof(Anime).GetProperty("Imdbrate");

        // Act
        var propertyType = imdbrateProperty.PropertyType;

        // Assert
        Assert.Equal(typeof(double), propertyType);
    }

    [Fact]
    public void Anime_Photo_Property_MaxLengthAttributeIs255()
    {
        // Arrange
        var photoProperty = typeof(Anime).GetProperty("Photo");

        // Act
        var maxLengthAttribute = (MaxLengthAttribute)photoProperty.GetCustomAttributes(typeof(MaxLengthAttribute), true)[0];

        // Assert
        Assert.Equal(255, maxLengthAttribute.Length);
    }

    [Fact]
    public void Anime_Year_Property_IsInt()
    {
        // Arrange
        var yearProperty = typeof(Anime).GetProperty("Year");

        // Act
        var propertyType = yearProperty.PropertyType;

        // Assert
        Assert.Equal(typeof(int), propertyType);
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
        var animeIdProperty = typeof(AnimeGenre).GetProperty("AnimeId");

        // Act
        var propertyType = animeIdProperty.PropertyType;

        // Assert
        Assert.Equal(typeof(int), propertyType);
    }

    [Fact]
    public void AnimeGenre_GenreId_Property_IsInt()
    {
        // Arrange
        var genreIdProperty = typeof(AnimeGenre).GetProperty("GenreId");

        // Act
        var propertyType = genreIdProperty.PropertyType;

        // Assert
        Assert.Equal(typeof(int), propertyType);
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
        var userIdProperty = typeof(DislikedAnime).GetProperty("UserId");

        // Act
        var propertyType = userIdProperty.PropertyType;

        // Assert
        Assert.Equal(typeof(int), propertyType);
    }

    [Fact]
    public void DislikedAnime_AnimeId_Property_IsInt()
    {
        // Arrange
        var animeIdProperty = typeof(DislikedAnime).GetProperty("AnimeId");

        // Act
        var propertyType = animeIdProperty.PropertyType;

        // Assert
        Assert.Equal(typeof(int), propertyType);
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
        var nameProperty = typeof(Genre).GetProperty("Name");

        // Act
        var requiredAttribute = nameProperty.GetCustomAttributes(typeof(RequiredAttribute), true);

        // Assert
        Assert.Single(requiredAttribute);
    }

    [Fact]
    public void Genre_Name_Property_MaxLengthAttributeIs255()
    {
        // Arrange
        var nameProperty = typeof(Genre).GetProperty("Name");

        // Act
        var maxLengthAttribute = (MaxLengthAttribute)nameProperty.GetCustomAttributes(typeof(MaxLengthAttribute), true)[0];

        // Assert
        Assert.Equal(255, maxLengthAttribute.Length);
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
        var userIdProperty = typeof(LikedAnime).GetProperty("UserId");

        // Act
        var propertyType = userIdProperty.PropertyType;

        // Assert
        Assert.Equal(typeof(int), propertyType);
    }

    [Fact]
    public void LikedAnime_AnimeId_Property_IsInt()
    {
        // Arrange
        var animeIdProperty = typeof(LikedAnime).GetProperty("AnimeId");

        // Act
        var propertyType = animeIdProperty.PropertyType;

        // Assert
        Assert.Equal(typeof(int), propertyType);
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
        var userIdProperty = typeof(Review).GetProperty("UserId");

        // Act
        var propertyType = userIdProperty.PropertyType;

        // Assert
        Assert.Equal(typeof(int), propertyType);
    }

    [Fact]
    public void Review_AnimeId_Property_IsInt()
    {
        // Arrange
        var animeIdProperty = typeof(Review).GetProperty("AnimeId");

        // Act
        var propertyType = animeIdProperty.PropertyType;

        // Assert
        Assert.Equal(typeof(int), propertyType);
    }

    [Fact]
    public void Review_Text_Property_CanBeNull()
    {
        // Arrange
        var textProperty = typeof(Review).GetProperty("Text");

        // Act
        var requiredAttribute = textProperty.GetCustomAttributes(typeof(RequiredAttribute), true);

        // Assert
        Assert.Empty(requiredAttribute);
    }

    [Fact]
    public void Review_Rate_Property_IsInt()
    {
        // Arrange
        var rateProperty = typeof(Review).GetProperty("Rate");

        // Act
        var propertyType = rateProperty.PropertyType;

        // Assert
        Assert.Equal(typeof(int), propertyType);
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
        var usernameProperty = typeof(UserInfo).GetProperty("Username");

        // Act
        var requiredAttribute = usernameProperty.GetCustomAttributes(typeof(RequiredAttribute), true);

        // Assert
        Assert.Single(requiredAttribute);
    }

    [Fact]
    public void UserInfo_Username_Property_MaxLengthAttributeIs255()
    {
        // Arrange
        var usernameProperty = typeof(UserInfo).GetProperty("Username");

        // Act
        var maxLengthAttribute = (MaxLengthAttribute)usernameProperty.GetCustomAttributes(typeof(MaxLengthAttribute), true)[0];

        // Assert
        Assert.Equal(255, maxLengthAttribute.Length);
    }

    [Fact]
    public void UserInfo_Password_Property_HasRequiredAttribute()
    {
        // Arrange
        var passwordProperty = typeof(UserInfo).GetProperty("Password");

        // Act
        var requiredAttribute = passwordProperty.GetCustomAttributes(typeof(RequiredAttribute), true);

        // Assert
        Assert.Single(requiredAttribute);
    }

    [Fact]
    public void UserInfo_Password_Property_MaxLengthAttributeIs255()
    {
        // Arrange
        var passwordProperty = typeof(UserInfo).GetProperty("Password");

        // Act
        var maxLengthAttribute = (MaxLengthAttribute)passwordProperty.GetCustomAttributes(typeof(MaxLengthAttribute), true)[0];

        // Assert
        Assert.Equal(255, maxLengthAttribute.Length);
    }

    [Fact]
    public void UserInfo_Email_Property_HasRequiredAttribute()
    {
        // Arrange
        var emailProperty = typeof(UserInfo).GetProperty("Email");

        // Act
        var requiredAttribute = emailProperty.GetCustomAttributes(typeof(RequiredAttribute), true);

        // Assert
        Assert.Single(requiredAttribute);
    }

    [Fact]
    public void UserInfo_Email_Property_MaxLengthAttributeIs255()
    {
        // Arrange
        var emailProperty = typeof(UserInfo).GetProperty("Email");

        // Act
        var maxLengthAttribute = (MaxLengthAttribute)emailProperty.GetCustomAttributes(typeof(MaxLengthAttribute), true)[0];

        // Assert
        Assert.Equal(255, maxLengthAttribute.Length);
    }

    [Fact]
    public void UserInfo_Name_Property_CanBeNull()
    {
        // Arrange
        var nameProperty = typeof(UserInfo).GetProperty("Name");

        // Act
        var requiredAttribute = nameProperty.GetCustomAttributes(typeof(RequiredAttribute), true);

        // Assert
        Assert.Empty(requiredAttribute);
    }

    [Fact]
    public void UserInfo_Level_Property_IsInt()
    {
        // Arrange
        var levelProperty = typeof(UserInfo).GetProperty("Level");

        // Act
        var propertyType = levelProperty.PropertyType;

        // Assert
        Assert.Equal(typeof(int), propertyType);
    }

    [Fact]
    public void UserInfo_Text_Property_CanBeNull()
    {
        // Arrange
        var textProperty = typeof(UserInfo).GetProperty("Text");

        // Act
        var requiredAttribute = textProperty.GetCustomAttributes(typeof(RequiredAttribute), true);

        // Assert
        Assert.Empty(requiredAttribute);
    }

    [Fact]
    public void UserInfo_Photo_Property_MaxLengthAttributeIs255()
    {
        // Arrange
        var photoProperty = typeof(UserInfo).GetProperty("Photo");

        // Act
        var maxLengthAttribute = (MaxLengthAttribute)photoProperty.GetCustomAttributes(typeof(MaxLengthAttribute), true)[0];

        // Assert
        Assert.Equal(255, maxLengthAttribute.Length);
    }

    [Fact]
    public void UserInfo_WatchedCount_Property_IsInt()
    {
        // Arrange
        var watchedCountProperty = typeof(UserInfo).GetProperty("WatchedCount");

        // Act
        var propertyType = watchedCountProperty.PropertyType;

        // Assert
        Assert.Equal(typeof(int), propertyType);
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
        var userIdProperty = typeof(WatchedAnime).GetProperty("UserId");

        // Act
        var propertyType = userIdProperty.PropertyType;

        // Assert
        Assert.Equal(typeof(int), propertyType);
    }

    [Fact]
    public void WatchedAnime_AnimeId_Property_IsInt()
    {
        // Arrange
        var animeIdProperty = typeof(WatchedAnime).GetProperty("AnimeId");

        // Act
        var propertyType = animeIdProperty.PropertyType;

        // Assert
        Assert.Equal(typeof(int), propertyType);
    }
}
