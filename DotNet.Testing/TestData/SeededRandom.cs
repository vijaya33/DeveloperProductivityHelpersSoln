using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


// Sample code: TestData

// File: `src/DotNet.Testing/TestData/SeededRandom.cs`

namespace DotNet.Testing.TestData
{

    public sealed class SeededRandom
    {
        private readonly Random _random;

        public SeededRandom(int seed = 12345)
        {
            _random = new Random(seed);
        }

        public int NextInt(int minInclusive, int maxExclusive) => _random.Next(minInclusive, maxExclusive);

        public bool NextBool() => _random.Next(0, 2) == 1;

        public string NextString(int length = 10)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Range(0, length)
                .Select(_ => chars[_random.Next(chars.Length)])
                .ToArray());
        }

        public DateTimeOffset NextDateTimeOffset(int pastDays = 30)
        {
            var days = _random.Next(0, pastDays);
            var seconds = _random.Next(0, 24 * 3600);
            return DateTimeOffset.UtcNow.AddDays(-days).AddSeconds(-seconds);
        }
    }

}