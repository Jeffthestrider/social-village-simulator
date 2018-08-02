using System;
using System.Collections.Generic;
using System.Linq;

namespace Jochum.SocialVillageSimulator.Interactions
{
    public class InteractionCriteria
    {

        public IList<Interaction> Interactions { get; set; }

        public delegate bool ValidityChecker(Character speaker, Character spokenTo);

        public IList<ValidityChecker> IsValidFuncs;

        public InteractionCriteria(IList<Interaction> interactions, IList<Func<Character, Character, bool>> isActiveFuncs)
        {
            Interactions = interactions;
            IsValidFuncs = isActiveFuncs.Select(prop => ((ValidityChecker)((speaker, spokenTo) => prop(speaker, spokenTo)))).ToList();
        }

        // Loop through all criteria funcs. If valid, this response can be used.
        public bool IsValid(Character speaker, Character spokenTo)
        {
            return IsValidFuncs.All(func => func(speaker, spokenTo));
        }
    }
}