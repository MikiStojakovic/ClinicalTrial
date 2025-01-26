using Domain;
using Domain.Interfaces;
using MediatR;
using Persistence.Data;

namespace Application.ClinicalTrials
{
    public class ClinicalTrialDetail
    {
        public class Query : IRequest<ClinicalTrial>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ClinicalTrial>
        {
            private readonly IGenericRepository<ClinicalTrial> _genericRepository;
            public Handler(IGenericRepository<ClinicalTrial> genericRepository)
            {
                _genericRepository = genericRepository;
            }
            public async Task<ClinicalTrial> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _genericRepository.GetByIdAsync(request.Id);
            }
        }
    }
}