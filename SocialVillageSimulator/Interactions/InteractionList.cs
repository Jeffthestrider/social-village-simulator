using System.Collections.Generic;
using Jochum.SocialVillageSimulator.Criteria;
using System;
using System.Linq;

namespace Jochum.SocialVillageSimulator.Interactions
{
//    public static class InteractionList
//    {
//        private ICriteriaParser _criteriaParser;
//
//        public static InteractionCriteria[] GreetInteractions => new []
//        {
//            new InteractionCriteria(GenericHappyGreetings,
//                CreateList(_criteriaGenerator.GetCriteria("Speaker.IsHappy"))),
//            new InteractionCriteria(GenericDefaultGreetings,
//                CreateList(_criteriaGenerator.GetCriteria("AlwaysTrue"))),
//        };
//
//        public static IList<Interaction> GenericHappyGreetings => new List<Interaction>
//        {
//            new Interaction
//            {
//                BodyLanguage = "{MyName} waves.",
//                Dialogue = "Hey {Name}!",
//                InteractionType = InteractionType.Greet,
//                InteractionCategory = InteractionCategory.Positive
//            },
//            new Interaction
//            {
//                BodyLanguage = "{MyName} waves.",
//                Dialogue = "Hi {Name}!",
//                InteractionType = InteractionType.Greet,
//                InteractionCategory = InteractionCategory.Positive
//            },
//            new Interaction
//            {
//                BodyLanguage = "{MyName} approaches {Name}.",
//                Dialogue = "Hey hey!",
//                InteractionType = InteractionType.Greet,
//                InteractionCategory = InteractionCategory.Positive
//            }
//        };
//        public static IList<Interaction> GenericDefaultGreetings => new List<Interaction>
//        {
//            new Interaction
//            {
//                BodyLanguage = "{MyName} clears their throat.",
//                Dialogue = "Hello, {Name}, do you have a second?",
//                InteractionType = InteractionType.Greet,
//                InteractionCategory = InteractionCategory.Neutral
//            },
//            new Interaction
//            {
//                BodyLanguage = "{MyName} approaches.",
//                Dialogue = "Hi {Name}.",
//                InteractionType = InteractionType.Greet,
//                InteractionCategory = InteractionCategory.Neutral
//            },
//        };
//
//        public static IList<Interaction> GenericHappyResponses => new List<Interaction>
//        {
//            new Interaction
//            {
//                BodyLanguage = "{MyName} smiles.",
//                Dialogue = "Hello {Name}!",
//                InteractionType = InteractionType.Greet,
//                InteractionCategory = InteractionCategory.Positive
//            },
//            new Interaction
//            {
//                BodyLanguage = "{MyName} smiles.",
//                Dialogue = "Hi {Name}!",
//                InteractionType = InteractionType.Greet,
//                InteractionCategory = InteractionCategory.Positive
//            },
//            new Interaction
//            {
//                BodyLanguage = "{MyName} smiles and waves.",
//                Dialogue = "Yo {Name}, good to see you!",
//                InteractionType = InteractionType.Greet,
//                InteractionCategory = InteractionCategory.Positive
//            }
//        };
//        public static IList<Interaction> GenericDefaultResponses => new List<Interaction>
//        {
//            new Interaction
//            {
//                BodyLanguage = "{MyName} looks up.",
//                Dialogue = "Hey {Name}.",
//                InteractionType = InteractionType.Greet,
//                InteractionCategory = InteractionCategory.Neutral
//            },
//            new Interaction
//            {
//                BodyLanguage = "{MyName} glances up.",
//                Dialogue = "Hi {Name}.",
//                InteractionType = InteractionType.Greet,
//                InteractionCategory = InteractionCategory.Neutral
//            }
//        };
//        public static IList<Interaction> GenericAngryResponses => new List<Interaction>
//        {
//            new Interaction
//            {
//                BodyLanguage = "{MyName} gives a rude gesture.",
//                Dialogue = "Go fuck yourself, {Name}.",
//                InteractionType = InteractionType.Greet,
//                InteractionCategory = InteractionCategory.Negative
//            }
//        };
//        public static IList<Interaction> HappyAndNoticingUpsetResponses => new List<Interaction>
//        {
//            new Interaction
//            {
//                BodyLanguage = "{MyName} starts to wave but stops.",
//                Dialogue = "Hey {Name}, how are- woah are you okay?",
//                InteractionType = InteractionType.Greet,
//                InteractionCategory = InteractionCategory.Neutral
//            }
//        };
//
//        public static InteractionCriteria[] GreetBackInteractions =
//        {
//            new InteractionCriteria(GenericHappyResponses,
//                CreateList(_criteriaGenerator.GetCriteria("Speaker.IsHappy"))),
//            new InteractionCriteria(GenericDefaultResponses,
//                CreateList(_criteriaGenerator.GetCriteria("AlwaysTrue"))),
//            new InteractionCriteria(GenericAngryResponses,
//                CreateList(_criteriaGenerator.GetCriteria("Speaker.IsAngry"))),
//            new InteractionCriteria(HappyAndNoticingUpsetResponses,
//                CreateList(_criteriaGenerator.GetCriteria("Speaker.IsHappy"), _criteriaGenerator.GetCriteria("SpokenTo.IsAngry"))),
//            new InteractionCriteria(HappyAndNoticingUpsetResponses,
//                CreateList(_criteriaGenerator.GetCriteria("Speaker.IsHappy"), _criteriaGenerator.GetCriteria("SpokenTo.IsSad")))
//        };
//
//
//        public static IList<Interaction> CannotHandleResponse => new List<Interaction>
//        {
//            new Interaction
//            {
//                BodyLanguage = "{MyName}'s face goes blank.",
//                Dialogue = $"I'm sorry, my responses are limited. You must ask the right questions.",
//                InteractionType = InteractionType.Invalid,
//                InteractionCategory = InteractionCategory.DoNotHaveAnswer
//            }
//        };
//
//        public static InteractionCriteria[] CannotHandleInteractions => new []
//        {
//            new InteractionCriteria(CannotHandleResponse,
//                CreateList(_criteriaGenerator.GetCriteria("AlwaysTrue"))),
//        };
//        
//        public static IList<Interaction> GenericHappyIntroduceInteractions => new List<Interaction>
//        {
//            new Interaction
//            {
//                BodyLanguage = "{MyGenderSubject} bows and flourishes {MyGenderPossessiveAdjective} hand. Well, you assume it's {MyGenderPossessivePronoun}. {MyGenderSubject} steals hands all the time. You don't really like {MyGenderObject}.",
//                Dialogue = "My name is {MyName}.",
//                InteractionType = InteractionType.Introduce,
//                InteractionCategory = InteractionCategory.Positive
//            },
//        };
//        public static IList<Interaction> GenericDefaultIntroduceInteractions => new List<Interaction>
//        {
//            new Interaction
//            {
//                BodyLanguage = "{MyName} looks at {NameOrYou}.",
//                Dialogue = "My name is {MyName}. I am a {MyJob}",
//                InteractionType = InteractionType.Introduce,
//                InteractionCategory = InteractionCategory.Neutral
//            },
//        };
//
//        public static InteractionCriteria[] IntroduceInteractions =
//        {
//            new InteractionCriteria(GenericHappyIntroduceInteractions,
//                CreateList(_criteriaGenerator.GetCriteria("Speaker.IsHappy"))),
//            new InteractionCriteria(GenericDefaultIntroduceInteractions,
//                CreateList(_criteriaGenerator.GetCriteria("AlwaysTrue"))),
//        };
//        
//        private static IList<Func<Character, Character, bool>> CreateList(params Func<Character, Character, bool>[] criteriaList)
//        {
//            return criteriaList.ToList();
//        }
//    }
}
