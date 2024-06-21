using Domain.Services.Repositories;
using Domain.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Services;
using Domain.Core.Entities;
using Domain.Core.Entities.Sprint;
using System;

using Xunit;
using Domain.Services.Patterns.Observer;
using Domain.Services.Patterns.State.Sprint;

namespace Tests
{
    public class SprintServiceTest
    {
        [Fact]
        public void NewSprint_WithValidType_CreatesSprint()
        {
            // Arrange
            var sprintRepositoryMock = new Mock<ISprintRepository>();
            var devPipeServiceMock = new Mock<IDevPipeService>();
            var reportServiceMock = new Mock<IReportService>();
            var sprintService = new SprintService(sprintRepositoryMock.Object, devPipeServiceMock.Object, reportServiceMock.Object);
            var project = new Project("TestProject", new User());

            // Act
            var sprint = sprintService.NewSprint("release", project);

            // Assert
            Assert.NotNull(sprint);
        }

        [Fact]
        public void Observe_AddsObserverToSprint()
        {
            // Arrange
            var sprint = new ReleaseSprint("", new InDevelopment(), new Subject(), new Project("TestProject", new User()));
            var observerMock = new Mock<IObserver>();
            var sprintRepositoryMock = new Mock<ISprintRepository>();
            var devPipeServiceMock = new Mock<IDevPipeService>();
            var reportServiceMock = new Mock<IReportService>();
            var sprintService = new SprintService(sprintRepositoryMock.Object, devPipeServiceMock.Object, reportServiceMock.Object);

            // Act
            sprintService.Observe(observerMock.Object, sprint);

            // Assert
            Assert.Contains(observerMock.Object, sprint.Subject.Observers);
        }

    }
}
