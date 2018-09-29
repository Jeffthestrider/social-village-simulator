using System.Collections.Generic;

namespace Jochum.SocialVillageSimulator.Interactions
{
    public class Interaction
    {
        public string Name { get; set; }
        public string Dialogue { get; set; }
        public string BodyLanguage { get; set; }
        public IList<IList<string>> InteractionCriteriaExpressions { get; set; }
        public InteractionType InteractionType { get; set; }
        public InteractionCategory InteractionCategory { get; set; }

        public Interaction GetAFilledInInteraction(Character speaker, Character spokenTo)
        {
            return new Interaction
            {
                Name = Name,
                BodyLanguage = StringTemplateReplacer.FillInTemplate(speaker, BodyLanguage, spokenTo),
                Dialogue = StringTemplateReplacer.FillInTemplate(speaker, Dialogue, spokenTo),
                InteractionCriteriaExpressions = InteractionCriteriaExpressions,
                InteractionType = InteractionType,
                InteractionCategory = InteractionCategory
            };
        }
    }
}