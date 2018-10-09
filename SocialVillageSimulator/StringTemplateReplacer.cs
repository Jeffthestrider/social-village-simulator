using Jochum.SocialVillageSimulator.SocialAspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Jochum.SocialVillageSimulator.GameObjects;

namespace Jochum.SocialVillageSimulator
{
    public static class StringTemplateReplacer
    {
        static Dictionary<string, Func<Character, string>> templatesForSpeaker = new Dictionary<string, Func<Character, string>>
        {
            { "{MyName}", speaker => speaker.Name },

            { "{MyMood}", speaker => speaker.Mood.ToString() },

            { "{MyGender}", speaker => speaker.Gender.ToString() },
            { "{MyGenderSubject}", speaker => speaker.Gender.ToSubject() },
            { "{MyGenderObject}", speaker => speaker.Gender.ToObject() },
            { "{MyGenderPossessivePronoun}", speaker => speaker.Gender.ToPossessivePronoun() },
            { "{MyGenderPossessiveAdjective}", speaker => speaker.Gender.ToPossessiveAdjective() },
        };

        static Dictionary<string, Func<Character, string>> templatesForSpokenTo = new Dictionary<string, Func<Character, string>>
        {
            { "{Name}", replier => replier.Name },
            { "{NameOrYou}", replier => replier.IsPc ? "you" : replier.Name.ToString() },

            { "{Mood}", replier => replier.Mood.ToString() },

            { "{Gender}", replier => replier.Gender.ToString() },
            { "{GenderSubject}", replier => replier.Gender.ToSubject() },
            { "{GenderSubjectOrYou}", replier => replier.IsPc ? "you" : replier.Gender.ToSubject() },
            { "{GenderObject}", replier => replier.Gender.ToObject() },
            { "{GenderObjectOrYou}", replier => replier.IsPc ? "you" : replier.Gender.ToObject() },
            { "{GenderPossessivePronoun}", replier => replier.Gender.ToPossessivePronoun() },
            { "{GenderPossessivePronounOrYou}", replier => replier.IsPc ? "yours" : replier.Gender.ToPossessivePronoun() },
            { "{GenderPossessiveAdjective}", replier => replier.Gender.ToPossessiveAdjective() },
            { "{GenderPossessiveAdjectiveOrYou}", replier => replier.IsPc ? "yours" : replier.Gender.ToPossessiveAdjective() },
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
