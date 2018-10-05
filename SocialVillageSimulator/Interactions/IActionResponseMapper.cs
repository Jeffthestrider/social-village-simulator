using Jochum.SocialVillageSimulator.Parsers;

namespace Jochum.SocialVillageSimulator.Interactions
{
    public interface IActionResponseMapper
    {
        ActionVerb? GetResponseVerb(ActionVerb verb);
    }
}