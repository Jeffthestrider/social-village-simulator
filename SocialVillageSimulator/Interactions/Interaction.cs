using System.Collections.Generic;

namespace Jochum.SocialVillageSimulator.Interactions
{
    public class Interaction
    {
        public string Name { get; set; }
        public string Dialogue { get; set; }
        public string BodyLanguage { get; set; }
        public InteractionType InteractionType {get;set; }
        public InteractionCategory InteractionCategory { get; set; }
    }
}