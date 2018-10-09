using System;
using Jochum.SocialVillageSimulator.GameObjects;
using Jochum.SocialVillageSimulator.SocialAspects;

namespace Jochum.SocialVillageSimulator.Parsers
{
    public interface ICriteriaParser
    {
        bool IsValid(Character speaker, Character spokenTo, string criteriaExpression);
    }

    public class CriteriaParser: ICriteriaParser
    {
        public bool IsValid(Character speaker, Character spokenTo, string criteriaExpression)
        {
            if (criteriaExpression.Equals("AlwaysTrue"))
            {
                return true;
            }

            var criteriaComponents = criteriaExpression.Split('.');

            var whoToCheck = criteriaComponents[0].ToLower();
            var whatToCheck = criteriaComponents[1].ToLower();
            var criteriaValue = criteriaComponents[2].ToLower();
            Character who = null;

            if (whoToCheck.Equals("speaker"))
            {
                who = speaker;
            }
            else if (whoToCheck.Equals("spokento"))
            {
                who = spokenTo;
            }
            else
            {
                throw new ArgumentException($"Criteria expression cannot parse {whoToCheck}.");
            }
            
            if (whatToCheck.Equals("mood"))
            {
                var parsedMood = ParserHelper.GetEnumValue<Mood>(criteriaValue);               
                
                return who.Mood == parsedMood;
            } else
            {
                throw new ArgumentException($"Criteria expression cannot parse {whatToCheck}.");
            }
        }
    }
}