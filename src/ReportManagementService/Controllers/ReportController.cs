using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportManagementService.Interfaces;

namespace ReportManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        IReportService _reportsService;

        public ReportController(IReportService reportService)
        {
            _reportsService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReport()
        {
            var response = await _reportsService.GetAllReadyReports();
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetReportDetailById(Guid id)
        {
            var response = await _reportsService.GetReadyReportDetail(id);

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        [HttpPost]
        [Route("/getstatisticsByLocation")]
        public async Task<IActionResult> GetStatisticsByLocation(string location)
        {
            var response = await _reportsService.GetStatisticsByLocation(location);

            return new ObjectResult(response)
            { StatusCode = response.StatusCode };
        }

        [HttpPost]
        [Route("/getstatisticsAllLocation")]
        public async Task<IActionResult> GetStatisticsAllLocation()
        {
            var response = await _reportsService.GetStatisticsAllLocation();

            return new ObjectResult(response)
            { StatusCode = response.StatusCode };
        }
    }
}
