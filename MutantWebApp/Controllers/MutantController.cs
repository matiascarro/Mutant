using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MutantCore.Contracts;
using System.Threading.Tasks;

namespace MutantWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController, AllowAnonymous]
    public class MutantController : ControllerBase
    {
        private readonly IMutantService mutantService;

        public MutantController(IMutantService mutantService)
        {
            this.mutantService = mutantService;
        }
        

        // POST api/values
        [HttpPost]
        public async Task<bool> Post([FromBody] string[] dna)
        {
            return await mutantService.IsMutant(dna);
        }
        
    }
}
