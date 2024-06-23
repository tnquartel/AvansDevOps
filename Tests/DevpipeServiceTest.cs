using Xunit;
using Moq;
using Domain.Core.Entities.Sprint;
using Domain.Core.Entities;
using Domain.Services.Patterns.Observer;
using Domain.Services.Repositories;
using Application.Services.Services;
using System;

namespace DevPipeServiceTests
{
    public class DevPipeServiceTests
    {
        private readonly Mock<IDevPipeRepository> _repositoryMock;
        private readonly DevPipeService _devPipeService;

        public DevPipeServiceTests()
        {
            _repositoryMock = new Mock<IDevPipeRepository>();
            _devPipeService = new DevPipeService(_repositoryMock.Object);
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

            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _devPipeService.Start(devPipe, scrummaster);

                // Assert
                Assert.Equal("Started development pipeling", sw.ToString().Trim());
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

            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _devPipeService.Start(devPipe, anotherUser);

                // Assert
                Assert.Equal("Only the scrummaster can start the Development Pipeline", sw.ToString().Trim());
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
            }
        }

        // Similar tests for Restart, Cancel, Failure, End, Deploy, and PubliciseTests methods...
    }
}
