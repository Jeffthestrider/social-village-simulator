using Jochum.SocialVillageSimulator.Parsers;

namespace Jochum.SocialVillageSimulator.Interactions
{
    public class ActionResponseMapper : IActionResponseMapper
    {
        public ActionVerb? GetResponseVerb(ActionVerb verb)
        {
            switch (verb)
            {
                case ActionVerb.Greet:
                    return ActionVerb.GreetBack;
                case ActionVerb.GetToKnow:
                    return ActionVerb.Introduce;
                default:
                    return null;
            }
        }
    }
}