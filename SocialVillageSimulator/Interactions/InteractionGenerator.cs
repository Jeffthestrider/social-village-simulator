using System;
using System.Collections.Generic;
using System.Linq;
using Jochum.SocialVillageSimulator.DataReader;
using Jochum.SocialVillageSimulator.Parsers;

namespace Jochum.SocialVillageSimulator.Interactions
{

    public interface IInteractionGenerator
    {
        Interaction GetResponse(Character speaker, string actionDoneToSpeakerText, Character spokenTo);

        Interaction GetInteraction(Character speaker, string actionText, Character spokenTo);

        Interaction GetInvalidInteraction(Character speaker, Character spokenTo);
    }

    public class InteractionGenerator : IInteractionGenerator
    {
        private ICriteriaParser _criteriaParser;
        private readonly IActionParser _actionParser;
        private IList<Interaction> _interactions;

        public InteractionGenerator(IList<Interaction> interactions, ICriteriaParser criteriaParser, IActionParser actionParser)
        {
            _interactions = interactions;

            _criteriaParser = criteriaParser;
            _actionParser = actionParser;
        }

        private IList<Interaction> GetRespondingInteractions(ActionVerb actionVerb)
        {
            var interactionsFilteredByType = _interactions.Where(p => p.ActionText.ToLower().Contains($".{actionVerb.ToString().ToLower()}")).ToList();

            if (!interactionsFilteredByType.Any())
            {
                var invalidInteractions = _interactions.Where(p => p.ActionText.ToLower().Contains($".{ActionVerb.BlanksOut.ToString().ToLower()}")).ToList();

                return invalidInteractions;
            }

            return interactionsFilteredByType;
        }

        public Interaction GetResponse(Character speaker, string actionDoneToSpeakerText, Character spokenTo)
        {

            var actionDoneToSpeaker = _actionParser.GetAction<string>(speaker, spokenTo, actionDoneToSpeakerText);

            var respondingVerb = speaker.ResponseMapper.GetResponseVerb(actionDoneToSpeaker.Verb);

            if (respondingVerb == null)
            {
                return null;
            }

            var possibleRespondingInteractions = GetRespondingInteractions(respondingVerb.Value);

            List<Interaction> validRespondingInteractions = GetInteractionsMatchingAllCriteria(speaker, spokenTo, possibleRespondingInteractions)
                .ToList();
            
            Interaction result;

            if (validRespondingInteractions.Any())
            {
                result = GetRandomResponse(validRespondingInteractions);
            }
            else
            {
                result = GetInvalidInteraction(speaker, spokenTo);
            }

            return result.GetAFilledInInteraction(speaker, spokenTo);
        }

        public Interaction GetInteraction(Character speaker, string actionText, Character spokenTo)
        {
            var possibleInteractions = _interactions.Where(p => p.ActionText == actionText).ToList();

            if (possibleInteractions.Any())
            {
                return GetRandomResponse(possibleInteractions).GetAFilledInInteraction(speaker, spokenTo);
            }
            else
            {
                return null;
            }

        }

        public Interaction GetInvalidInteraction(Character speaker, Character spokenTo)
        {
            return GetInteraction(speaker, "SpokenTo.Neutrally.BlanksOut", spokenTo);
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
