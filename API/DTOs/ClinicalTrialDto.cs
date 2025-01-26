namespace API.DTOs
{
    public class ClinicalTrialDto
    {
       public required string Title { get; set; }
        public required string StartDate { get; set; }
        public string? EndDate { get; set; }
        public required int Participants { get; set; }
        public required int Status { get; set; }
    }
}