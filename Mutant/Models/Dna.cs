using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MutantCore.Models
{
    public class Dna
    {
        public ImmutableArray<string> GetDna => ImmutableArray.CreateRange(dna);
        private readonly IEnumerable<string> dna;

        private static readonly Predicate<char> dnaElement = s => s.Equals('A')
                || s.Equals('T')
                || s.Equals('C')
                || s.Equals('G');

        public Dna(IEnumerable<string> dna)
        {
            if (!IsValidDna(dna))
                throw new ArgumentNullException(nameof(dna));
            this.dna = dna;
        }

        public static bool IsValidDna(IEnumerable<string> dna) 
            =>dna.All(s => s.ToUpper().All(c => dnaElement(c)));
    }
}
