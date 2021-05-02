using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutantCore.ValueObject
{
    public class AdnValidator
    {
        private readonly string[] adn;
        private readonly Predicate<string> mutant = s => s.Contains("AAAA")
                || s.Contains("TTTT")
                || s.Contains("CCCC")
                || s.Contains("GGGG");

        public AdnValidator(string[] adn)
        {
            if (adn is null)
                throw new ArgumentNullException(nameof(adn));
            this.adn = adn;
        }

        public async Task<bool> IsMutant()
        {
            int mutantChain = 0;
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(() => {
                mutantChain += adn.HorizontalValidator(mutant).Count();
            }));
            tasks.Add(Task.Run(() => {
                mutantChain += adn.VerticalValidator(mutant).Count();
            }));
            tasks.Add(Task.Run(() => {
                mutantChain += adn.DiagonalValidator(mutant).Count();
            }));
            await Task.WhenAll(tasks.ToArray());
            
            return mutantChain > 1;
        }
    }
}
