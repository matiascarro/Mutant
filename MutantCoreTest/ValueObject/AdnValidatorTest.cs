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
            AdnValidator adnValidator = new AdnValidator(mutant);
            Assert.True(adnValidator.IsMutant().Result);
        }

        [Fact]
        public void IsMutant_NotValidAdn_ReturnFalse()
        {
            AdnValidator adnValidator = new AdnValidator(human);
            Assert.False(adnValidator.IsMutant().Result);
        }

        [Fact]
        public void IsMutant_NullAdn_ReturnException()
        {
            Assert.Throws<ArgumentNullException>(()=> new AdnValidator(null));
        }
    }
}
