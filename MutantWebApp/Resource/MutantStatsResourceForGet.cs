using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutantWebApp.Resource
{
    public class MutantStatsResourceForGet
    {
        public int count_mutant_dna { get; set; }
        public int count_human_dna { get; set; }
        public double ratio { get; set; }
    }
}
