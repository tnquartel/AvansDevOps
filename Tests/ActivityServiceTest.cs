using Xunit;
using Moq;
using Domain.Core.Entities.Backlog;
using Domain.Core.Entities;
using Domain.Services.Services;
using Application.Services.Services;
using Domain.Services.Patterns.State.ItemStates;

namespace Tests
{
    public class ActivityServiceTests
    {
        private readonly ActivityService _activityService;
        private readonly Mock<IUserService> _userServiceMock;

        public ActivityServiceTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _activityService = new ActivityService(_userServiceMock.Object);
        }

        [Fact]
        public void AssignDev_ShouldCallCoupleToFirstAvailable()
        {
            // Arrange
            var activity = new ActivityItem(new Item(new ToDo()));
            var user = new User();

            // Act
            _activityService.AssignDev(activity, user);

            // Assert
            _userServiceMock.Verify(s => s.CoupleToFirstAvailable(activity, user), Times.Once);
        }

        [Fact]
        public void NewThread_ShouldCreateNewThread_WhenThreadIsNull()
        {
            // Arrange
            var activity = new ActivityItem(new Item(new ToDo()));

            // Act
            _activityService.NewThread(activity);

            // Assert
            Assert.NotNull(activity.Thread);
            Assert.Equal(activity, activity.Thread.ParentActivityItem);
        }

        [Fact]
        public void NewThread_ShouldNotCreateNewThread_WhenThreadIsNotNull()
        {
            // Arrange
            var activity = new ActivityItem(new Item(new ToDo()))
            {
                Thread = new MessageThread()
            };

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _activityService.NewThread(activity);

                // Assert
                Assert.Equal("This activity already contains a thread.", sw.ToString().Trim());
                Assert.NotNull(activity.Thread); // Ensure thread is still not null
            }
        }
    }
}
