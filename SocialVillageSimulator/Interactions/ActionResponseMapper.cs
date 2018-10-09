using System.Collections.Generic;
using Jochum.SocialVillageSimulator.Parsers;

namespace Jochum.SocialVillageSimulator.Interactions
{
    public class ActionResponseMapper : IActionResponseMapper
    {
        public IList<ActionVerb> GetResponseVerbs(ActionVerb verb)
        {
            switch (verb)
            {
                case ActionVerb.Greet:
                    return new List<ActionVerb> {ActionVerb.GreetBack};
                case ActionVerb.GetToKnow:
                    return new List<ActionVerb> { ActionVerb.Introduce };
                case ActionVerb.RequestItemType:
                    return new List<ActionVerb> { ActionVerb.GiveItemType, ActionVerb.RefuseItemType, ActionVerb.DontHaveItemType };
                default:
                    return new List<ActionVerb>();
            }
        }
    }
}