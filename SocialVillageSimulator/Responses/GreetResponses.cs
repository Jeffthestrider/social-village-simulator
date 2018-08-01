using System.Collections.Generic;
using Jochum.SocialVillageSimulator.Criteria;

namespace Jochum.SocialVillageSimulator.Responses
{
    public static partial class Responses
    {
        public static IList<Response> GenericHappyResponses => new List<Response>
        {
            new Response
            {
                BodyLanguage = "{MyName} smiles.",
                Dialogue = "Hello {Name}!",
                ResponseType = ResponseType.Positive
            },
            new Response
            {
                BodyLanguage = "{MyName} smiles.",
                Dialogue = "Hi {Name}!",
                ResponseType = ResponseType.Positive
            },
            new Response
            {
                BodyLanguage = "{MyName} smiles and waves.",
                Dialogue = "Yo {Name}, good to see you!",
                ResponseType = ResponseType.Positive
            }
        };
        public static IList<Response> GenericDefaultResponses => new List<Response>
        {
            new Response
            {
                BodyLanguage = "{MyName} looks up.",
                Dialogue = "Hey {Name}.",
                ResponseType = ResponseType.Neutral
            },
            new Response
            {
                BodyLanguage = "{MyName} glances up.",
                Dialogue = "Hi {Name}.",
                ResponseType = ResponseType.Neutral
            }
        };
        public static IList<Response> GenericAngryResponses => new List<Response>
        {
            new Response
            {
                BodyLanguage = "{MyName} gives a rude gesture.",
                Dialogue = "Go fuck yourself, {Name}.",
                ResponseType = ResponseType.Negative
            }
        };
        public static IList<Response> HappyAndNoticingUpsetResponses => new List<Response>
        {
            new Response
            {
                BodyLanguage = "{MyName} starts to wave but stops.",
                Dialogue = "Hey {Name}, how are- woah are you okay?",
                ResponseType = ResponseType.Neutral
            }
        };

        public static ResponseCriteria[] GreetResponses =
        {
            new ResponseCriteria(GenericHappyResponses,
                GenericCriteriaGenerator.CreateList(MoodCriteriaGenerator.SpeakerIsHappy)),
            new ResponseCriteria(GenericDefaultResponses,
                GenericCriteriaGenerator.CreateList(GenericCriteriaGenerator.AlwaysTrue)),
            new ResponseCriteria(GenericAngryResponses,
                GenericCriteriaGenerator.CreateList(MoodCriteriaGenerator.SpeakerIsAngry)),
            new ResponseCriteria(HappyAndNoticingUpsetResponses,
                GenericCriteriaGenerator.CreateList(MoodCriteriaGenerator.SpeakerIsHappy, MoodCriteriaGenerator.SpokenToIsAngry)),
            new ResponseCriteria(HappyAndNoticingUpsetResponses,
                GenericCriteriaGenerator.CreateList(MoodCriteriaGenerator.SpeakerIsHappy, MoodCriteriaGenerator.SpokenToIsSad))
        };
    }
}
