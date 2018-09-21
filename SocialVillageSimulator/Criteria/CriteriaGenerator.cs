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
