﻿namespace ReportManagementService.Dtos
{
    public class ReportDetailDto
    {
        public Guid ReportId { get; set; }
        public string City { get; set; }
        public int PeopleCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
