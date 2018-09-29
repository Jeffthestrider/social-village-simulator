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
        private ICriteriaParser _criteriaParser;
        private IList<Interaction> _interactions;

        public InteractionGenerator(IList<Interaction> interactions, ICriteriaParser criteriaParser)
        {
            _interactions = interactions;

            _criteriaParser = criteriaParser;
        }

        private IList<Interaction> GetPossibleInteractions(InteractionType interactionType)
        {
            var interactionsFilteredByType = _interactions.Where(p => p.InteractionType == interactionType).ToList();

            if (!interactionsFilteredByType.Any())
            {
                var invalidInteractions = _interactions.Where(p => p.InteractionType == InteractionType.Invalid).ToList();

                return invalidInteractions;
            }

            return interactionsFilteredByType;
        }

        public Interaction GetResponse(Character speaker, Interaction interaction, Character spokenTo)
        {
            // TODO: This needs a way to get interactions not by speaker's interaction type but by some way the spoken would respond.

            Interaction result = GetInteraction(speaker, interaction.InteractionType, spokenTo);

            return result.GetAFilledInInteraction(speaker, spokenTo);
        }

        public Interaction GetInteraction(Character speaker, InteractionType interactionType, Character spokenTo)
        {
            var possibleInteractions = GetPossibleInteractions(interactionType);
            List<Interaction> validInteractions = GetInteractionsMatchingAllCriteria(speaker, spokenTo, possibleInteractions)
                .ToList();

            Interaction result;

            if (validInteractions.Any())
            {
                result = GetRandomResponse(validInteractions);
            }
            else
            {
                result = GetInteraction(speaker, InteractionType.Invalid, spokenTo);
            }

            return result;
        }

        private IEnumerable<Interaction> GetInteractionsMatchingAllCriteria(Character speaker, Character spokenTo, 
            IList<Interaction> possibleInteractions)
        {
            return possibleInteractions
                .Where(criteria =>
                    criteria.InteractionCriteriaExpressions.Any(listOfAndCriteria => listOfAndCriteria.All(expression =>
                        _criteriaParser.IsValid(speaker, spokenTo, expression))));
        }

        private T GetRandomResponse<T>(IList<T> list)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));

            return list[MasterRandom.Rand.Next(list.Count)];
        }
    }
}
