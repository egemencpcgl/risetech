using ReportManagementService.Dtos;
using ReportManagementService.Models;
using ReportManagementService.Responses;

namespace ReportManagementService.Interfaces
{
    public interface IReportService
    {
        Report CreatReport(ReportDto reportDto);
        Task<Response<List<ReportDto>>> GetStatisticsAllLocation();
        Task<Response<ReportDto>> GetStatisticsByLocation(string location);
        Task<Response<List<ReportDto>>> GetAllReadyReports();
        Task<Response<List<ReportDetailDto>>> GetReadyReportDetail(Guid reportID);
    }
}
