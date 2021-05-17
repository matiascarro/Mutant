using System;
using System.Collections.Generic;
using System.Text;

namespace MutantCore.Models
{
    public class DnaStats
    {
        public int CountMutantDna { get; set; }
        public int CountHumanDna { get; set; }

        public double Ratio => CountHumanDna + CountMutantDna == 0 ? 0 : CountMutantDna / (CountHumanDna + CountMutantDna);
    }
}
