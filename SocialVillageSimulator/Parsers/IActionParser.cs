using Jochum.SocialVillageSimulator.GameObjects;

namespace Jochum.SocialVillageSimulator.Parsers
{
    public interface IActionParser
    {

        Interactions.ParsedAction GetAction(string actionText);

    }
}