using System;
using System.Collections.Generic;
using System.Linq;
using Jochum.SocialVillageSimulator.DataReader;
using Jochum.SocialVillageSimulator.GameObjects;
using Jochum.SocialVillageSimulator.Parsers;

namespace Jochum.SocialVillageSimulator.Interactions
{

    public interface IInteractionGenerator
    {
        Interaction GetResponse(Character speaker, ParsedAction actionDoneToSpeaker, Character spokenTo);

        Interaction GetInteraction(Character speaker, ParsedAction action, Character spokenTo);

        Interaction GetInvalidInteraction(Character speaker, Character spokenTo);
    }

    public class InteractionGenerator : IInteractionGenerator
    {
        private ICriteriaParser _criteriaParser;
        private readonly IStringTemplateReplacer _stringTemplateReplacer;
        private IList<Interaction> _interactions;

        public InteractionGenerator(IList<Interaction> interactions, 
            ICriteriaParser criteriaParser, 
            IStringTemplateReplacer stringTemplateReplacer)
        {
            _interactions = interactions;

            _criteriaParser = criteriaParser;
            _stringTemplateReplacer = stringTemplateReplacer;
        }

        private IList<Interaction> GetRespondingInteractions(ActionVerb actionVerb)
        {
            var interactionsFilteredByType = _interactions.Where(p => p.Action.Verb == actionVerb).ToList();

            if (!interactionsFilteredByType.Any())
            {
                var invalidInteractions = _interactions.Where(p => p.Action.Verb == ActionVerb.BlanksOut).ToList();

                return invalidInteractions;
            }

            return interactionsFilteredByType;
        }

        public Interaction GetResponse(Character speaker, ParsedAction actionDoneToSpeaker, Character spokenTo)
        {
            var respondingVerbs = speaker.ResponseMapper.GetResponseVerbs(actionDoneToSpeaker.Verb);

            if (!respondingVerbs.Any())
            {
                return null;
            }

            var possibleRespondingInteractions = respondingVerbs.SelectMany(GetRespondingInteractions).ToList();

            List<Interaction> validRespondingInteractions = GetInteractionsMatchingAllCriteria(speaker, spokenTo, possibleRespondingInteractions)
                .ToList();

            validRespondingInteractions =
                GetInteractionsPossibleWithAction(speaker, validRespondingInteractions, actionDoneToSpeaker);

            Interaction result;

            if (validRespondingInteractions.Any())
            {
                result = GetRandomResponse(validRespondingInteractions);
                result.Action.Object = actionDoneToSpeaker.Object;
            }
            else
            {
                result = GetInvalidInteraction(speaker, spokenTo);
            }

            return result.GetAFilledInInteraction(_stringTemplateReplacer, speaker, spokenTo);
        }

        private List<Interaction> GetInteractionsPossibleWithAction(Character speaker,
            List<Interaction> interactions, 
            ParsedAction actionDoneToSpeaker)
        {
            if (actionDoneToSpeaker.Verb == ActionVerb.RequestItemType)
            {
                ItemType itemType;
                var isSuccessful = Enum.TryParse(actionDoneToSpeaker.Object, true, out itemType);

                if (!isSuccessful)
                {
                    throw new ArgumentNullException(nameof(actionDoneToSpeaker.Object), "Cannot parse item type.");
                }

                if (speaker.GetPossessionsOfType(itemType).Any())
                {
                    return interactions.Where(p => p.Action.Verb != ActionVerb.DontHaveItemType ).ToList();
                }
                else
                {
                    return interactions.Where(p => p.Action.Verb == ActionVerb.DontHaveItemType).ToList();
                }
            }

            return interactions;
        }

        public Interaction GetInteraction(Character speaker, ParsedAction action, Character spokenTo)
        {
            var possibleInteractions = _interactions.Where(p => p.Action.Adverb == action.Adverb 
                                                                && p.Action.Verb == action.Verb).ToList();

            if (possibleInteractions.Any())
            {
                return GetRandomResponse(possibleInteractions).GetAFilledInInteraction(_stringTemplateReplacer, speaker, spokenTo);
            }
            else
            {
                return null;
            }

        }

        public Interaction GetInvalidInteraction(Character speaker, Character spokenTo)
        {
            return GetInteraction(speaker, new ParsedAction {Adverb = ActionAdverb.Neutrally, Verb = ActionVerb.BlanksOut}, spokenTo);
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

            return list[SeedRandom.Rand.Next(list.Count)];
        }
    }
}
