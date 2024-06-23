using Xunit;
using Moq;
using Domain.Core.Entities;
using Domain.Core.Entities.Sprint;
using Domain.Services.Services;
using System;
using System.Collections.Generic;
using Application.Services.Services;
using System.Net.WebSockets;

namespace ReportServiceTests
{
    public class ReportServiceTests
    {
        private readonly ReportService _reportService;

        public ReportServiceTests()
        {
            _reportService = new ReportService();
        }

        [Fact]
        public void NewReport_ShouldCreateNewReport()
        {
            // Arrange
            var sprintMock = new Mock<ISprint>();

            // Act
            var result = _reportService.NewReport(sprintMock.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(sprintMock.Object, result.Sprint);
        }

        [Fact]
        public void Decorate_ShouldSetReportHeaderAndFooter()
        {
            // Arrange
            var report = new Report(new Mock<ISprint>().Object);
            var header = "Header";
            var footer = "Footer";

            // Act
            _reportService.Decorate(report, footer, header);

            // Assert
            Assert.Equal(footer, report.Ftr);
            Assert.Equal(header, report.Hdr);
        }

        [Fact]
        public void SetStrategy_ShouldSetReportExportStrategy()
        {
            // Arrange
            var report = new Report(new Mock<ISprint>().Object);
            var exportMock = new Mock<IExport>();

            // Act
            _reportService.SetStrategy(report, exportMock.Object);

            // Assert
            Assert.Equal(exportMock.Object, report.Export);
        }

        [Fact]
        public void Export_ShouldCallExportMethod_WhenExportStrategyIsSet()
        {
            // Arrange

            // sprint setup
            var productOwner = new User() { Name = "productOwner" };
            var scrummaster = new User() { Name = "scrummaster" };
            var stateMock = new Mock<ISprintState>();
            var subjectMock = new Mock<ISubject>();
           
            var testUser_1 = new User() { Name = "test user" };
            var testUser_2 = new User() { Name = "test user" };
            var testUser_3 = new User() { Name = "test user" };
            var testUser_4 = new User() { Name = "test user" };

            var userList = new List<User>();
            userList.Add(testUser_1);
            userList.Add(testUser_2);
            userList.Add(testUser_3);
            userList.Add(testUser_4);
            
            var sprintMock = new PartialSprint("test goal", stateMock.Object, subjectMock.Object, new Project("test sprint", productOwner));
            sprintMock.Scrummaster = scrummaster;
            sprintMock.Users = userList;

            // report setup
            var exportMock = new Mock<IExport>();
            var hdr = "header";
            var ftr = "footer";
            var report = new Report(sprintMock);
            report.Export = exportMock.Object;
            report.Hdr = hdr;
            report.Ftr = ftr;

            // Act
            _reportService.Export(report);

            // Assert
            exportMock.Verify(e => e.Export(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Export_ShouldPrintMessage_WhenExportStrategyIsNotSet()
        {
            // Arrange
            var report = new Report(new Mock<ISprint>().Object);

            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);

                // Act
                _reportService.Export(report);

                // Assert
                Assert.Equal("Please select a export method", sw.ToString().Trim());
            }
        }

        [Fact]
        public void Generate_ShouldReturnFormattedReportString()
        {
            // Arrange
            var sprintMock = new Mock<ISprint>();
            var scrummaster = new User { Name = "Scrummaster" };
            var user1 = new User { Name = "Dev1" };
            var user2 = new User { Name = "Dev2" };
            sprintMock.Setup(s => s.Scrummaster).Returns(scrummaster);
            sprintMock.Setup(s => s.Users).Returns(new List<User> { user1, user2 });

            var report = new Report(sprintMock.Object)
            {
                Hdr = "Header",
                Ftr = "Footer"
            };

            // Act
            var result = _reportService.Generate(report);

            // Assert
            var expected = "Header\nScrummaster : Scrummaster\nDevs : Dev1Dev2Footer";
            Assert.Equal(expected, result);
        }
    }
}
