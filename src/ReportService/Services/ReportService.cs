using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MqttClient;
using MqttClient.Enums;
using MqttClient.MessageModel;
using ReportManagementService.Context;
using ReportManagementService.Dtos;
using ReportManagementService.Interfaces;
using ReportManagementService.Models;
using ReportManagementService.Responses;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text;
using System.Collections;

namespace ReportManagementService.Services
{
    public class ReportService : IReportService
    {

        private readonly IMapper _mapper;
        readonly PgDbContext PgDbContext;

        public ReportService(IMapper mapper, PgDbContext pgDbContext)
        {
            PgDbContext = pgDbContext;
            _mapper = mapper;
        }

        public void InitMqttClient()
        {
            Client.StartClient(System.Environment.GetEnvironmentVariable("BrokerIp"), 5004, "reportClient");

            Client._mqttClient.ApplicationMessageReceivedAsync += _mqttClient_ApplicationMessageReceivedAsync;
            Client.SubscribeTopics(MessageTopic.Response.ToString());
        }
        /// <summary>
        /// Sub olunan topicler yakalanır.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private async Task _mqttClient_ApplicationMessageReceivedAsync(MQTTnet.Client.MqttApplicationMessageReceivedEventArgs arg)
        {
            var payload = arg.ApplicationMessage?.Payload == null ? null : Encoding.UTF8.GetString(arg.ApplicationMessage?.Payload);
            var msgbyte = JsonSerializer.Deserialize<MqttMessage>(payload);
            var msjjson = Encoding.UTF8.GetString(msgbyte.MessageData);

            bool messageiscorrect;

            switch (msgbyte.MessageType)
            {
                case MessageType.StatisticByLocation:
                    var msg1 = JsonSerializer.Deserialize<Tuple<string, int, int>>(msjjson);
                    if (msg1.Item1 == "")
                    {
                        messageiscorrect = false;
                    }
                    else
                    {
                        messageiscorrect = true;
                    }
                    if (await SaveReportDetailAsync(msgbyte.MessageId, msg1))
                    {
                        CheckReportStatusAndUpdate(msgbyte.MessageId, messageiscorrect);
                    }
                    break;
                case MessageType.GetStatisticsAllLocation:
                    var msg2 = JsonSerializer.Deserialize<List<Tuple<string, int, int>>>(msjjson);
                    if (msg2.Count == 0)
                    {
                        messageiscorrect = false;
                    }
                    else
                    {
                        messageiscorrect = true;
                    }
                    if (await SaveReportDetailAsync(msgbyte.MessageId, msg2))
                    {
                        CheckReportStatusAndUpdate(msgbyte.MessageId, messageiscorrect);
                    }
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Rapor sonucu kisi servisinden gelen yanıta gore guncellenir.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="correctcompleted"></param>
        /// <returns></returns>
        bool CheckReportStatusAndUpdate(Guid id, bool correctcompleted)
        {
            try
            {
                var report = PgDbContext.Reports.Where(x => x.Id == id).FirstOrDefault();
                switch (correctcompleted)
                {
                    case false:
                        report.ReportStatus = "Başarısız.";
                        PgDbContext.Reports.Update(report);
                        break;
                    case true:
                        report.ReportStatus = "Tamamlandı";
                        PgDbContext.Reports.Update(report);
                        break;
                }
                PgDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("CHECK REPORT STATUS ERROR: " + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Tamamlanan rapor detayı kaydedilir.
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        async Task<bool> SaveReportDetailAsync(Guid messageId, List<Tuple<string, int, int>> details)
        {
            try
            {
                foreach (var item in details)
                {
                    ReportDetail reportDetail = new ReportDetail()
                    {
                        City = item.Item1,
                        PeopleCount = item.Item2,
                        PhoneCount = item.Item3,
                        ReportId = messageId
                    };
                    await PgDbContext.ReportDetail.AddAsync(reportDetail);
                }
                await PgDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                PgDbContext.ChangeTracker.Clear();

                return false;
            }
        }
        async Task<bool> SaveReportDetailAsync(Guid messageId, Tuple<string, int, int> details)
        {
            try
            {

                ReportDetail reportDetail = new ReportDetail()
                {
                    City = details.Item1,
                    PeopleCount = details.Item2,
                    PhoneCount = details.Item3,
                    ReportId = messageId
                };
                await PgDbContext.ReportDetail.AddAsync(reportDetail);

                await PgDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                PgDbContext.ChangeTracker.Clear();

                return false;
            }
        }
        /// <summary>
        /// Olusturulan rapor eklenir.
        /// </summary>
        /// <param name="reportDto"></param>
        /// <returns></returns>
        public async Task<Report> CreatReportAsync(ReportDto reportDto)
        {
            try
            {
                var report = _mapper.Map<Report>(reportDto);
                await PgDbContext.Reports.AddAsync(report);
                await PgDbContext.SaveChangesAsync();
                return report;
            }
            catch (Exception)
            {
                PgDbContext.ChangeTracker.Clear();
                return null;
            }

        }
        /// <summary>
        /// Tum raporları listeler
        /// </summary>
        /// <returns></returns>
        public async Task<Response<List<Report>>> GetAllReadyReports()
        {
            var report = await PgDbContext.Reports.ToListAsync();
            return Response<List<Report>>.Success(_mapper.Map<List<Report>>(report), 200);
        }
        /// <summary>
        /// Hazır olan raporlardan Id'si girilen raporun detayı getirilir.
        /// </summary>
        /// <param name="reportID"></param>
        /// <returns></returns>
        public async Task<Response<List<ReportDetailDto>>> GetReadyReportDetail(Guid reportID)
        {
            try
            {
                var report = await PgDbContext.ReportDetail.Where(x => x.ReportId == reportID).ToListAsync();

                if (report != null)
                {
                    return Response<List<ReportDetailDto>>.Success(_mapper.Map<List<ReportDetailDto>>(report), 200);
                }
                return Response<List<ReportDetailDto>>.Fail("Report not found", 404);
            }
            catch (Exception ex)
            {
                return Response<List<ReportDetailDto>>.Fail("Report not found. Ex: " + ex.Message, 404);
            }

        }
        /// <summary>
        /// Tum sehirlerin istatistiklerini getirir.
        /// </summary>
        /// <returns></returns>
        public async Task<Response<List<ReportDto>>> GetStatisticsAllLocation()
        {
            try
            {
                ReportDto createdreport = new ReportDto()
                {
                    //Id = Guid.NewGuid(),
                    CreatedTime = DateTime.UtcNow,
                    ReportName = "ALL",
                    ReportStatus = "Hazırlanıyor"
                };
                var result = CreatReportAsync(createdreport).Result;
                if (result != null)
                {
                    var reportid = result.Id;
                    await Client.SendTopicAsync(reportid, MessageTopic.Request, MessageType.GetStatisticsAllLocation);
                    return Response<List<ReportDto>>.Success(200);
                }
                else
                {
                    return Response<List<ReportDto>>.Success(404);
                }

            }
            catch (Exception ex)
            {
                return Response<List<ReportDto>>.Fail("Report not created. Ex: " + ex.Message, 400);
            }
        }
        /// <summary>
        /// Girilen sehirin istatistiklerini getirir.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public async Task<Response<ReportDto>> GetStatisticsByLocation(string location)
        {
            try
            {
                ReportDto createdreport = new ReportDto()
                {
                    //Id = Guid.NewGuid(),
                    CreatedTime = DateTime.UtcNow,
                    ReportName = location,
                    ReportStatus = "Hazırlanıyor"
                };
                var result = CreatReportAsync(createdreport).Result;
                if (result != null)
                {
                    var reportid = result.Id;
                    byte[] data = Encoding.UTF8.GetBytes(location);
                    await Client.SendTopicAsync(reportid, MessageTopic.Request, MessageType.StatisticByLocation, data);
                    return Response<ReportDto>.Success(200);
                }
                else
                {
                    return Response<ReportDto>.Success(404);
                }
            }
            catch (Exception ex)
            {
                return Response<ReportDto>.Fail("Report not created. Ex: " + ex.Message, 400);
            }
        }
    }
}
