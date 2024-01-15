namespace ReportManagementService.Models
{
    public class ReportDetail
    {
        public Guid Id { get; set; }
        public Guid ReportId { get; set; }
        public string City { get; set; }
        public int PeopleCount { get; set; }
        public int PhoneCount { get; set; }

        public ReportDetail()
        {
            Id = Guid.NewGuid();
        }
    }
}
