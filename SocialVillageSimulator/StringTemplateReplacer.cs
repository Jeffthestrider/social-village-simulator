using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Jochum.SocialVillageSimulator
{
    public static class StringTemplateReplacer
    {
        static Dictionary<string, Func<Character, string>> templatesForSpeaker = new Dictionary<string, Func<Character, string>>
        {
            { "{MyName}", speaker => speaker.Name }
        };

        static Dictionary<string, Func<Character, string>> templatesForSpokenTo = new Dictionary<string, Func<Character, string>>
        {
            { "{Name}", replier => replier.Name }
        };
        
        public static string FillInTemplate(Character speaker, string templateString, Character spokenTo)
        {
            var respondeeMatches = Regex.Matches(templateString, "[{][^}]+[}]")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            foreach (var match in respondeeMatches)
            {
                Func<Character, string> speakerReplacementFunc = null; ;
                templatesForSpeaker.TryGetValue(match, out speakerReplacementFunc);

                Func<Character, string> replierReplacementFunc = null;
                templatesForSpokenTo.TryGetValue(match, out replierReplacementFunc);

                if (speakerReplacementFunc != null)
                    templateString = templateString.Replace(match, speakerReplacementFunc(speaker));
                else if (replierReplacementFunc != null)
                    templateString = templateString.Replace(match, replierReplacementFunc(spokenTo));
            }

            return templateString;
        }
    }
}
