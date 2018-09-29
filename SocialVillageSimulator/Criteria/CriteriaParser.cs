using Jochum.SocialVillageSimulator.SocialAspects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jochum.SocialVillageSimulator.Criteria
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
                var parsedMood = GetEnumValue<Mood>(criteriaValue);               
                
                return who.Mood == parsedMood;
            } else
            {
                throw new ArgumentException($"Criteria expression cannot parse {whatToCheck}.");
            }
        }
        
        private TEnum GetEnumValue<TEnum> (string value) where TEnum : struct
        {
            TEnum parsedType;
            var successful = Enum.TryParse(value, true, out parsedType);
            if (!successful)
            {
                throw new ArgumentException($"Criteria expression cannot parse {value}");
            }

            return parsedType;
        }
    }
}