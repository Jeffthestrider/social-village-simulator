using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jochum.SocialVillageSimulator.Parsers
{

    public enum ActionVerb
    {
        BlanksOut,
        Greet,
        GreetBack,
        Introduce,
        GetToKnow
        /*
        Invalid,
        Greet,
        Introduce,
        Farewell,
        GetToKnow,
        Information,
        ShareWith,
        Request,
        Give,
        Thank,
        Threaten,*/
    }

    public enum ActionAdverb
    {
        Positively,
        Negatively,
        Neutrally,
        /*
        Positive,
        Neutral,
        Negative,
        DetailedAnswer,
        BriefAnswer,
        RefuseAnswer,
        DoNotHaveAnswer,*/
    }

    public class Action<TObject>
    {
        public Character SpokenTo { get; set; }
        public ActionVerb Verb { get; set; }
        public ActionAdverb Adverb { get; set; }
        public TObject Object { get; set; }
    }

    public interface IActionParser
    {

        Action<TObject> GetAction<TObject>(Character speaker, Character spokenTo, string actionText);

    }

    public class ActionParser : IActionParser
    {
        public Action<TObject> GetAction<TObject>(Character speaker, Character spokenTo, string actionText)
        {
            var actionComponents = actionText.Split('.');

            var subjectText = actionComponents[0].ToLower();
            var adverbText = actionComponents[1].ToLower();
            var verbText = actionComponents[2].ToLower();
            string objectText = string.Empty;
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

            return new Action<TObject>
            {
                Adverb = parsedAdverb,
                Verb = parsedVerb,
                Object = default(TObject),
                SpokenTo = parsedSubject
            };
        }
    }
}
