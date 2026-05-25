using System;

namespace Domain
{
    public static class PermutationShuffle
    {
        public static void RearrangeShuffle(int[] indexes)
        {
            var random = new Random();
            
            for (var i = indexes.Length - 1; i > 0; i--)
            {
                var j = random.Next(i + 1);
                (indexes[i], indexes[j]) = (indexes[j], indexes[i]);
            }
        }
    }
}