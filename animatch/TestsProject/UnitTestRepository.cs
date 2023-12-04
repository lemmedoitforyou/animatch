using AniDAL.DataBaseClasses;
using AniDAL.DbContext;
using AniDAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using Xunit;

public class GenericRepositoryTests
{
    [Fact]
    public void GetAll_ReturnsAllRecords()
    {
        var repository = new GenericRepository<Anime>();
        
        var result = repository.GetAll();

        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Anime>>(result);
        Assert.Equal(50, result.Count());
    }

    [Fact]
    public void GetById_ReturnsCorrectRecord()
    {
        var repository = new GenericRepository<Anime>();

        var targetId = 2;

        var result = repository.GetById(targetId);

        Assert.NotNull(result);
        Assert.IsAssignableFrom<Anime>(result);
        Assert.Equal(targetId, result.Id);
    }

    [Fact]
    public void Insert_AddsNewRecord()
    {
        var repository = new GenericRepository<UserInfo>();

        var newData = new UserInfo
        {
            Id = 100500,
            Name = "name",
            Photo = "path",
            Text = "text",
            Email = "email",
            Level = 1,
            Password = "password",
            Username = "username" 
        };

        repository.Insert(newData);

        var result = repository.GetById(100500);

        Assert.NotNull(result);
        Assert.IsAssignableFrom<UserInfo>(result);
        Assert.Equal(100500, result.Id);

        repository.Delete(100500);
    }

    [Fact]
    public void Update_UpdatesRecord()
    {
        var repository = new GenericRepository<UserInfo>();

        var newData = new UserInfo
        {
            Id = 100500,
            Name = "newname",
            Photo = "newpath",
            Text = "text",
            Email = "email",
            Level = 1,
            Password = "password",
            Username = "username"
        };
        repository.Update(newData);

        var result = repository.GetById(100500);

        Assert.NotNull(result);
        Assert.IsAssignableFrom<Anime>(result);
        Assert.Equal("newpath", result.Photo);
        Assert.Equal("newname", result.Name);
    }

    //[Fact]
    //public void Delete_RemovesRecord()
    //{
    //    // Arrange
    //    var dbContext = new Mock<ApplicationDbContext>();
    //    var repository = new GenericRepository<YourEntity>(dbContext.Object);

    //    var targetId = 1;

    //    // Act
    //    repository.Delete(targetId);

    //    // Assert
    //    dbContext.Verify(c => c.Set<YourEntity>().Remove(It.IsAny<YourEntity>()), Times.Once);
    //    dbContext.Verify(c => c.SaveChanges(), Times.Once);
    //}

    //[Fact]
    //public void Save_SavesChanges()
    //{
    //    // Arrange
    //    var dbContext = new Mock<ApplicationDbContext>();
    //    var repository = new GenericRepository<YourEntity>(dbContext.Object);

    //    // Act
    //    repository.Save();

    //    // Assert
    //    dbContext.Verify(c => c.SaveChanges(), Times.Once);
    //}
}
