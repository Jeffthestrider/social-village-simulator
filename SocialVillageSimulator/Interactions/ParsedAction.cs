using Jochum.SocialVillageSimulator.GameObjects;

namespace Jochum.SocialVillageSimulator.Interactions
{
    public class ParsedAction
    {
        public Character SpokenTo { get; set; }
        public ActionVerb Verb { get; set; }
        public ActionAdverb Adverb { get; set; }
        public string Object { get; set; }
    }
}