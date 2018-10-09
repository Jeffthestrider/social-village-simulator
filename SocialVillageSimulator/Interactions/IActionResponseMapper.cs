using System.Collections.Generic;
using Jochum.SocialVillageSimulator.Parsers;

namespace Jochum.SocialVillageSimulator.Interactions
{
    public interface IActionResponseMapper
    {
        IList<ActionVerb> GetResponseVerbs(ActionVerb verb);
    }
}