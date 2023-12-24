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
        static PgDbContext PgDbContext = new PgDbContext();

        public ReportService(IMapper mapper)
        {
            // PgDbContext =;
            _mapper = mapper;
        }

        public static void InitMqttClient()
        {
            Client.StartClient("localhost", 5004, "reportClient");

            Client._mqttClient.ApplicationMessageReceivedAsync += _mqttClient_ApplicationMessageReceivedAsync;
            Client.SubscribeTopics(MessageTopic.Response.ToString());
        }

        private static Task _mqttClient_ApplicationMessageReceivedAsync(MQTTnet.Client.MqttApplicationMessageReceivedEventArgs arg)
        {
            var payload = arg.ApplicationMessage?.Payload == null ? null : Encoding.UTF8.GetString(arg.ApplicationMessage?.Payload);
            var msgbyte = JsonSerializer.Deserialize<MqttMessage>(payload);
            var msjjson = Encoding.UTF8.GetString(msgbyte.MessageData);
            var msg = JsonSerializer.Deserialize<List<Tuple<string, int, int>>>(msjjson);

            bool messageiscorrect;
            if (msg.Count == 0)
            {
                messageiscorrect = false;
            }
            else
            {
                messageiscorrect = true;

            }
            if(SaveReportDetail(msgbyte.MessageId, msg))
            {
                CheckReportStatusAndUpdate(msgbyte.MessageId, messageiscorrect);
            }
            return Task.CompletedTask;
        }

        static bool CheckReportStatusAndUpdate(Guid id, bool correctcompleted)
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
                return false;
            }

        }

        static bool SaveReportDetail(Guid messageId,List<Tuple<string,int,int>> details)
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
                    PgDbContext.ReportDetail.AddAsync(reportDetail);
                }
                PgDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public Report CreatReport(ReportDto reportDto)
        {
            var report = _mapper.Map<Report>(reportDto);
            PgDbContext.Reports.AddAsync(report);
            PgDbContext.SaveChangesAsync();
            return report;
        }

        public async Task<Response<List<ReportDto>>> GetAllReadyReports()
        {
            var report = await PgDbContext.Reports.ToListAsync();
            return Response<List<ReportDto>>.Success(_mapper.Map<List<ReportDto>>(report), 200);
        }

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
            catch (Exception)
            {
                return Response<List<ReportDetailDto>>.Fail("Report not found", 404);
            }

        }

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
                var reportid = CreatReport(createdreport).Id;
                await Client.SendTopicAsync(reportid, MessageTopic.Request, MessageType.GetStatisticsAllLocation);
                return Response<List<ReportDto>>.Success(200);
            }
            catch (Exception)
            {
                return Response<List<ReportDto>>.Fail("Report not created", 400);
            }
        }

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
                var reportid = CreatReport(createdreport).Id;
                byte[] data = Encoding.UTF8.GetBytes(location);
                await Client.SendTopicAsync(reportid, MessageTopic.Request, MessageType.StatisticByLocation, data);
                return Response<ReportDto>.Success(createdreport, 200);
            }
            catch (Exception)
            {
                return Response<ReportDto>.Fail("Report not created", 400);
            }
        }
    }
}
