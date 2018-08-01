using System;

namespace Jochum.SocialVillageSimulator
{
    public static class MasterRandom
    {
        private static Random _rand;

        public static void InitializeRandom(int seed)
        {
            _rand = new Random(seed);
        }
        public static Random Rand => _rand;
    }
}
