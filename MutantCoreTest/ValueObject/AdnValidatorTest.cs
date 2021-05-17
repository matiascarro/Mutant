using MutantCore.Models;
using MutantCore.ValueObject;
using System;
using Xunit;

namespace MutantCoreTest.ValueObject
{
    public class AdnValidatorTest
    {
        private readonly string[] mutant = new string[]
        {
            "ATGCGA",
            "CAGTGC",
            "TTATGT",
            "AGAAGG",
            "CCCCTA",
            "TCACTG"
        };

        private readonly string[] human = new string[]
        {
            "ATGCGA",
            "CAGTGC",
            "TTATTT",
            "AGACGG",
            "GCGTCA",
            "TCACTG"
        };


        [Fact]
        public void IsMutant_ValidAdn_ReturnTrue()
        {
            Dna dna = new Dna(mutant);
            Assert.True(dna.IsMutant().Result.Value);
        }

        [Fact]
        public void IsMutant_NotValidAdn_ReturnFalse()
        {
            Dna dna = new Dna(human);
            Assert.False(dna.IsMutant().Result.Value);
        }

        [Fact]
        public void IsMutant_NullAdn_ReturnException()
        {
            Assert.Throws<ArgumentNullException>(()=> new Dna(null));
        }
    }
}
