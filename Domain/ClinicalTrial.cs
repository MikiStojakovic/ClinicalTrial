namespace Domain
{
    public class ClinicalTrial : BaseEntity
    {
        public required string Title { get; set; }
        public required DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public required int Participants { get; set; }
        public ClinicalTrialStatus Status { get; set; }
        public required int ClinicalTrialStatusId { get; set; }
        public int TrialDurationInDays { get; set; }
    }
}