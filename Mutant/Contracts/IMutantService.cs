using MutantCore.Models;
using MutantCore.ServiceResult;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MutantCore.Contracts
{
    public interface IMutantService
    {
        Task<Result<bool>> IsMutant(Guid requestId, IEnumerable<string> adn);
        Task<Result<DnaStats>> GetStats();
    }
}
