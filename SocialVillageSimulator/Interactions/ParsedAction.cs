using Jochum.SocialVillageSimulator.GameObjects;

namespace Jochum.SocialVillageSimulator.Interactions
{
    public class ParsedAction
    {
        public ActionVerb Verb { get; set; }
        public ActionAdverb Adverb { get; set; }
        public string Object { get; set; }
    }
}