namespace Jochum.SocialVillageSimulator.Parsers
{
    public interface IActionParser
    {

        Interactions.Action<TObject> GetAction<TObject>(Character speaker, Character spokenTo, string actionText);

    }
}