namespace ReportManagementService.Dtos
{
    public class ReportDto
    {
        public Guid Id { get; set; }
        public string ReportName { get; set; }
        public string ReportStatus { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
