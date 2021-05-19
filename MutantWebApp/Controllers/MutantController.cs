using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MutantCore.Contracts;
using MutantWebApp.Extensions;
using MutantWebApp.Resource;
using System;
using System.Threading.Tasks;

namespace MutantWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController, AllowAnonymous]
    public class MutantController : ControllerBase
    {
        private readonly IMutantService mutantService;
        private readonly Guid requestId = Guid.NewGuid();

        public MutantController(IMutantService mutantService)
        {
            this.mutantService = mutantService;
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MutantResponseResourceForPost>> Post([FromBody] MutantResourceForPost mutantResource)
        {
            if (mutantResource is null || mutantResource.Dna is null)
                return BadRequest(new { error = "The dna prvided cannot be null" });
            var resource = await mutantService.IsMutant(requestId, mutantResource.Dna);
            return resource.CheckIfErrorApiResult(r => new MutantResponseResourceForPost { IsMutant = r });
        }

        [HttpGet]
        [Route("stats")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MutantStatsResourceForGet>> GetStats()
        {
            var resource = await mutantService.GetStats();
            return resource.CheckIfErrorApiResult(stats => new MutantStatsResourceForGet
            {
                count_human_dna = stats.CountHumanDna,
                count_mutant_dna = stats.CountMutantDna,
                ratio = Math.Round(stats.Ratio, 1)
            }); ;
        }

    }
  
}
