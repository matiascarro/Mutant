using MutantCore.Models;
using MutantCore.ValueObject;
using System;
using System.Collections.Generic;
using Xunit;

namespace MutantCoreTest
{
    public class StringExtensionsTest
    {
        private readonly Dna dna = new Dna(new string[] { "abcdef", "abcdef", "abcdef", "abcdef", "abcdef", "aaaaaa" });
        
        [Fact]
        public void HorizontalValidator_ValidPredicate_RowsExpected()
        {
            
            Predicate<string> predicate = s => s.Contains("abcd");
            IEnumerable<string> expected = new string[] { "abcdef", "abcdef", "abcdef", "abcdef", "abcdef" };
            Assert.Equal(expected, dna.HorizontalValidator(predicate));
        }

        [Fact]
        public void HorizontalValidator_InvalidPredicate_EmptyExpected()
        {

            Predicate<string> predicate = s => s.Contains("abcdd");
            IEnumerable<string> expected = new string[] { };
            Assert.Empty(dna.HorizontalValidator(predicate));
        }

        [Fact]
        public void VerticalValidator_ValidPredicate_RowsExpected()
        {

            Predicate<string> predicate = s => s.Contains("bbba");
            IEnumerable<string> expected = new string[] { "bbbbba" };
            Assert.Equal(expected, dna.VerticalValidator(predicate));
        }

        [Fact]
        public void VerticalValidator_InvalidPredicate_EmptyExpected()
        {

            Predicate<string> predicate = s => s.Contains("bbba");
            IEnumerable<string> expected = new string[] { "bbbbba" };
            Assert.Equal(expected, dna.VerticalValidator(predicate));
        }

        [Fact]
        public void DiagonalValidator_ValidPredicate_RowsExpected()
        {
            Predicate<string> predicate = s => s.Contains("abca");
            IEnumerable<string> expected = new string[] { "abca" };
            Assert.Equal(expected, dna.DiagonalValidator(predicate));
        }

        [Fact]
        public void DiagonalValidator_InvalidPredicate_EmptyExpected()
        {
            Predicate<string> predicate = s => s.Contains("abcaa");
            Assert.Empty(dna.DiagonalValidator(predicate));
        }
    }
}
