namespace ReportManagementService.Models
{
    public class Report
    {
        public Guid Id { get; set; }
        public string ReportName { get; set; }
        public string ReportStatus { get; set; }
        public DateTime CreatedTime { get; set; }

        public Report()
        {
            Id = Guid.NewGuid();
        }
    }
}
