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
        public ParsedAction GetAction(string actionText)
        {
            var actionComponents = actionText.ToLower().Split('.');
            
            var adverbText = actionComponents[0];
            var verbText = actionComponents[1];
            string objectText = null;
            if (actionComponents.Length == 3)
                objectText = actionComponents[2];
            
            var parsedAdverb = ParserHelper.GetEnumValue<ActionAdverb>(adverbText);
            var parsedVerb = ParserHelper.GetEnumValue<ActionVerb>(verbText);
            
            return new ParsedAction
            {
                Adverb = parsedAdverb,
                Verb = parsedVerb,
                Object = objectText
            };
        }
    }
}
