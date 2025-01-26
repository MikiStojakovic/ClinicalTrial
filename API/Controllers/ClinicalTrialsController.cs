using System.Text.Json;
using System.Text.Json.Serialization;
using API.DTOs;
using API.Extensions;
using Application.ClinicalTrials;
using Application.ClinicalTrials.Commands;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ClinicalTrialsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<ClinicalTrial>>> GetClientTrials()
        {
            return await Mediator.Send(new ClinicalTrialList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicalTrial>> GetClinicalTrial(int id)
        {
            return await Mediator.Send(new ClinicalTrialDetail.Query{Id = id});
        }

        [Consumes("application/json")]
        [HttpPost]
        public async Task<IActionResult> CreateClinicalTrials(IFormFile file)
        {
            string fileContent = null;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                fileContent = reader.ReadToEnd();
            }

            var clinicalTrialsDto = JsonSerializer.Deserialize<List<ClinicalTrialDto>>(fileContent);
            var clinicalTrials = clinicalTrialsDto?.Select(c => c.ToEntity()).ToList();
            await Mediator.Send(new ClinicalTrialCreate.Command { ClinicalTrials = clinicalTrials });

            return Ok();
        }
    }
}