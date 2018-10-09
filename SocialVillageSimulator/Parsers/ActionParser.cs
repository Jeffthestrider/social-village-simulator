using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jochum.SocialVillageSimulator.GameObjects;
using Jochum.SocialVillageSimulator.Interactions;

namespace Jochum.SocialVillageSimulator.Parsers
{
    public class ActionParser : IActionParser
    {
        public ParsedAction GetAction(Character speaker, Character spokenTo, string actionText)
        {
            var actionComponents = actionText.Split('.');

            var subjectText = actionComponents[0].ToLower();
            var adverbText = actionComponents[1].ToLower();
            var verbText = actionComponents[2].ToLower();
            string objectText = null;
            if (actionComponents.Length == 4)
                objectText = actionComponents[3].ToLower();

            Character parsedSubject = null;


            if (subjectText.Equals("speaker"))
            {
                parsedSubject = speaker;
            }
            else if (subjectText.Equals("spokento"))
            {
                parsedSubject = spokenTo;
            }
            else
            {
                throw new ArgumentException($"Criteria expression cannot parse {subjectText}.");
            }
            
            var parsedAdverb = ParserHelper.GetEnumValue<ActionAdverb>(adverbText);
            var parsedVerb = ParserHelper.GetEnumValue<ActionVerb>(verbText);
            
            // Ways to handle different objects (items, people, places)

            return new ParsedAction
            {
                Adverb = parsedAdverb,
                Verb = parsedVerb,
                Object = objectText,
                SpokenTo = parsedSubject
            };
        }
    }
}
