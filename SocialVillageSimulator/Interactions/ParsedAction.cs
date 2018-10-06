namespace Jochum.SocialVillageSimulator.Interactions
{
    public class Action<TObject>
    {
        public Character SpokenTo { get; set; }
        public ActionVerb Verb { get; set; }
        public ActionAdverb Adverb { get; set; }
        public TObject Object { get; set; }
    }
}