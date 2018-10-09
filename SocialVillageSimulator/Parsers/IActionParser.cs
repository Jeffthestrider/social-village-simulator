using Jochum.SocialVillageSimulator.GameObjects;

namespace Jochum.SocialVillageSimulator.Parsers
{
    public interface IActionParser
    {

        Interactions.ParsedAction GetAction(Character speaker, Character spokenTo, string actionText);

    }
}