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
                _genericRepository.AddRange(request.ClinicalTrials);

                await _genericRepository.SaveChangesAsync();
            }
        }
    }
}