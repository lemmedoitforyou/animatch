using AniDAL.DataBaseClasses;
using System.ComponentModel.DataAnnotations;
using Xunit;

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

    // You can write similar tests for other properties like Text, Imdbrate, Photo, and Year.
}
