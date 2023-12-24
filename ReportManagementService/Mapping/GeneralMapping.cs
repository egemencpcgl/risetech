using AutoMapper;
using ReportManagementService.Dtos;
using ReportManagementService.Models;
using System;

namespace ReportManagementService.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Report, ReportDto>().ReverseMap();
            CreateMap<ReportDetail, ReportDetailDto>().ReverseMap();
        }
    }
}
