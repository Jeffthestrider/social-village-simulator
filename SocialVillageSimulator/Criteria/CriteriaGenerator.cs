using Jochum.SocialVillageSimulator.SocialAspects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jochum.SocialVillageSimulator.Criteria
{
    public interface ICriteriaGenerator
    {
        Func<Character, Character, bool> GetCriteria(string criteriaName);
    }

    public class CriteriaParser
    {
        public bool IsValid(Character speaker, Character spokenTo, string criteriaName)
        {
            var criteriaComponents = criteriaName.Split('.');
            var whoToCheck = criteriaComponents[0].ToLower();
            var whatToCheck = criteriaComponents[1].ToLower();
            var criteriaValue = criteriaComponents[2].ToLower();
            Character who = null;

            if (whoToCheck == "speaker")
            {
                who = speaker;
            }
            else if (whoToCheck == "spokento")
            {
                who = spokenTo;
            }
            else
            {
                throw new ArgumentException($"Criteria expression cannot parse {whoToCheck}.");
            }
            
            if (whatToCheck == "mood")
            {
                Mood parsedMood = GetEnumValue<Mood>(criteriaValue);               
                

                return who.Mood == parsedMood;
            } else
            {
                throw new ArgumentException($"Criteria expression cannot parse {whatToCheck}.");
            }
        }
        
        private T GetEnumValue<T> (string value) where T : struct
        {
            T parsedType;
            bool successful = Enum.TryParse(value, true, out parsedType);
            if (!successful)
            {
                throw new ArgumentException($"Criteria expression cannot parse {value}");
            }

            return parsedType;
        }
    }

    public class StubCriteriaGenerator: ICriteriaGenerator
    {
        private Dictionary<string, Func<Character, Character, bool>> CriteriaDictionary = new Dictionary<string, Func<Character, Character, bool>>();

        private Func<Character, Character, bool> SpeakerIsHappy => (s, st) => s.Mood == Mood.Happy;
        private Func<Character, Character, bool> SpeakerIsSad => (s, st) => s.Mood == Mood.Sad;
        private Func<Character, Character, bool> SpeakerIsAngry => (s, st) => s.Mood == Mood.Angry;

        private Func<Character, Character, bool> SpokenToIsHappy => (s, st) => st.Mood == Mood.Happy;
        private Func<Character, Character, bool> SpokenToIsSad => (s, st) => st.Mood == Mood.Sad;
        private Func<Character, Character, bool> SpokenToIsAngry => (s, st) => st.Mood == Mood.Angry;

        private Func<Character, Character, bool> AlwaysTrue => (s, st) => true;

        public StubCriteriaGenerator()
        {
            CriteriaDictionary.Add("AlwaysTrue", AlwaysTrue);
            CriteriaDictionary.Add("Speaker.IsHappy", SpeakerIsHappy);
            CriteriaDictionary.Add("Speaker.IsSad", SpeakerIsSad);
            CriteriaDictionary.Add("Speaker.IsAngry", SpeakerIsAngry);
            CriteriaDictionary.Add("SpokenTo.IsHappy", SpokenToIsHappy);
            CriteriaDictionary.Add("SpokenTo.IsSad", SpokenToIsSad);
            CriteriaDictionary.Add("SpokenTo.IsAngry", SpokenToIsAngry);
        }

        public Func<Character, Character, bool> GetCriteria(string criteriaName)
        {
            // Do we want to handle this? Or just throw an exception for now. Returning always true/false func would mask problem.
            return CriteriaDictionary[criteriaName];
        }
    }
}