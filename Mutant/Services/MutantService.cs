using MutantCore.Contract;
using MutantCore.Contracts;
using MutantCore.Models;
using MutantCore.ServiceResult;
using MutantCore.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutantCore.Services
{
    public class MutantService : IMutantService
    {
        private readonly IMutantStatsRepo mutantRepo;

        public MutantService(IMutantStatsRepo mutantStatsRepo)
        {
            this.mutantRepo = mutantStatsRepo;
        }

        public async Task<Result<DnaStats>> GetStats()
        {
            return await mutantRepo.GetStats();
        }

        public async Task<Result<bool>> IsMutant(Guid requestId, IEnumerable<string> dna)
        {
            if (!Dna.IsValidDna(dna))
                return Result<bool>.CreateFailureResult($"Dna provided is not valid, dna:{dna}");
            Dna dnaWrapper = new Dna(dna);
            Result<bool> isMutant = await dnaWrapper.IsMutant();
            if(isMutant.IsSuccessful())
            {
                var result  = await mutantRepo.SaveStats(requestId.ToString(), isMutant.Value);
                if(!result.IsSuccessful())
                {
                    return Result<bool>.CreateFailureResult(result.Error);
                }
            }
            
            return isMutant;
        }
    }
}
