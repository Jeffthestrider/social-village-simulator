using System;
using System.Collections.Generic;
using System.Linq;

namespace Jochum.SocialVillageSimulator
{
    public class Response
    {
        public string Dialogue { get; set; }
        public string BodyLanguage { get; set; }
        public ResponseType ResponseType {get;set;}
    }


    public class ResponseCriteria
    {
        public ResponseCriteria(IList<Response> responses, IList<Func<Character, Character, bool>> isActiveFuncs)
        {
            Responses = responses;
            IsValidFuncs = isActiveFuncs.Select(prop => ((ValidityChecker)((speaker, spokenTo) => prop(speaker, spokenTo)))).ToList();
        }

        public IList<Response> Responses { get; set; }

        public delegate bool ValidityChecker(Character speaker, Character spokenTo);

        public IList<ValidityChecker> IsValidFuncs;

        // Loop through all criteria funcs. If valid, this response can be used.
        public bool IsValid(Character speaker, Character spokenTo)
        {
            return IsValidFuncs.All(func => func(speaker, spokenTo));
        }
    }

    public enum ResponseType
    {
        Positive,
        Neutral,
        Negative,
        DetailedAnswer,
        BriefAnswer,
        RefuseAnswer,
        DoNotHaveAnswer,
    }
}