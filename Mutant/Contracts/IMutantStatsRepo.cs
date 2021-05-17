using MutantCore.Models;
using MutantCore.ServiceResult;
using System.Threading.Tasks;

namespace MutantCore.Contract
{
    public interface IMutantStatsRepo
    {
        Task<IResult> SaveStats(string id, bool IsMutant);

        Task<Result<DnaStats>> GetStats();
    }
}
