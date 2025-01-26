using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using API.DTOs;
using Domain;

namespace API.Extensions
{
    public static class ClinicalTrialMappingExtensions
    {
        public static ClinicalTrialDto ToDto(this ClinicalTrial clinicalTrial)
        {
            if (clinicalTrial == null) return null;

            return new ClinicalTrialDto
            {
                Title = clinicalTrial.Title,
                StartDate = clinicalTrial.StartDate.ToString(),
                EndDate = clinicalTrial.EndDate.ToString(),
                Participants = clinicalTrial.Participants,
                Status = clinicalTrial.ClinicalTrialStatusId
            };
        }

        public static ClinicalTrial ToEntity(this ClinicalTrialDto clinicalTrialDto)
        {
            if (clinicalTrialDto == null) throw new ArgumentNullException(nameof(clinicalTrialDto));

            return new ClinicalTrial
            {
                Title = clinicalTrialDto.Title,
                StartDate = DateOnly.FromDateTime(DateTime.Parse(clinicalTrialDto.StartDate)),
                EndDate = String.IsNullOrEmpty(clinicalTrialDto.EndDate) ? null : DateOnly.FromDateTime(DateTime.Parse(clinicalTrialDto.StartDate)),
                Participants = clinicalTrialDto.Participants,
                ClinicalTrialStatusId = clinicalTrialDto.Status 
            };
        }
    }
}