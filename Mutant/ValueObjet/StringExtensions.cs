using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MutantCore.ValueObject
{
    public static class StringExtensions
    {
        /// <summary>
        /// Given a list of strings, validate each line with the predicate
        /// </summary>
        /// <param name="this"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<string> HorizontalValidator(this IEnumerable<string> @this, Predicate<string> predicate)
        {
            return @this.Where(line => predicate(line));
        }

        /// <summary>
        /// Given a string list, it go through in a vetical way with the predicate   
        /// </summary>
        /// <param name="this"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<string> VerticalValidator(this IEnumerable<string> @this, Predicate<string> predicate)
        {
            var transpose = @this
                .Select((word => word.Select((c, pos) => new { c, pos })))
                .SelectMany(a => a)
                .GroupBy(a => a.pos)
                .OrderBy(a => a.Key)
                .Select(group => string.Concat(group.Select(c => c.c)));
            return transpose.Where(line => predicate(line));
        }

        public static IEnumerable<string> DiagonalValidator(this IEnumerable<string> @this, Predicate<string> predicate)
        {
            var matrix = @this
                .Select((wordx, posy) => wordx.Select((c, posx) => new { c, posx, posy }))
                .SelectMany(a => a).ToList();
            List<string> result = new List<string>();

            result.Add(string.Concat(matrix.Where(c => c.posx == c.posy).Select(c=>c.c)));
            for(int x = 0; x < @this.Count()-4; x++ )
            {
                result.Add(string.Concat(matrix.Where(c => c.posx == (c.posy + x + 1)).Select(c => c.c)));
                result.Add(string.Concat(matrix.Where(c => c.posx == (c.posy - x - 1)).Select(c => c.c)));
            }
            return result.Where(line => predicate(line));
        }
    }
}
