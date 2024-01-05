using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using ReportManagementService.Controllers;
using ReportManagementService.Dtos;
using ReportManagementService.Interfaces;
using ReportManagementService.Responses;

namespace TestReportService
{
    public class TestReportController
    {
        [Fact]
        public void GetAllReport_ShouldReturnSuccessResponse()
        {
            //Arrange

            List<ReportDto> reportdto = new List<ReportDto>
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

            var successReturn = Response<List<ReportDto>>.Success(reportdto, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };
            var dataStore = A.Fake<IReportService>();

            A.CallTo(() => dataStore.GetAllReadyReports()).Returns(successReturn);
            var controller = new ReportController(dataStore);

            //Act
            var actionResult = controller.GetAllReport();

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult.Result).StatusCode);
        }
        [Fact]
        public void GetReportDetailById_ShouldReturnSuccessResponse()
        {
            //Arrange
            List<ReportDetailDto> reportDetailDtos = new List<ReportDetailDto>()
            {
                new ReportDetailDto{City="Eskişehir",PeopleCount=10,PhoneCount=10},
                new ReportDetailDto{City="Çanakkale",PeopleCount=10,PhoneCount=10}
            };

            ReportDto reportdto = new ReportDto
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTime.UtcNow,
                ReportName = "Test",
                ReportStatus = "Tamamlandı"
            };

            var successReturn = Response<List<ReportDetailDto>>.Success(reportDetailDtos, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };
            var dataStore = A.Fake<IReportService>();

            A.CallTo(() => dataStore.GetReadyReportDetail(reportdto.Id)).Returns(successReturn);
            var controller = new ReportController(dataStore);

            //Act
            var actionResult = controller.GetReportDetailById(reportdto.Id);

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult.Result).StatusCode);
        }
        [Fact]
        public void GetStatisticsByLocation_ShouldReturnSuccessResponse()
        {
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
            var dataStore = A.Fake<IReportService>();

            A.CallTo(() => dataStore.GetStatisticsByLocation(location)).Returns(successReturn);
            var controller = new ReportController(dataStore);

            //Act
            var actionResult = controller.GetStatisticsByLocation(location);

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult.Result).StatusCode);
        }
        [Fact]
        public void GetStatisticsAllLocation_ShouldReturnSuccessResponse()
        {
            List<ReportDto> reportdto = new List<ReportDto>
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

            var successReturn = Response<List<ReportDto>>.Success(reportdto, 200);
            var expected = new ObjectResult(successReturn)
            {
                StatusCode = successReturn.StatusCode
            };
            var dataStore = A.Fake<IReportService>();

            A.CallTo(() => dataStore.GetStatisticsAllLocation()).Returns(successReturn);
            var controller = new ReportController(dataStore);

            //Act
            var actionResult = controller.GetStatisticsAllLocation();

            //Assert
            Assert.Equal(expected.StatusCode, ((ObjectResult)actionResult.Result).StatusCode);
        }

    }
}