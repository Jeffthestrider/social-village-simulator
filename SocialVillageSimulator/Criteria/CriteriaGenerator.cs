using System;
using System.Collections.Generic;
using System.Linq;

namespace Jochum.SocialVillageSimulator.Criteria
{
    public static class GenericCriteriaGenerator
    {
        public static Func<Character, Character, bool> AlwaysTrue => (s, st) => true;

        public static IList<Func<Character, Character, bool>> CreateList(params Func<Character, Character, bool>[] criteriaList)
        {
            return criteriaList.ToList();
        }
    }
}
