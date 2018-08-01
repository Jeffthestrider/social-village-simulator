using System.Collections.Generic;
using System.Linq;
using Jochum.SocialVillageSimulator.Criteria;

namespace Jochum.SocialVillageSimulator.Responses
{
    public static class ResponseGenerator
    {
        public static IList<Response> CannotHandleResponse => new List<Response>
        {
            new Response
            {
                BodyLanguage = "{MyName}'s face goes blank.",
                Dialogue = $"I'm sorry, my responses are limited. You must ask the right questions.",
                ResponseType = ResponseType.DoNotHaveAnswer
            }
        };

        public static ResponseCriteria[] CannotHandleResponses =
        {
            new ResponseCriteria(CannotHandleResponse,
                GenericCriteriaGenerator.CreateList(GenericCriteriaGenerator.AlwaysTrue)),
        };

        private static ResponseCriteria[] GetResponseCriteria<T>(Interaction<T> interaction)
        {
            switch (interaction.InteractionType)
            {
                case InteractionType.Greet:
                    return Responses.GreetResponses;

                default:
                    return CannotHandleResponses;
            }
        }

        public static Response GetResponse<T>(Character speaker, Interaction<T> interaction, Character spokenTo)
        {
            var validResponses = GetResponseCriteria<T>(interaction).Where(r => r.IsValid(speaker, spokenTo)).SelectMany(r => r.Responses).ToArray();
            var result = GetRandomResponse(validResponses);

            result.BodyLanguage = StringTemplateReplacer.FillInTemplate(speaker, result.BodyLanguage, spokenTo);
            result.Dialogue = StringTemplateReplacer.FillInTemplate(speaker, result.Dialogue, spokenTo);

            return result;
        }

        private static Response GetRandomResponse(Response[] responses)
        {
            var nextResponseIndex = MasterRandom.Rand.Next(responses.Length);

            return responses[nextResponseIndex];
        }
    }
}
