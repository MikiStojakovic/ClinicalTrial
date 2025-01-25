using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence.Data
{
    public class DataContextSeed
    {
        public static async Task SeedData(DataContext context)
        {
            if (!context.ClinicalTrialStatus.Any())
            {
                var clinicalTrialStatuses = new List<ClinicalTrialStatus>
                {
                    new ClinicalTrialStatus { Type = "Not Started" },
                    new ClinicalTrialStatus { Type = "Ongoing" },
                    new ClinicalTrialStatus { Type = "Completed" }
                };

                await context.ClinicalTrialStatus.AddRangeAsync(clinicalTrialStatuses);
                await context.SaveChangesAsync();
            }

            if (!context.ClinicalTrials.Any())
            {
                var clinicalTraialStatusId = context.ClinicalTrialStatus.Take(1).Select(status => status.Id).FirstOrDefault();
                var clinicalTrials = new List<ClinicalTrial>
                {
                    new ClinicalTrial { Title = "Aspirin", StartDate = DateOnly.FromDateTime(DateTime.Now), 
                        Participants = 3, ClinicalTrialStatusId = clinicalTraialStatusId},
                    new ClinicalTrial { Title = "Andol", StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)), 
                        Participants = 6, ClinicalTrialStatusId = clinicalTraialStatusId},
                    new ClinicalTrial { Title = "Caffetin", StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)), 
                        Participants = 9, ClinicalTrialStatusId = clinicalTraialStatusId}
                };

                await context.ClinicalTrials.AddRangeAsync(clinicalTrials);
                await context.SaveChangesAsync();
            }
        }
    }
}