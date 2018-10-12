using Jochum.SocialVillageSimulator.GameObjects;
using Jochum.SocialVillageSimulator.Interactions;

namespace Jochum.SocialVillageSimulator
{
    public interface IStringTemplateReplacer
    {
        string FillInTemplate(ParsedAction action, Character speaker, string templateString, Character spokenTo);
    }
}