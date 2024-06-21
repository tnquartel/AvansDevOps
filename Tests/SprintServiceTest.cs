using Xunit;
using Moq;
using Domain.Core.Entities;
using Domain.Core.Entities.Backlog;
using Domain.Core.Entities.Sprint;
using Domain.Services.Patterns.Observer;
using Domain.Services.Patterns.State.Sprint;
using Domain.Services.Repositories;
using Domain.Services.Services;
using Domain.Services.Patterns.Factory;
using Application.Services.Services;
using System;
using Domain.Services.Patterns.Factory.Factory_Interfaces;
using Domain.Services.Patterns.State.ItemStates;

namespace Tests
{
    public class SprintServiceTests
    {
        private readonly SprintService _sprintService;
        private readonly Mock<ISprintRepository> _sprintRepositoryMock;
        private readonly Mock<IDevPipeService> _devPipeServiceMock;
        private readonly Mock<IReportService> _reportServiceMock;

        public SprintServiceTests()
        {
            _sprintRepositoryMock = new Mock<ISprintRepository>();
            _devPipeServiceMock = new Mock<IDevPipeService>();
            _reportServiceMock = new Mock<IReportService>();
            _sprintService = new SprintService(_sprintRepositoryMock.Object, _devPipeServiceMock.Object, _reportServiceMock.Object);
        }

        [Fact]
        public void NewSprint_ShouldCreateSprint()
        {
            // Arrange
            var sprintFactoryMock = new Mock<ISprintFactory>();
            var user = new User();
            var project = new Project("Test Project", user);
            var goal = "New Sprint Goal";
            var subject = new Mock<ISubject>();
            var sprintMock = new Mock<ISprint>();

            sprintFactoryMock.Setup(f => f.CreateSprint(goal, It.IsAny<InDevelopment>(), subject.Object, project)).Returns(sprintMock.Object);

            // Act
            var result = _sprintService.NewSprint(sprintFactoryMock.Object, project, goal, subject.Object);

            // Assert
            _sprintRepositoryMock.Verify(r => r.Create(result), Times.Once);
            Assert.Equal(sprintMock.Object, result);
        }

        [Fact]
        public void AddDeveloper_ShouldAddDeveloper_WhenInDevelopment()
        {
            // Arrange
            var sprintMock = new Mock<ISprint>();
            var user = new User();
            var inDevelopmentState = new InDevelopment();
            var users = new List<User>();

            sprintMock.Setup(s => s.State.GetState()).Returns(inDevelopmentState);
            sprintMock.Setup(s => s.Users).Returns(users);

            // Act
            _sprintService.AddDeveloper(user, sprintMock.Object);

            // Assert
            Assert.Contains(user, sprintMock.Object.Users);
        }


        [Fact]
        public void AddItems_ShouldAddItem_WhenInDevelopment()
        {
            // Arrange
            var sprintMock = new Mock<ISprint>();
            var item = new Item(new ToDo());
            var inDevelopmentState = new InDevelopment();
            var backlog = new List<IBacklog>();

            sprintMock.Setup(s => s.State.GetState()).Returns(inDevelopmentState);
            sprintMock.Setup(s => s.Backlogs).Returns(backlog);

            // Act
            _sprintService.AddItems(sprintMock.Object, item);

            // Assert
            Assert.Contains(item, sprintMock.Object.Backlogs);
        }


        [Fact]
        public void AddScrummaster_ShouldAddScrummaster_WhenInDevelopment()
        {
            // Arrange
            var sprintMock = new Mock<ISprint>();
            var user = new User();
            var inDevelopmentState = new InDevelopment();

            sprintMock.Setup(s => s.State.GetState()).Returns(inDevelopmentState);
            sprintMock.SetupProperty(s => s.Scrummaster);

            // Act
            _sprintService.AddScrummaster(user, sprintMock.Object);

            // Assert
            Assert.Equal(user, sprintMock.Object.Scrummaster);
        }

        [Fact]
        public void FinishSprint_ShouldMoveToNextState_WhenInDevelopment()
        {
            // Arrange
            var sprintMock = new Mock<ISprint>();
            var inDevelopmentState = new InDevelopment();

            sprintMock.Setup(s => s.State.GetState()).Returns(inDevelopmentState);

            // Act
            _sprintService.FinishSprint(sprintMock.Object);

            // Assert
            sprintMock.Verify(s => s.State.NextState(), Times.Once);
        }

        [Fact]
        public void AddDevPipe_ShouldCreateDevPipe_WhenSprintIsFinished()
        {
            // Arrange
            var sprintMock = new Mock<ISprint>();
            var finishedState = new Finished(sprintMock.Object);
            var scrummaster = new User();
            var productOwner = new User();
            var project = new Project("Test Project", productOwner);

            var subjectMock = new Mock<ISubject>();
            var devPipe = new DevPipe(scrummaster, sprintMock.Object, subjectMock.Object); // Create a concrete DevPipe

            sprintMock.Setup(s => s.State.GetState()).Returns(finishedState);
            sprintMock.Setup(s => s.Scrummaster).Returns(scrummaster);
            sprintMock.Setup(s => s.Project).Returns(project);
            sprintMock.Setup(s => s.DevPipe).Returns((DevPipe)null);

            // Setup DevPipe to return a mocked Subject
            devPipe.Subject = subjectMock.Object;

            _devPipeServiceMock.Setup(d => d.NewDevPipe(sprintMock.Object)).Returns(devPipe);

            // Act
            _sprintService.AddDevPipe(sprintMock.Object);

            // Assert
            sprintMock.VerifySet(s => s.DevPipe = devPipe, Times.Once);
            subjectMock.Verify(s => s.Subscribe("Scrummaster", It.IsAny<IObserver>()), Times.Once);
            subjectMock.Verify(s => s.Subscribe("ProductOwner", It.IsAny<IObserver>()), Times.Once);
        }


    }
}
