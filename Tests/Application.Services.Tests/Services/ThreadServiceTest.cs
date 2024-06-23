using Xunit;
using Moq;
using Domain.Core.Entities;
using Domain.Services.Patterns.State.ItemStates;
using Application.Services.Services;
using System.Collections.Generic;
using Domain.Core.Entities.Backlog;

namespace Application.Services.Tests
{
    public class ThreadServiceTests
    {
        private readonly ThreadService _threadService;

        public ThreadServiceTests()
        {
            _threadService = new ThreadService();
        }

        [Fact]
        public void NewMessage_ShouldAddMessageAndUser_WhenParentItemNotDone()
        {
            // Arrange
            var message = "Test message";
            var user = new User { Name = "Test User" };
            var parentItem = new Item(new ToDo());
            var messageThread = new MessageThread
            {
                ParentItem = parentItem,
                Messages = new List<Message>(),
                Particapants = new List<User>()
            };

            // Act
            _threadService.NewMessage(message, user, messageThread);

            // Assert
            Assert.Single(messageThread.Messages);
            Assert.Contains(user, messageThread.Particapants);
        }

        [Fact]
        public void NewMessage_ShouldNotAddMessage_WhenParentItemDone()
        {
            // Arrange
            var message = "Test message";
            var user = new User { Name = "Test User" };
            var parentItem = new Item(new ToDo());
            var doneState = new Done(parentItem);
            parentItem.State = doneState;
            var messageThread = new MessageThread
            {
                ParentItem = parentItem,
                Messages = new List<Message>(),
                Particapants = new List<User>()
            };

            // Act
            _threadService.NewMessage(message, user, messageThread);

            // Assert
            Assert.Empty(messageThread.Messages);
            Assert.DoesNotContain(user, messageThread.Particapants);
        }

        [Fact]
        public void NewMessage_ShouldAddMessageAndUser_WhenParentActivityItemNotDone()
        {
            // Arrange
            var message = "Test message";
            var user = new User { Name = "Test User" };
            var parentItem = new Item(new ToDo());
            var parentActivityItem = new ActivityItem(parentItem);
            var messageThread = new MessageThread
            {
                ParentActivityItem = parentActivityItem,
                Messages = new List<Message>(),
                Particapants = new List<User>()
            };

            // Act
            _threadService.NewMessage(message, user, messageThread);

            // Assert
            Assert.Single(messageThread.Messages);
            Assert.Contains(user, messageThread.Particapants);
        }

        [Fact]
        public void NewMessage_ShouldNotAddMessage_WhenParentActivityItemDone()
        {
            // Arrange
            var message = "Test message";
            var user = new User { Name = "Test User" };
            var parentItem = new Item(new ToDo());
            var doneState = new Done(parentItem);
            parentItem.State = doneState;
            var parentActivityItem = new ActivityItem (parentItem);
            var messageThread = new MessageThread
            {
                ParentActivityItem = parentActivityItem,
                Messages = new List<Message>(),
                Particapants = new List<User>()
            };

            // Act
            _threadService.NewMessage(message, user, messageThread);

            // Assert
            Assert.Empty(messageThread.Messages);
            Assert.DoesNotContain(user, messageThread.Particapants);
        }
    }
}
