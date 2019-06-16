using System;

namespace Jochum.SocialVillageSimulator
{
    public static class RandomSeedGenerator
    {
        private static Random _random;

        public static void InitializeRandom(int seed)
        {
            _random = new Random(seed);
        }

        public static Random Random => _random;
    }
}
