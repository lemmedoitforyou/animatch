using Xunit;
using FluentAssertions;
using AniDAL.DataBaseClasses;
using AniDAL.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;

public class GenericRepositoryTests
{
    [Fact]
    public void GetAll_ReturnsAllRecords()
    {
        // Arrange
        var repositoryMock = new Mock<IGenericRepository<Anime>>();
        repositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Anime> { new Anime(), new Anime() });

        var repository = repositoryMock.Object;

        // Act
        var result = repository.GetAll();

        // Assert
        result.Should().NotBeNull().And.BeAssignableTo<IEnumerable<Anime>>().And.NotBeEmpty();
    }

    [Fact]
    public void GetById_ReturnsCorrectRecord()
    {
        // Arrange
        var repositoryMock = new Mock<IGenericRepository<Anime>>();
        var targetId = 2;
        repositoryMock.Setup(repo => repo.GetById(targetId)).Returns(new Anime { Id = targetId });

        var repository = repositoryMock.Object;

        // Act
        var result = repository.GetById(targetId);

        // Assert
        result.Should().NotBeNull().And.BeAssignableTo<Anime>().And.BeEquivalentTo(new Anime { Id = targetId });
    }

    [Fact]
    public void Insert_AddsNewRecord()
    {
        // Arrange
        var repositoryMock = new Mock<IGenericRepository<UserInfo>>();
        var newData = new UserInfo
        {
            Id = 100,
            Name = "name",
            Photo = "path",
            Text = "text",
            Email = "email",
            Level = 1,
            Password = "password",
            Username = "username"
        };

        int targetId = newData.Id;

        // Set up GetById to return null initially (record not found)
        repositoryMock.Setup(repo => repo.GetById(targetId)).Returns((UserInfo)null);

        // Set up GetLastId to return a value (e.g., 99)
        repositoryMock.Setup(repo => repo.GetLastId()).Returns(99);

        // Set up Insert to add the new data to a list or perform necessary logic
        List<UserInfo> repositoryData = new List<UserInfo>();
        repositoryMock.Setup(repo => repo.Insert(It.IsAny<UserInfo>())).Callback<UserInfo>(data =>
        {
            repositoryData.Add(data);
        });

        // Set up GetById to return the inserted record after Insert is called
        repositoryMock.Setup(repo => repo.GetById(targetId)).Returns(newData);

        var repository = repositoryMock.Object;

        // Act
        repository.Insert(newData);

        // Assert
        var result = repository.GetById(newData.Id);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(newData, options => options.ExcludingMissingMembers());

    }

    [Fact]
    public void Update_UpdatesRecord()
    {
        // Arrange
        var repositoryMock = new Mock<IGenericRepository<UserInfo>>();
        var newData = new UserInfo
        {
            Id = 1,
            Name = "newname",
            Photo = "newpath",
            Text = "text",
            Email = "email",
            Level = 1,
            Password = "password",
            Username = "username"
        };

        UserInfo updatedData = null;
        repositoryMock.Setup(repo => repo.Update(It.IsAny<UserInfo>()))
            .Callback<UserInfo>(data => updatedData = data);

        var repository = repositoryMock.Object;

        // Act
        repository.Update(newData);

        // Assert
        updatedData.Should().NotBeNull().And.BeEquivalentTo(newData);

        var result = repository.GetById(1);
        result.Should().NotBeNull().And.BeEquivalentTo(newData);
    }

    //[Fact]
    //public void Delete_RemovesRecord()
    //{
    //    // Arrange
    //    var repositoryMock = new Mock<IGenericRepository<YourEntity>>();
    //    var targetId = 1;

    //    repositoryMock.Setup(repo => repo.Delete(targetId));

    //    var repository = repositoryMock.Object;

    //    // Act
    //    repository.Delete(targetId);

    //    // Assert
    //    repositoryMock.Verify(repo => repo.Delete(targetId), Times.Once);
    //}

    //[Fact]
    //public void Save_SavesChanges()
    //{
    //    // Arrange
    //    var repositoryMock = new Mock<IGenericRepository<YourEntity>>();
    //    repositoryMock.Setup(repo => repo.Save());

    //    var repository = repositoryMock.Object;

    //    // Act
    //    repository.Save();

    //    // Assert
    //    repositoryMock.Verify(repo => repo.Save(), Times.Once);
    //}
}
