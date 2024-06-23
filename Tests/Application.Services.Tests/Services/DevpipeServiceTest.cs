using Xunit;
using Moq;
using Domain.Core.Entities.Sprint;
using Domain.Core.Entities;
using Domain.Services.Patterns.Observer;
using Domain.Services.Repositories;
using Application.Services.Services;
using System;
using Domain.Services.Patterns.State.Sprint;

namespace Tests.Application.Services.Tests.Services
{
    [Collection("DevPipe Service Tests")]
    [CollectionDefinition("DevPipe Service Tests", DisableParallelization = true)]
    public class DevPipeServiceTests
    {
        private readonly Mock<IDevPipeRepository> _repositoryMock;
        private readonly DevPipeService _devPipeService;
        private readonly Mock<ISubject> _mockSubject;


        public DevPipeServiceTests()
        {
            _repositoryMock = new Mock<IDevPipeRepository>();
            _devPipeService = new DevPipeService(_repositoryMock.Object);
            _mockSubject = new Mock<ISubject>();

        }

        [Fact]
        public void NewDevPipe_ShouldCreateDevPipe_WhenSprintIsValid()
        {
            // Arrange
            var sprintMock = new Mock<ISprint>();
            var scrummaster = new User();
            sprintMock.Setup(s => s.DevPipe).Returns((DevPipe)null);
            sprintMock.Setup(s => s.Scrummaster).Returns(scrummaster);

            // Act
            var result = _devPipeService.NewDevPipe(sprintMock.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(scrummaster, result.Scrummaster);
            Assert.Equal(sprintMock.Object, result.Sprint);
            Assert.NotNull(result.Subject);
            _repositoryMock.Verify(r => r.Create(It.IsAny<DevPipe>()), Times.Once);
        }

        [Fact]
        public void NewDevPipe_ShouldThrowException_WhenDevPipeAlreadyExists()
        {
            // Arrange
            var sprintMock = new Mock<ISprint>();
            var subjectMock = new Mock<ISubject>();
            sprintMock.Setup(s => s.DevPipe).Returns(new DevPipe(new User(), sprintMock.Object, subjectMock.Object));

            // Act & Assert
            Assert.Throws<Exception>(() => _devPipeService.NewDevPipe(sprintMock.Object));
        }

        [Fact]
        public void NewDevPipe_ShouldThrowException_WhenScrummasterIsNull()
        {
            // Arrange
            var sprintMock = new Mock<ISprint>();
            sprintMock.Setup(s => s.Scrummaster).Returns((User)null);

            // Act & Assert
            Assert.Throws<Exception>(() => _devPipeService.NewDevPipe(sprintMock.Object));
        }

        [Fact]
        public void Start_ShouldPrintStartedMessage_WhenUserIsScrummaster()
        {
            // Arrange
            var sprintMock = new Mock<ISprint>();
            var scrummaster = new User();
            var devPipe = new DevPipe(scrummaster, sprintMock.Object, new Subject());
            sprintMock.Setup(s => s.Scrummaster).Returns(scrummaster);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _devPipeService.Start(devPipe, scrummaster);

                // Assert
                Assert.Equal("Started development pipeling", sw.ToString().Trim());
                sw.Close();
            }
        }

        [Fact]
        public void Start_ShouldPrintErrorMessage_WhenUserIsNotScrummaster()
        {
            // Arrange
            var sprintMock = new Mock<ISprint>();
            var scrummaster = new User();
            var anotherUser = new User();
            var devPipe = new DevPipe(scrummaster, sprintMock.Object, new Subject());
            sprintMock.Setup(s => s.Scrummaster).Returns(scrummaster);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _devPipeService.Start(devPipe, anotherUser);

                // Assert
                Assert.Equal("Only the scrummaster can start the Development Pipeline", sw.ToString().Trim());
                sw.Close();
            }
        }

        [Fact]
        public void Observe_ShouldSubscribeObserver_WhenSubjectIsNotNull()
        {
            // Arrange
            var observerMock = new Mock<IObserver>();
            var devPipe = new DevPipe(new User(), new Mock<ISprint>().Object, new Subject());
            var subjectMock = new Mock<ISubject>();
            devPipe.Subject = subjectMock.Object;

            // Act
            _devPipeService.Observe(observerMock.Object, devPipe);

            // Assert
            subjectMock.Verify(s => s.Subscribe("Test Failed", observerMock.Object), Times.Once);
        }

        [Fact]
        public void Observe_ShouldPrintErrorMessage_WhenSubjectIsNull()
        {
            // Arrange
            var observerMock = new Mock<IObserver>();
            var devPipe = new DevPipe(new User(), new Mock<ISprint>().Object, null);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _devPipeService.Observe(observerMock.Object, devPipe);

                // Assert
                Assert.Equal("Something's Wrong, I can feel it", sw.ToString().Trim());
                sw.Close();
            }
        }

        // Similar tests for Restart, Cancel, Failure, End, Deploy, and PubliciseTests methods...


            [Fact]
            public void Restart_ShouldPrintRestarted_WhenUserIsScrummaster()
            {
                // Arrange
                var user = new User { Name = "John Doe" };
                var sprint = new Mock<ISprint>();
                sprint.Setup(s => s.Scrummaster).Returns(user);
                var devPipe = new DevPipe(user, sprint.Object, _mockSubject.Object);

                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);

                    // Act
                    _devPipeService.Restart(devPipe, user);

                    // Assert
                    var expectedMessage = "Restarted development pipeling";
                    Assert.Equal(expectedMessage, sw.ToString().Trim());
                    sw.Close();
            }
            }

            [Fact]
            public void Restart_ShouldPrintError_WhenUserIsNotScrummaster()
            {
                // Arrange
                var user = new User { Name = "John Doe" };
                var anotherUser = new User { Name = "Jane Doe" };
                var sprint = new Mock<ISprint>();
                sprint.Setup(s => s.Scrummaster).Returns(user);
                var devPipe = new DevPipe(user, sprint.Object, _mockSubject.Object);

                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);

                    // Act
                    _devPipeService.Restart(devPipe, anotherUser);

                    // Assert
                    var expectedMessage = "Only the scrummaster can restart the Development Pipeline";
                    Assert.Equal(expectedMessage, sw.ToString().Trim());
                    sw.Close();
            }
            
        }

            [Fact]
            public void Cancel_ShouldRemoveDevPipe_WhenUserIsScrummaster()
            {
                // Arrange
                var user = new User { Name = "John Doe" };
                var sprint = new Mock<ISprint>();
                sprint.Setup(s => s.Scrummaster).Returns(user);
                var devPipe = new DevPipe(user, sprint.Object, _mockSubject.Object);

                // Act
                _devPipeService.Cancel(devPipe, user);

                // Assert
                sprint.VerifySet(s => s.DevPipe = null, Times.Once);
            }

            [Fact]
            public void Cancel_ShouldPrintError_WhenUserIsNotScrummaster()
            {
                // Arrange
                var user = new User { Name = "John Doe" };
                var anotherUser = new User { Name = "Jane Doe" };
                var sprint = new Mock<ISprint>();
                sprint.Setup(s => s.Scrummaster).Returns(user);
                var devPipe = new DevPipe(user, sprint.Object, _mockSubject.Object);

                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);

                    // Act
                    _devPipeService.Cancel(devPipe, anotherUser);

                    // Assert
                    var expectedMessage = "Only the scrummaster can cancel the Development Pipeline";
                    Assert.Equal(expectedMessage, sw.ToString().Trim());
                    sw.Close();
                }
            }

            [Fact]
            public void Failure_ShouldNotify_WhenSprintIsFinished()
            {
                // Arrange
                var user = new User { Name = "John Doe" };
                var sprint = new Mock<ISprint>();
                sprint.Setup(s => s.State.GetState()).Returns(new Finished(sprint.Object));
                var devPipe = new DevPipe(user, sprint.Object, _mockSubject.Object);

                // Act
                _devPipeService.Failure(devPipe);

                // Assert
                _mockSubject.Verify(s => s.Notify("Scrummaster", "Tests failed"), Times.Once);
            }

            [Fact]
            public void Failure_ShouldPrintError_WhenSprintIsNotFinished()
            {
                // Arrange
                var user = new User { Name = "John Doe" };
                var sprint = new Mock<ISprint>();
                sprint.Setup(s => s.State.GetState()).Returns(new InDevelopment());
                var devPipe = new DevPipe(user, sprint.Object, _mockSubject.Object);

                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);

                    // Act
                    _devPipeService.Failure(devPipe);

                    // Assert
                    var expectedMessage = "Release sprint is not valid";
                    Assert.Equal(expectedMessage, sw.ToString().Trim());
                    sw.Close();
                }
            }

            [Fact]
            public void End_ShouldNotify_WhenSprintIsFinished()
            {
                // Arrange
                var user = new User { Name = "John Doe" };
                var sprint = new Mock<ISprint>();
                sprint.Setup(s => s.State.GetState()).Returns(new Finished(sprint.Object));
                var devPipe = new DevPipe(user, sprint.Object, _mockSubject.Object);

                // Act
                _devPipeService.End(devPipe);

                // Assert
                sprint.Verify(s => s.State.NextState(), Times.Once);
                _mockSubject.Verify(s => s.Notify("Scrummaster", "Tests succesful"), Times.Once);
                _mockSubject.Verify(s => s.Notify("ProductOwner", "Tests succesful"), Times.Once);
            }

            [Fact]
            public void End_ShouldPrintError_WhenSprintIsNotFinished()
            {
                // Arrange
                var user = new User { Name = "John Doe" };
                var sprint = new Mock<ISprint>();
                sprint.Setup(s => s.State.GetState()).Returns(new InDevelopment());
                var devPipe = new DevPipe(user, sprint.Object, _mockSubject.Object);

                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);

                    // Act
                    _devPipeService.End(devPipe);

                    // Assert
                    var expectedMessage = "Release sprint is not valid";
                    Assert.Equal(expectedMessage, sw.ToString().Trim());
                    sw.Close();
                }
            }

            [Fact]
            public void Deploy_ShouldPrintDeployed_WhenUserIsScrummaster()
            {
                // Arrange
                var user = new User { Name = "John Doe" };
                var sprint = new Mock<ISprint>();
                sprint.Setup(s => s.Scrummaster).Returns(user);
                var devPipe = new DevPipe(user, sprint.Object, _mockSubject.Object);

                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);

                    // Act
                    _devPipeService.Deploy(devPipe, user);

                    // Assert
                    var expectedMessage = "Sprint Deployed";
                    Assert.Equal(expectedMessage, sw.ToString().Trim());
                    sw.Close();
                }
            }

            [Fact]
            public void Deploy_ShouldPrintError_WhenUserIsNotScrummaster()
            {
                // Arrange
                var user = new User { Name = "John Doe" };
                var anotherUser = new User { Name = "Jane Doe" };
                var sprint = new Mock<ISprint>();
                sprint.Setup(s => s.Scrummaster).Returns(user);
                var devPipe = new DevPipe(user, sprint.Object, _mockSubject.Object);

                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);

                    // Act
                    _devPipeService.Deploy(devPipe, anotherUser);

                    // Assert
                    var expectedMessage = "Only the scrummaster can cancel the Development Pipeline";
                    Assert.Equal(expectedMessage, sw.ToString().Trim());
                    sw.Close();

            }
        }

            [Fact]
            public void PubliciseTests_ShouldPrintPublished_WhenUserIsScrummaster()
            {
                // Arrange
                var user = new User { Name = "John Doe" };
                var sprint = new Mock<ISprint>();
                sprint.Setup(s => s.Scrummaster).Returns(user);
                var devPipe = new DevPipe(user, sprint.Object, _mockSubject.Object);

                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);

                    // Act
                    _devPipeService.PubliciseTests(devPipe, user);

                    // Assert
                    var expectedMessage = "Tests Published";
                    Assert.Equal(expectedMessage, sw.ToString().Trim());
                    sw.Close();
            }
        }

            [Fact]
            public void PubliciseTests_ShouldPrintError_WhenUserIsNotScrummaster()
            {
                // Arrange
                var user = new User { Name = "John Doe" };
                var anotherUser = new User { Name = "Jane Doe" };
                var sprint = new Mock<ISprint>();
                sprint.Setup(s => s.Scrummaster).Returns(user);
                var devPipe = new DevPipe(user, sprint.Object, _mockSubject.Object);

                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);

                    // Act
                    _devPipeService.PubliciseTests(devPipe, anotherUser);

                    // Assert
                    var expectedMessage = "Only the scrummaster can cancel the Development Pipeline";
                    Assert.Equal(expectedMessage, sw.ToString().Trim());
                    sw.Close();
                }
            }
        }
    }



