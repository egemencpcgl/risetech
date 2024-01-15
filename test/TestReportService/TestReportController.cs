using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using ReportManagementService.Controllers;
using ReportManagementService.Dtos;
using ReportManagementService.Interfaces;
using ReportManagementService.Models;
using ReportManagementService.Responses;

namespace TestReportService
{
    public class TestReportController : IClassFixture<BaseFixture>
    {

        BaseFixture baseFixture;

        public TestReportController(BaseFixture _baseFixture)
        {
            baseFixture = _baseFixture;
        }

        [Fact]
        public async Task GetAllReport_ShouldReturnSuccessResponseAsync()
        {
            //Arrange
            List<Report> reportlist = new List<Report>
            {
                new Report
                {
                    CreatedTime = DateTime.UtcNow,
                    ReportName = "Test",
                    ReportStatus="Tamamlandı"
                },
                new Report
                {
                    CreatedTime = DateTime.UtcNow,
                    ReportName = "Test2",
                    ReportStatus="Tamamlandı"
                }
            };

            var successReturn = Response<List<Report>>.Success(reportlist, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };

            //Act
            A.CallTo(() => baseFixture.dataStore.GetAllReadyReports()).Returns(successReturn);

            var actionResult = await baseFixture.reportController.GetAllReport();

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult).StatusCode);
        }



        [Fact]
        public async Task GetReportDetailById_ShouldReturnSuccessResponseAsync()
        {
            //Arrange

            Report report = new Report
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTime.UtcNow,
                ReportName = "Test",
                ReportStatus = "Tamamlandı"
            };
            List<ReportDetailDto> reportDetailDtoList = new List<ReportDetailDto>()
            {
                new ReportDetailDto{City="Eskişehir",PeopleCount=10,PhoneCount=10},
                new ReportDetailDto{City="Çanakkale",PeopleCount=10,PhoneCount=10}
            };

            var successReturn = Response<List<ReportDetailDto>>.Success(reportDetailDtoList, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };

            //Act
            A.CallTo(() => baseFixture.dataStore.GetReadyReportDetail(report.Id)).Returns(successReturn);

            var actionResult = await baseFixture.reportController.GetReportDetailById(report.Id);

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult).StatusCode);
        }
        [Fact]
        public async Task GetStatisticsByLocation_ShouldReturnSuccessResponseAsync()
        {
            // Arrange

            ReportDto reportdto = new ReportDto
            {
                CreatedTime = DateTime.UtcNow,
                ReportName = "Test2",
                ReportStatus = "Tamamlandı"
            };
            string location = "Eskişehir";
            var successReturn = Response<ReportDto>.Success(reportdto, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };

            //Act
            A.CallTo(() => baseFixture.dataStore.GetStatisticsByLocation(location)).Returns(successReturn);

            var actionResult = await baseFixture.reportController.GetStatisticsByLocation(location);

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult).StatusCode);
        }
        [Fact]
        public async Task GetStatisticsAllLocation_ShouldReturnSuccessResponseAsync()
        {
            // Arrange
            List<ReportDto> reportdtoList = new List<ReportDto>
            {
                new ReportDto
                {
                    CreatedTime = DateTime.UtcNow,
                    ReportName = "Test",
                    ReportStatus="Tamamlandı"
                },
                new ReportDto
                {
                    CreatedTime = DateTime.UtcNow,
                    ReportName = "Test2",
                    ReportStatus="Tamamlandı"
                }
            };
            var successReturn = Response<List<ReportDto>>.Success(reportdtoList, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };

            //Act
            A.CallTo(() => baseFixture.dataStore.GetStatisticsAllLocation()).Returns(successReturn);

            var actionResult = await baseFixture.reportController.GetStatisticsAllLocation();

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult).StatusCode);
        }

    }
}