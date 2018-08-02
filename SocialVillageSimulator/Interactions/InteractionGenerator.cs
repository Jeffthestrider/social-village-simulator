using System;
using System.Collections.Generic;
using System.Linq;
using Jochum.SocialVillageSimulator.Criteria;

namespace Jochum.SocialVillageSimulator.Interactions
{
    public static class InteractionGenerator
    {
        private static InteractionCriteria[] GetPossibleInteractions(InteractionType interactionType)
        {
            switch (interactionType)
            {
                case InteractionType.Greet:
                    return InteractionList.GreetInteractions;

                default:
                    return InteractionList.CannotHandleInteractions;
            }
        }

        private static InteractionCriteria[] GetPossibleResponses(InteractionType interactionType)
        {
            switch (interactionType)
            {
                case InteractionType.Greet:
                    return InteractionList.GreetBackInteractions;

                default:
                    return InteractionList.CannotHandleInteractions;
            }
        }

        public static Interaction GetResponse(Character speaker, Interaction interaction, Character spokenTo)
        {
            var validResponses = GetPossibleResponses(interaction.InteractionType).Where(criteria => criteria.IsValid(speaker, spokenTo)).SelectMany(r => r.Interactions).ToArray();
            var result = GetRandomResponse(validResponses);

            result.BodyLanguage = StringTemplateReplacer.FillInTemplate(speaker, result.BodyLanguage, spokenTo);
            result.Dialogue = StringTemplateReplacer.FillInTemplate(speaker, result.Dialogue, spokenTo);

            return result;
        }


        public static Interaction GetInteraction(Character speaker, InteractionType interactionType, Character spokenTo)
        {
            var validResponses = GetPossibleInteractions(interactionType).Where(criteria => criteria.IsValid(speaker, spokenTo)).SelectMany(r => r.Interactions).ToArray();
            var result = GetRandomResponse(validResponses);

            result.BodyLanguage = StringTemplateReplacer.FillInTemplate(speaker, result.BodyLanguage, spokenTo);
            result.Dialogue = StringTemplateReplacer.FillInTemplate(speaker, result.Dialogue, spokenTo);

            return result;
        }

        private static Interaction GetRandomResponse(Interaction[] interactions)
        {
            if (interactions == null) throw new ArgumentNullException(nameof(interactions));

            var nextResponseIndex = MasterRandom.Rand.Next(interactions.Length);

            return interactions[nextResponseIndex];
        }
    }
}
