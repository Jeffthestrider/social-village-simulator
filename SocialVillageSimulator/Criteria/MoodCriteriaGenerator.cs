using System;

namespace Jochum.SocialVillageSimulator.Criteria
{
    public static class MoodCriteriaGenerator
    {

        public static Func<Character, Character, bool> SpeakerIsHappy => (s, st) => s.Mood == Mood.Happy;
        public static Func<Character, Character, bool> SpeakerIsSad => (s, st) => s.Mood == Mood.Sad;
        public static Func<Character, Character, bool> SpeakerIsAngry => (s, st) => s.Mood == Mood.Angry;

        public static Func<Character, Character, bool> SpokenToIsHappy => (s, st) => st.Mood == Mood.Happy;
        public static Func<Character, Character, bool> SpokenToIsSad => (s, st) => st.Mood == Mood.Sad;
        public static Func<Character, Character, bool> SpokenToIsAngry => (s, st) => st.Mood == Mood.Angry;
    }
}
