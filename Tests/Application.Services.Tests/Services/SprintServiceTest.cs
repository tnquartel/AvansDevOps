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

namespace Tests.Application.Services.Tests.Services
{
    public class SprintServiceTests
    {


        private readonly SprintService _sprintService;
        private readonly Mock<IReportService> _mockReportService;
        private readonly Mock<IObserver> _mockObserver;
        private readonly Mock<ISprint> _mockSprint;
        private readonly Mock<ISubject> _mockSubject;
        private readonly Mock<ISprintState> _mockStateSprint;

        private readonly Mock<ISprintRepository> _sprintRepositoryMock;
        private readonly Mock<IDevPipeService> _devPipeServiceMock;
        private readonly Mock<IReportService> _reportServiceMock;

        public SprintServiceTests()
        {
            _mockReportService = new Mock<IReportService>();
            _mockObserver = new Mock<IObserver>();
            _mockSprint = new Mock<ISprint>();
            _mockSubject = new Mock<ISubject>();
            _mockStateSprint = new Mock<ISprintState>();

            _mockSprint.SetupGet(s => s.Subject).Returns(_mockSubject.Object);
            _mockSprint.SetupGet(s => s.State).Returns(_mockStateSprint.Object);
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


        [Fact]
        public void Observe_ShouldSubscribeObserverToSprint()
        {
            // Act
            _sprintService.Observe(_mockObserver.Object, _mockSprint.Object);

            // Assert
            _mockSubject.Verify(s => s.Subscribe("Test Failed", _mockObserver.Object), Times.Once);
        }

        [Fact]
        public void TestFailure_ShouldNotifySprintObservers()
        {
            // Act
            _sprintService.TestFailure(_mockSprint.Object);

            // Assert
            _mockSubject.Verify(s => s.Notify("Tests Failed", "The Tests have failed"), Times.Once);
        }

        [Fact]
        public void NextState_ShouldTransitionToNextState()
        {
            // Act
            _sprintService.NextState(_mockSprint.Object);

            // Assert
            _mockStateSprint.Verify(s => s.NextState(), Times.Once);
        }


        [Fact]
        public void GenerateReport_ShouldPrintError_WhenReportAlreadyExists()
        {
            // Arrange
            var report = new Report(_mockSprint.Object);
            _mockSprint.SetupGet(s => s.Report).Returns(report);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _sprintService.GenerateReport(_mockSprint.Object);

                // Assert
                var expectedMessage = "Sprint has already been reported upon";
                Assert.Equal(expectedMessage, sw.ToString().Trim());
            }
        }

        //new

        //[Fact]
        //public void Observe_ShouldSubscribeObserverToSprint()
        //{
        //    // Act
        //    _sprintService.Observe(_mockObserver.Object, _mockSprint.Object);

        //    // Assert
        //    _mockSubject.Verify(s => s.Subscribe("Test Failed", _mockObserver.Object), Times.Once);
        //}

        //[Fact]
        //public void TestFailure_ShouldNotifySprintObservers()
        //{
        //    // Act
        //    _sprintService.TestFailure(_mockSprint.Object);

        //    // Assert
        //    _mockSubject.Verify(s => s.Notify("Tests Failed", "The Tests have failed"), Times.Once);
        //}

        //[Fact]
        //public void NextState_ShouldTransitionToNextState()
        //{
        //    // Act
        //    _sprintService.NextState(_mockSprint.Object);

        //    // Assert
        //    _mockStateSprint.Verify(s => s.NextState(), Times.Once);
        //}

        [Fact]
        public void AddReview_ShouldSetReviewAndTransitionState_WhenSprintIsFinished()
        {
            // Arrange
            var review = new Review();
            var sprint = new PartialSprint { State = _mockStateSprint.Object };
            _mockStateSprint.Setup(s => s.GetState()).Returns(new Finished(sprint));

            // Act
            _sprintService.AddReview(review, sprint);

            // Assert
            Assert.Equal(review, sprint.Review);
            _mockStateSprint.Verify(s => s.NextState(), Times.Once);
        }

        [Fact]
        public void AddReview_ShouldPrintError_WhenSprintIsNotFinished()
        {
            // Arrange
            var review = new Review();
            var stateMock = new InDevelopment();
            var sprint = new PartialSprint("test goal", stateMock, new Mock<ISubject>().Object, new Project("test project", new User()));

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _sprintService.AddReview(review, sprint);

                // Assert
                var expectedMessage = "Sprint is not Finished";
                Assert.Equal(expectedMessage, sw.ToString().Trim());
            }
        }

        [Fact]
        public void AddReview_ShouldPrintError_WhenReviewAlreadyAdded()
        {
            // Arrange
            var review = new Review();
            var sprint = new PartialSprint { State = _mockStateSprint.Object, Review = review };
            _mockStateSprint.Setup(s => s.GetState()).Returns(new Finished(sprint));

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _sprintService.AddReview(review, sprint);

                // Assert
                var expectedMessage = "Already added this Report";
                Assert.Equal(expectedMessage, sw.ToString().Trim());
            }
        }


        [Fact]
        public void GenerateReport_ShouldCreateNewReport_WhenNoneExists()
        {
            // Arrange
            _mockSprint.SetupGet(s => s.Report).Returns((Report)null);

            // Act
            _sprintService.GenerateReport(_mockSprint.Object);

            // Assert
            //_mockReportService.Verify(r => r.NewReport(_mockSprint.Object), Times.Once);
            _mockSprint.VerifySet(s => s.Report = It.IsAny<Report>(), Times.Once);
        }

        //[Fact]
        //public void GenerateReport_ShouldPrintError_WhenReportAlreadyExists()
        //{
        //    // Arrange
        //    var report = new Report(_mockSprint.Object);
        //    _mockSprint.SetupGet(s => s.Report).Returns(report);

        //    using (var sw = new StringWriter())
        //    {
        //        Console.SetOut(sw);

        //        // Act
        //        _sprintService.GenerateReport(_mockSprint.Object);

        //        // Assert
        //        var expectedMessage = "Sprint has already been reported upon";
        //        Assert.Equal(expectedMessage, sw.ToString().Trim());
        //    }
        //}
    }
}
    




