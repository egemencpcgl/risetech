using ReportManagementService.Dtos;
using ReportManagementService.Models;
using ReportManagementService.Responses;

namespace ReportManagementService.Interfaces
{
    public interface IReportService
    {
        /// <summary>
        /// Olusturulan rapor eklenir.
        /// </summary>
        /// <param name="reportDto"></param>
        /// <returns></returns>
        Report CreatReport(ReportDto reportDto);
        /// <summary>
        /// Tum sehirlerin istatistiklerini getirir.
        /// </summary>
        /// <returns></returns>
        Task<Response<List<ReportDto>>> GetStatisticsAllLocation();
        /// <summary>
        /// Girilen sehirin istatistiklerini getirir.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        Task<Response<ReportDto>> GetStatisticsByLocation(string location);
        /// <summary>
        /// Tum raporları listeler
        /// </summary>
        /// <returns></returns>
        Task<Response<List<ReportDto>>> GetAllReadyReports();
        /// <summary>
        /// Hazır olan raporlardan Id'si girilen raporun detayı getirilir.
        /// </summary>
        /// <param name="reportID"></param>
        /// <returns></returns>
        Task<Response<List<ReportDetailDto>>> GetReadyReportDetail(Guid reportID);
    }
}
