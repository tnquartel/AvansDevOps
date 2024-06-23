using Xunit;
using Moq;
using Domain.Core.Entities;
using Domain.Core.Entities.Backlog;
using Domain.Core.Entities.Sprint;
using Domain.Services.Patterns.Factory.Factory_Interfaces;
using Domain.Services.Patterns.Observer;
using Domain.Services.Repositories;
using Application.Services.Services;
using System;
using Domain.Services.Patterns.State.ItemStates;
using Domain.Services.Services;

namespace Tests.Application.Services.Tests.Services
{
    public class ProjectServiceTests
    {
        private readonly Mock<IProjectRepository> _repositoryMock;
        private readonly Mock<ISprintService> _sprintServiceMock;
        private readonly ProjectService _projectService;

        public ProjectServiceTests()
        {
            _repositoryMock = new Mock<IProjectRepository>();
            _sprintServiceMock = new Mock<ISprintService>();
            _projectService = new ProjectService(_repositoryMock.Object, _sprintServiceMock.Object);
        }

        [Fact]
        public void CreateProject_ShouldCreateNewProject()
        {
            // Arrange
            var projectName = "New Project";
            var user = new User();
            _repositoryMock.Setup(r => r.Create(It.IsAny<Project>()));

            // Act
            var result = _projectService.CreateProject(projectName, user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(projectName, result.Name);
            Assert.Equal(user, result.ProductOwner);
            _repositoryMock.Verify(r => r.Create(result), Times.Once);
        }

        [Fact]
        public void AddUser_ShouldAddUser_WhenNotAlreadyPresent()
        {
            // Arrange
            var project = new Project("Test Project", new User());
            var user = new User();

            // Act
            _projectService.AddUser(user, project);

            // Assert
            Assert.Contains(user, project.Developers);
        }

        [Fact]
        public void AddUser_ShouldNotAddDuplicateUser()
        {
            // Arrange
            var project = new Project("Test Project", new User());
            var user = new User();
            project.Developers.Add(user);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _projectService.AddUser(user, project);

                // Assert
                Assert.Equal("User is already part of this project", sw.ToString().Trim());
                Assert.Single(project.Developers);
            }
        }

        [Fact]
        public void AddItem_ShouldAddItem_WhenNotAlreadyPresent()
        {
            // Arrange
            var project = new Project("Test Project", new User());
            var item = new Item(new ToDo());

            // Act
            _projectService.AddItem(item, project);

            // Assert
            Assert.Contains(item, project.Items);
        }

        [Fact]
        public void AddItem_ShouldNotAddDuplicateItem()
        {
            // Arrange
            var project = new Project("Test Project", new User());
            var item = new Item(new ToDo());
            project.Items.Add(item);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _projectService.AddItem(item, project);

                // Assert
                Assert.Equal("Project can't have duplicate items", sw.ToString().Trim());
                Assert.Single(project.Items);
            }
        }

        [Fact]
        public void AddSprint_ShouldAddNewSprint()
        {
            // Arrange
            var project = new Project("Test Project", new User());
            var sprintFactoryMock = new Mock<ISprintFactory>();
            var goal = "Sprint Goal";
            var subject = new Mock<ISubject>().Object;
            var sprintMock = new Mock<ISprint>().Object;

            _sprintServiceMock.Setup(s => s.NewSprint(sprintFactoryMock.Object, project, goal, subject)).Returns(sprintMock);

            // Act
            _projectService.AddSprint(sprintFactoryMock.Object, project, goal, subject);

            // Assert
            Assert.Contains(sprintMock, project.Sprints);
            _sprintServiceMock.Verify(s => s.NewSprint(sprintFactoryMock.Object, project, goal, subject), Times.Once);
        }
    }
}
