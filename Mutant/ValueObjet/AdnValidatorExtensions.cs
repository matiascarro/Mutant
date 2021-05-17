using MutantCore.Models;
using MutantCore.ServiceResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MutantCore.ValueObject
{
    public static class AdnValidatorExtension
    {
        private static readonly Predicate<string> mutant = s => s.Contains("AAAA")
                || s.Contains("TTTT")
                || s.Contains("CCCC")
                || s.Contains("GGGG");

        public static async Task<Result<bool>> IsMutant(this Dna @this)
        {
            int horizontal = 0, vertical = 0, diagonal = 0;

            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(() => {
                horizontal = @this.HorizontalValidator(mutant).Count();
            }));
            tasks.Add(Task.Run(() => {
                vertical = @this.VerticalValidator(mutant).Count();
            }));
            tasks.Add(Task.Run(() => {
                diagonal = @this.DiagonalValidator(mutant).Count();
            }));
            await Task.WhenAll(tasks.ToArray());
            
            return Result<bool>.CreateSuccessfulResult(horizontal + vertical + diagonal > 1);
        }
    }
}
