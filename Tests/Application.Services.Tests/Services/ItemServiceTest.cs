﻿using Xunit;
using Moq;
using Domain.Core.Entities;
using Domain.Core.Entities.Backlog;
using Domain.Services.Patterns.State.ItemStates;
using Domain.Services.Repositories;
using Application.Services.Services;
using System;
using Domain.Services.Services;

namespace Tests.Application.Services.Tests.Services
{
    [Collection("Item Service Tests")]
    [CollectionDefinition("Item Service Tests", DisableParallelization = true)]

    public class ItemServiceTests
    {
        private readonly Mock<IItemRepository> _repositoryMock;
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<ISprintService> _sprintServiceMock;
        private readonly ItemService _itemService;
        private readonly ProjectService _projectServiceMock;

        public ItemServiceTests()
        {
            _repositoryMock = new Mock<IItemRepository>();
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _sprintServiceMock = new Mock<ISprintService>();
            _userServiceMock = new Mock<IUserService>();
            _projectServiceMock = new ProjectService(_projectRepositoryMock.Object, _sprintServiceMock.Object);
            _itemService = new ItemService(_repositoryMock.Object, _projectServiceMock ,_userServiceMock.Object);
        }

        [Fact]
        public void CreateItem_ShouldCreateNewItem()
        {
            // Arrange
            _repositoryMock.Setup(r => r.Create(It.IsAny<Item>()));

            // Act
            var result = _itemService.CreateItem("test name");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ToDo>(result.State);
            Assert.NotNull(((ToDo)result.State).Item);
            _repositoryMock.Verify(r => r.Create(result), Times.Once);
        }

        [Fact]
        public void AddActivity_ShouldAddActivity_WhenNotAlreadyPresent()
        {
            // Arrange
            var item = new Item(new ToDo());
            var activity = new ActivityItem(item);

            // Act
            _itemService.AddActivity(item, activity);

            // Assert
            Assert.Contains(activity, item.Activities);
        }

        [Fact]
        public void AddActivity_ShouldNotAddDuplicateActivity()
        {
            // Arrange
            var item = new Item(new ToDo());
            var activity = new ActivityItem(item);
            item.Activities.Add(activity);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _itemService.AddActivity(item, activity);

                // Assert
                Assert.Equal("Item can't have duplicate activities", sw.ToString().Trim());
                Assert.Single(item.Activities);
                sw.Close();
            }
        }

        [Fact]
        public void NextState_ShouldChangeState()
        {
            // Arrange
            var stateMock = new Mock<IStateItem>();
            var item = new Item(stateMock.Object);

            // Act
            _itemService.NextState(item);

            // Assert
            stateMock.Verify(s => s.NextState(), Times.Once);
        }

        [Fact]
        public void AssignDev_ShouldCallCoupleToFirstAvailable()
        {
            // Arrange
            var item = new Item(new ToDo());
            var user = new User();

            // Act
            _itemService.AssignDev(item, user);

            // Assert
            _userServiceMock.Verify(us => us.CoupleToFirstAvailable(item, user), Times.Once);
        }

        [Fact]
        public void NewThread_ShouldCreateNewThread_WhenThreadIsNull()
        {
            // Arrange
            var item = new Item(new ToDo());

            // Act
            _itemService.NewThread(item);

            // Assert
            Assert.NotNull(item.Thread);
            Assert.Equal(item, item.Thread.ParentItem);
        }

        [Fact]
        public void NewThread_ShouldNotCreateNewThread_WhenThreadIsNotNull()
        {
            // Arrange
            var item = new Item(new ToDo())
            {
                Thread = new MessageThread()
            };

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _itemService.NewThread(item);

                // Assert
                Assert.Equal("This item already contains a thread.", sw.ToString().Trim());
                Assert.NotNull(item.Thread); // Ensure thread is still not null
                sw.Close();
            }
        }

        [Fact]
        public void ItemDone_ShouldBePossible_WhenTestFails()
        {
            // Arrange
            var item = _itemService.CreateItem("test");

            // Act
            _itemService.NextState(item);
            _itemService.NextState(item);
            _itemService.NextState(item);
            _itemService.FailTest(item);
            _itemService.NextState(item);
            _itemService.NextState(item);
            _itemService.NextState(item);
            _itemService.NextState(item);
            _itemService.NextState(item);

            // Assert
            Assert.IsType<Done>(item.State); 
            Assert.NotNull(((Done)item.State).Item); 
            Assert.Same(item, ((Done)item.State).Item); 

        }

        [Fact]
        public void ItemDone_ShouldBePossible_WhenRejectedAndRestarted()
        {
            // Arrange
            var item = _itemService.CreateItem("test");

            // Act
            _itemService.NextState(item);
            _itemService.NextState(item);
            _itemService.NextState(item);
            _itemService.NextState(item);
            _itemService.Rejected(item);
            _itemService.NextState(item);
            _itemService.NextState(item);
            _itemService.NextState(item);
            _itemService.NextState(item);
            _itemService.NextState(item);
            _itemService.NextState(item);
            _itemService.NextState(item);
            _itemService.NextState(item);
            _itemService.NextState(item);

            // Assert
            Assert.IsType<Done>(item.State);
            Assert.NotNull(((Done)item.State).Item);
            Assert.Same(item, ((Done)item.State).Item);

        }
    }
}
