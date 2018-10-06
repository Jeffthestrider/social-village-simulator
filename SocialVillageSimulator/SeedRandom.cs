using System;

namespace Jochum.SocialVillageSimulator
{
    public static class SeedRandom
    {
        private static Random _rand;

        public static void InitializeRandom(int seed)
        {
            _rand = new Random(seed);
        }
        public static Random Rand => _rand;
    }
}
