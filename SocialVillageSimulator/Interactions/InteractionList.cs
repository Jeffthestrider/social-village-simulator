using System.Collections.Generic;
using Jochum.SocialVillageSimulator.Criteria;

namespace Jochum.SocialVillageSimulator.Interactions
{
    public static partial class InteractionList
    {

        public static InteractionCriteria[] GreetInteractions => new []
        {
            new InteractionCriteria(GenericHappyGreetings,
                GenericCriteriaGenerator.CreateList(MoodCriteriaGenerator.SpeakerIsHappy)),
            new InteractionCriteria(GenericDefaultGreetings,
                GenericCriteriaGenerator.CreateList(GenericCriteriaGenerator.AlwaysTrue)),
        };

        public static IList<Interaction> GenericHappyGreetings => new List<Interaction>
        {
            new Interaction
            {
                BodyLanguage = "{MyName} waves.",
                Dialogue = "Hey {Name}!",
                InteractionType = InteractionType.Greet,
                InteractionCategory = InteractionCategory.Positive
            },
            new Interaction
            {
                BodyLanguage = "{MyName} waves.",
                Dialogue = "Hi {Name}!",
                InteractionType = InteractionType.Greet,
                InteractionCategory = InteractionCategory.Positive
            },
            new Interaction
            {
                BodyLanguage = "{MyName} approaches {Name}.",
                Dialogue = "Hey hey!",
                InteractionType = InteractionType.Greet,
                InteractionCategory = InteractionCategory.Positive
            }
        };
        public static IList<Interaction> GenericDefaultGreetings => new List<Interaction>
        {
            new Interaction
            {
                BodyLanguage = "{MyName} clears their throat.",
                Dialogue = "Hello, {Name}, do you have a second?",
                InteractionType = InteractionType.Greet,
                InteractionCategory = InteractionCategory.Neutral
            },
            new Interaction
            {
                BodyLanguage = "{MyName} approaches.",
                Dialogue = "Hi {Name}.",
                InteractionType = InteractionType.Greet,
                InteractionCategory = InteractionCategory.Neutral
            },
        };

        public static IList<Interaction> GenericHappyResponses => new List<Interaction>
        {
            new Interaction
            {
                BodyLanguage = "{MyName} smiles.",
                Dialogue = "Hello {Name}!",
                InteractionType = InteractionType.Greet,
                InteractionCategory = InteractionCategory.Positive
            },
            new Interaction
            {
                BodyLanguage = "{MyName} smiles.",
                Dialogue = "Hi {Name}!",
                InteractionType = InteractionType.Greet,
                InteractionCategory = InteractionCategory.Positive
            },
            new Interaction
            {
                BodyLanguage = "{MyName} smiles and waves.",
                Dialogue = "Yo {Name}, good to see you!",
                InteractionType = InteractionType.Greet,
                InteractionCategory = InteractionCategory.Positive
            }
        };
        public static IList<Interaction> GenericDefaultResponses => new List<Interaction>
        {
            new Interaction
            {
                BodyLanguage = "{MyName} looks up.",
                Dialogue = "Hey {Name}.",
                InteractionType = InteractionType.Greet,
                InteractionCategory = InteractionCategory.Neutral
            },
            new Interaction
            {
                BodyLanguage = "{MyName} glances up.",
                Dialogue = "Hi {Name}.",
                InteractionType = InteractionType.Greet,
                InteractionCategory = InteractionCategory.Neutral
            }
        };
        public static IList<Interaction> GenericAngryResponses => new List<Interaction>
        {
            new Interaction
            {
                BodyLanguage = "{MyName} gives a rude gesture.",
                Dialogue = "Go fuck yourself, {Name}.",
                InteractionType = InteractionType.Greet,
                InteractionCategory = InteractionCategory.Negative
            }
        };
        public static IList<Interaction> HappyAndNoticingUpsetResponses => new List<Interaction>
        {
            new Interaction
            {
                BodyLanguage = "{MyName} starts to wave but stops.",
                Dialogue = "Hey {Name}, how are- woah are you okay?",
                InteractionType = InteractionType.Greet,
                InteractionCategory = InteractionCategory.Neutral
            }
        };

        public static InteractionCriteria[] GreetBackInteractions =
        {
            new InteractionCriteria(GenericHappyResponses,
                GenericCriteriaGenerator.CreateList(MoodCriteriaGenerator.SpeakerIsHappy)),
            new InteractionCriteria(GenericDefaultResponses,
                GenericCriteriaGenerator.CreateList(GenericCriteriaGenerator.AlwaysTrue)),
            new InteractionCriteria(GenericAngryResponses,
                GenericCriteriaGenerator.CreateList(MoodCriteriaGenerator.SpeakerIsAngry)),
            new InteractionCriteria(HappyAndNoticingUpsetResponses,
                GenericCriteriaGenerator.CreateList(MoodCriteriaGenerator.SpeakerIsHappy, MoodCriteriaGenerator.SpokenToIsAngry)),
            new InteractionCriteria(HappyAndNoticingUpsetResponses,
                GenericCriteriaGenerator.CreateList(MoodCriteriaGenerator.SpeakerIsHappy, MoodCriteriaGenerator.SpokenToIsSad))
        };


        public static IList<Interaction> CannotHandleResponse => new List<Interaction>
        {
            new Interaction
            {
                BodyLanguage = "{MyName}'s face goes blank.",
                Dialogue = $"I'm sorry, my responses are limited. You must ask the right questions.",
                InteractionType = InteractionType.Invalid,
                InteractionCategory = InteractionCategory.DoNotHaveAnswer
            }
        };

        public static InteractionCriteria[] CannotHandleInteractions => new []
        {
            new InteractionCriteria(InteractionList.CannotHandleResponse,
                GenericCriteriaGenerator.CreateList(GenericCriteriaGenerator.AlwaysTrue)),
        };
        
        public static IList<Interaction> GenericHappyIntroduceInteractions => new List<Interaction>
        {
            new Interaction
            {
                BodyLanguage = "{MyGenderSubject} bows and flourishes {MyGenderPossessiveAdjective} hand. Well, you assume it's {MyGenderPossessivePronoun}. {MyGenderSubject} steals hands all the time. You don't really like {MyGenderObject}.",
                Dialogue = "My name is {MyName}.",
                InteractionType = InteractionType.Introduce,
                InteractionCategory = InteractionCategory.Positive
            },
        };
        public static IList<Interaction> GenericDefaultIntroduceInteractions => new List<Interaction>
        {
            new Interaction
            {
                BodyLanguage = "{MyName} looks at {NameOrYou}.",
                Dialogue = "My name is {MyName}. I am a {MyJob}",
                InteractionType = InteractionType.Introduce,
                InteractionCategory = InteractionCategory.Neutral
            },
        };

        public static InteractionCriteria[] IntroduceInteractions =
        {
            new InteractionCriteria(GenericHappyIntroduceInteractions,
                GenericCriteriaGenerator.CreateList(MoodCriteriaGenerator.SpeakerIsHappy)),
            new InteractionCriteria(GenericDefaultIntroduceInteractions,
                GenericCriteriaGenerator.CreateList(GenericCriteriaGenerator.AlwaysTrue)),
        };
    }
}
