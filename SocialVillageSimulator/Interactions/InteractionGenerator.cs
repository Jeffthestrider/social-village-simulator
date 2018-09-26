using System;
using System.Collections.Generic;
using System.Linq;
using Jochum.SocialVillageSimulator.Criteria;
using Jochum.SocialVillageSimulator.DataReader;

namespace Jochum.SocialVillageSimulator.Interactions
{

    public interface IInteractionGenerator
    {
        Interaction GetResponse(Character speaker, Interaction interaction, Character spokenTo);
        
        Interaction GetInteraction(Character speaker, InteractionType interactionType, Character spokenTo);
    }

    public class InteractionGenerator : IInteractionGenerator
    {
        IGameDataReader _dataReader;

        public InteractionGenerator(IGameDataReader dataReader)
        {
            _dataReader = dataReader;
            var interactions = _dataReader.GetGameData<Interaction>();
        }

        private InteractionCriteria[] GetPossibleInteractions(InteractionType interactionType)
        {
            var interactions = _dataReader.GetGameData<Interaction>();
            var interactionsFilteredByType = interactions.Where(p => p.InteractionType == interactionType);

            if (interactionsFilteredByType.Count() == 0)
            {
                return InteractionList.CannotHandleInteractions;
            }

            //return interactionsFilteredByType.ToArray();

            switch (interactionType)
            {
                case InteractionType.Greet:
                    return InteractionList.GreetInteractions;

                case InteractionType.Introduce:
                    return InteractionList.IntroduceInteractions;

                default:
                    return InteractionList.CannotHandleInteractions;
            }
        }

        private InteractionCriteria[] GetPossibleResponses(InteractionType interactionType)
        {
            switch (interactionType)
            {
                case InteractionType.Greet:
                    return InteractionList.GreetBackInteractions;

                case InteractionType.Introduce:
                    return InteractionList.IntroduceInteractions;

                default:
                    return InteractionList.CannotHandleInteractions;
            }
        }

        public Interaction GetResponse(Character speaker, Interaction interaction, Character spokenTo)
        {
            var validResponses = GetPossibleResponses(interaction.InteractionType).Where(criteria => criteria.IsValid(speaker, spokenTo)).SelectMany(r => r.Interactions).ToArray();
            var result = GetRandomResponse(validResponses);

            result.BodyLanguage = StringTemplateReplacer.FillInTemplate(speaker, result.BodyLanguage, spokenTo);
            result.Dialogue = StringTemplateReplacer.FillInTemplate(speaker, result.Dialogue, spokenTo);

            return result;
        }


        public Interaction GetInteraction(Character speaker, InteractionType interactionType, Character spokenTo)
        {
            var validResponses = GetPossibleInteractions(interactionType).Where(criteria => criteria.IsValid(speaker, spokenTo)).SelectMany(r => r.Interactions).ToArray();
            var result = GetRandomResponse(validResponses);

            result.BodyLanguage = StringTemplateReplacer.FillInTemplate(speaker, result.BodyLanguage, spokenTo);
            result.Dialogue = StringTemplateReplacer.FillInTemplate(speaker, result.Dialogue, spokenTo);

            return result;
        }

        private Interaction GetRandomResponse(Interaction[] interactions)
        {
            if (interactions == null) throw new ArgumentNullException(nameof(interactions));

            var nextResponseIndex = MasterRandom.Rand.Next(interactions.Length);

            return interactions[nextResponseIndex];
        }
    }
}
