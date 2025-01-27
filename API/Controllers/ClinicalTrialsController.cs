using System.Text.Json;
using System.Text.Json.Serialization;
using API.DTOs;
using API.Extensions;
using API.Helper;
using Application.ClinicalTrials;
using Application.ClinicalTrials.Commands;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

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

        [HttpPost]
        public async Task<IActionResult> CreateClinicalTrials(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            if (file.ContentType != "application/json")
                return BadRequest("Only JSON files are allowed.");

            var receivedFile = FileHelper.ReadReceivedFile(file);
            if (!FileHelper.IsJsonFileValidationSuccesfull(receivedFile))
                return BadRequest("JSON file is in invalid format.");

            var clinicalTrialsDto = JsonSerializer.Deserialize<List<ClinicalTrialDto>>(receivedFile);
            var clinicalTrials = clinicalTrialsDto?.Select(c => c.ToEntity()).ToList();
            await Mediator.Send(new ClinicalTrialCreate.Command { ClinicalTrials = clinicalTrials });

            return Ok();
        } 
    }
}