using System.Collections.Generic;
using Jochum.SocialVillageSimulator.GameObjects;

namespace Jochum.SocialVillageSimulator.Interactions
{
    public class Interaction
    {
        public string Name { get; set; }
        public string Dialogue { get; set; }
        public string BodyLanguage { get; set; }
        public ParsedAction Action { get; set; }
        public IList<IList<string>> InteractionCriteriaExpressions { get; set; }

        public Interaction GetAFilledInInteraction(IStringTemplateReplacer replacer, Character speaker, Character spokenTo)
        {
            return new Interaction
            {
                Name = Name,
                BodyLanguage = replacer.FillInTemplate(Action, speaker, BodyLanguage, spokenTo),
                Dialogue = replacer.FillInTemplate(Action, speaker, Dialogue, spokenTo),
                Action = Action,
                InteractionCriteriaExpressions = InteractionCriteriaExpressions
            };
        }
    }
}