using Domain;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.ClinicalTrials
{
    public class ClinicalTrialList
    {
        public class Query : IRequest<List<ClinicalTrial>> {}

        public class Handler : IRequestHandler<Query, List<ClinicalTrial>>
        {
            private readonly IGenericRepository<ClinicalTrial> _genericRepository;

            public Handler(IGenericRepository<ClinicalTrial> genericRepository)
            {
                _genericRepository = genericRepository;
            }

            public async Task<List<ClinicalTrial>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _genericRepository.ListAllAsync();
            }
        }
    }
}