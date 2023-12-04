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
        var newData = new UserInfo { Id = 100, Name = "name", Photo = "path", Text = "text", Email = "email", Level = 1, Password = "password", Username = "username" };
        int targetId = newData.Id;
        repositoryMock.Setup(repo => repo.GetById(targetId)).Returns(new UserInfo { Id = targetId });
        repositoryMock.Setup(repo => repo.GetLastId()).Returns(99);
        repositoryMock.Setup(repo => repo.Insert(newData));

        var repository = repositoryMock.Object;

        // Act
        repository.Insert(newData);

        // Assert
        var result = repository.GetById(newData.Id);

        result.Should().NotBeNull(); // Check if the result is not null

        // Use BeEquivalentTo only for checking property values, not for reference equality
        result.Should().BeEquivalentTo(newData, options => options.ExcludingMissingMembers());

        // Cleanup
        repository.Delete(newData.Id);
    }

    [Fact]
    public void Update_UpdatesRecord()
    {
        // Arrange
        var repositoryMock = new Mock<IGenericRepository<UserInfo>>();
        var newData = new UserInfo { Id = 1, Name = "newname", Photo = "newpath", Text = "text", Email = "email", Level = 1, Password = "password", Username = "username" };

        repositoryMock.Setup(repo => repo.Update(newData));

        var repository = repositoryMock.Object;

        // Act
        repository.Update(newData);

        // Assert
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
