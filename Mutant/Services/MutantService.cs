using MutantCore.Contracts;
using MutantCore.ValueObject;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutantCore.Services
{
    public class MutantService : IMutantService
    {
        public async Task<bool> IsMutant(IEnumerable<string> adn)
        {
            return await new AdnValidator(adn.ToArray()).IsMutant();
        }
    }
}
