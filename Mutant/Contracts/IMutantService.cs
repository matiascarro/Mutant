using MutantCore.ServiceResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MutantCore.Contracts
{
    public interface IMutantService
    {
        Task<bool> IsMutant(IEnumerable<string> adn);
    }
}
