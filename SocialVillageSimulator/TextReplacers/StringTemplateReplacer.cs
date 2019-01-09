using Jochum.SocialVillageSimulator.SocialAspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AvsAnLib;
using Jochum.SocialVillageSimulator.GameObjects;
using Jochum.SocialVillageSimulator.Interactions;

namespace Jochum.SocialVillageSimulator
{
    public class StringTemplateReplacer : IStringTemplateReplacer
    {
        static readonly Dictionary<string, Func<Character, string>> templatesForSpeaker = new Dictionary<string, Func<Character, string>>
        {
            { "{MyName}", speaker => speaker.Name },

            { "{MyMood}", speaker => speaker.Mood.ToString() },

            { "{MyGender}", speaker => speaker.Gender.ToString() },
            { "{MyGenderSubject}", speaker => speaker.Gender.ToSubject() },
            { "{MyGenderObject}", speaker => speaker.Gender.ToObject() },
            { "{MyGenderPossessivePronoun}", speaker => speaker.Gender.ToPossessivePronoun() },
            { "{MyGenderPossessiveAdjective}", speaker => speaker.Gender.ToPossessiveAdjective() },
        };

        static readonly Dictionary<string, Func<Character, string>> templatesForSpokenTo = new Dictionary<string, Func<Character, string>>
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

        static Dictionary<string, Func<ParsedAction, string>> _templatesForActions = new Dictionary<string, Func<ParsedAction, string>>
        {
            { "{ItemType}", action => action.Object?.ToLower() },
        };

        private struct TemplateInText
        {
            public string Text { get; set; }
            public int TemplateIndex { get; set; }
        }

        private static readonly Dictionary<string, Func<TemplateInText, string>> _templatesForGrammarRules = new Dictionary<string, Func<TemplateInText, string>>
        {
            { "a(n)", ReplaceTextWithAOrAn
            },
        };

        private static string ReplaceTextWithAOrAn(TemplateInText templateInText)
        {
            if (templateInText.TemplateIndex + 5 >= templateInText.Text.Length)
                return "a";

            string wordAfterTemplate = GetNextWord(templateInText.Text, templateInText.TemplateIndex + 5);

            var result = AvsAn.Query(wordAfterTemplate);
            return result.Article;
        }

        private static string GetNextWord(string text, int index)
        {
            var textAfterTemplate = text.Substring(index);
            var nonWordMatch = Regex.Match(textAfterTemplate, "\\W");
            var nonWord = nonWordMatch.Value;
            var nonWordIndex = nonWordMatch.Length == 0 ? textAfterTemplate.Length : textAfterTemplate.IndexOf(nonWord, StringComparison.Ordinal);
            var word = textAfterTemplate.Substring(0, nonWordIndex);
            return word;
        }

        public string FillInTemplate(ParsedAction action, Character speaker, string templateString, Character spokenTo)
        {
            var fillInBlankMatches = Regex.Matches(templateString, "[{][^}]+[}]")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            foreach (var match in fillInBlankMatches)
            {
                Func<Character, string> speakerReplacementFunc = null; ;
                templatesForSpeaker.TryGetValue(match, out speakerReplacementFunc);

                Func<Character, string> replierReplacementFunc = null;
                templatesForSpokenTo.TryGetValue(match, out replierReplacementFunc);
                
                Func<ParsedAction, string> actionReplacementFunc = null;
                _templatesForActions.TryGetValue(match, out actionReplacementFunc);


                if (speakerReplacementFunc != null)
                    templateString = templateString.Replace(match, speakerReplacementFunc(speaker));
                else if (replierReplacementFunc != null)
                    templateString = templateString.Replace(match, replierReplacementFunc(spokenTo));
                else if (actionReplacementFunc != null)
                    templateString = templateString.Replace(match, actionReplacementFunc(action));
            }

            var grammarMatches = Regex.Matches(templateString, "a\\(n\\)")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();


            foreach (var match in grammarMatches)
            {
                Func<TemplateInText, string> grammarReplacementFunc = null;
                _templatesForGrammarRules.TryGetValue(match, out grammarReplacementFunc);

                if (grammarReplacementFunc != null)
                {
                    var grammarTemplateText = new TemplateInText
                    {
                        TemplateIndex = templateString.IndexOf(match, StringComparison.Ordinal),
                        Text = templateString
                    };
                    var regex = new Regex(Regex.Escape(match));
                    templateString = regex.Replace(templateString, grammarReplacementFunc(grammarTemplateText), 1);
                }
            }

            // Capitalize filled in words for filled interactions.
            // We can probably move a(an) to just either a/an and find those specific words and double check them.
            // verb modifiers for plural - first person - second person/singular like take/takes, have/has, do/does etc I hope there is a library for this or at least a list of these verbs

            return templateString;
        }
    }
}
