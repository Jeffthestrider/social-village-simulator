namespace Jochum.SocialVillageSimulator.Interactions
{
    public class Interaction
    {
        public string Dialogue { get; set; }
        public string BodyLanguage { get; set; }
        public InteractionType InteractionType {get;set; }
        public InteractionCategory InteractionCategory { get; set; }
    }
}