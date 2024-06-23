using Xunit;
using Moq;
using System;
using System.IO;
using Domain.Core.Entities;
using Domain.Core.Entities.Backlog;
using Domain.Services.Patterns.State.ItemStates;
using Application.Services.Services;

namespace Tests.Application.Services.Tests.Services
{
    public class UserServiceTests
    {
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userService = new UserService();
        }

        [Fact]
        public void SendMessage_ShouldPrintMessageWithUserName()
        {
            // Arrange
            var user = new User { Name = "John Doe" };
            var message = "Hello, world!";

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _userService.SendMessage(user, message);

                // Assert
                var expectedMessage = "John Doe : Hello, world!";
                Assert.Equal(expectedMessage, sw.ToString().Trim());
            }
        }

        [Fact]
        public void CoupleToFirstAvailable_Item_ShouldCoupleUserToItem_WhenNotCoupled()
        {
            // Arrange
            var user = new User { Name = "John Doe" };
            var itemStateMock = new Mock<IStateItem>();
            var item = new Item(itemStateMock.Object);
            user.CoupledItem = null;
            user.CoupledActivityItem = null;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _userService.CoupleToFirstAvailable(item, user);

                // Assert
                Assert.Equal(item, user.CoupledItem);
                Assert.Null(user.CoupledActivityItem);
                var expectedMessage = $"{user.Name} is now coupled to item: {item.Id}.";
                Assert.Equal(expectedMessage, sw.ToString().Trim());
            }
        }

        [Fact]
        public void CoupleToFirstAvailable_Activity_ShouldCoupleUserToActivity_WhenNotCoupled()
        {
            // Arrange
            var user = new User { Name = "John Doe" };
            var activity = new ActivityItem(new Item(new Mock<IStateItem>().Object));
            user.CoupledItem = null;
            user.CoupledActivityItem = null;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _userService.CoupleToFirstAvailable(activity, user);

                // Assert
                Assert.Equal(activity, user.CoupledActivityItem);
                Assert.Null(user.CoupledItem);
                var expectedMessage = $"{user.Name} is now coupled to activity: {activity.Id}.";
                Assert.Equal(expectedMessage, sw.ToString().Trim());
            }
        }

        [Fact]
        public void IsCoupled_ShouldReturnFalse_WhenUserIsNotCoupled()
        {
            // Arrange
            var user = new User();
            user.CoupledItem = null;
            user.CoupledActivityItem = null;

            // Act
            var result = _userService.IsCoupled(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsCoupled_ShouldReturnFalse_WhenUserCoupledItemIsDone()
        {
            // Arrange
            var user = new User();
            var item = new Item(new ToDo());
            var done = new Done(item);
            item.State = done;
            user.CoupledItem = item;
            user.CoupledActivityItem = null;

            // Act
            var result = _userService.IsCoupled(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsCoupled_ShouldReturnFalse_WhenUserCoupledActivityItemParentIsDone()
        {
            // Arrange
            var user = new User();
            var item = new Item(new ToDo());
            var done = new Done(item);
            item.State = done;
            var activity = new ActivityItem(item);
            user.CoupledActivityItem = activity;
            user.CoupledItem = null;

            // Act
            var result = _userService.IsCoupled(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsCoupled_ShouldReturnTrue_WhenUserIsCoupledToActiveItemOrActivity()
        {
            // Arrange
            var user = new User();
            var item = new Item(new ToDo());
            user.CoupledItem = item;
            user.CoupledActivityItem = null;

            // Act
            var result = _userService.IsCoupled(user);

            // Assert
            Assert.True(result);
        }
    }
}
