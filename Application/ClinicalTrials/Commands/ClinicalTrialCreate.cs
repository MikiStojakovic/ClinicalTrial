using Domain;
using Domain.Interfaces;
using MediatR;

namespace Application.ClinicalTrials.Commands
{
    public class ClinicalTrialCreate
    {
        public class Command : IRequest
        {
            public List<ClinicalTrial> ClinicalTrials { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IGenericRepository<ClinicalTrial> _genericRepository;
            public Handler(IGenericRepository<ClinicalTrial> genericRepository)
            {
                _genericRepository = genericRepository;
            }
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var clinicalTrials = SetEndDateAndTrialDuration(request.ClinicalTrials);
                _genericRepository.AddRange(clinicalTrials);

                await _genericRepository.SaveChangesAsync();
            }

            protected List<ClinicalTrial> SetEndDateAndTrialDuration(List<ClinicalTrial> clinicalTrials)
            {
                var result = new List<ClinicalTrial>();

                foreach (var clinicalTrial in clinicalTrials)
                {
                    if (clinicalTrial.EndDate is null && clinicalTrial.ClinicalTrialStatusId == 2)
                    {
                        var endDate = DateOnly.FromDateTime(clinicalTrial.StartDate.ToDateTime(TimeOnly.Parse("00:00 AM")).AddMonths(1));
                        clinicalTrial.EndDate = endDate;
                        clinicalTrial.TrialDurationInDays = clinicalTrial.EndDate.Value.DayNumber - clinicalTrial.StartDate.DayNumber;
                        result.Add(clinicalTrial); 
                    }
                    else
                    {
                        result.Add(clinicalTrial);
                    }                
                }

                return result;
            }
        }
    }
}