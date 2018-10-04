using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jochum.SocialVillageSimulator.SocialAspects
{
    public enum Gender
    {
        Male,
        Female,
        None,
    }
    
    public static class CharacterPropertyExtensions
    {
        public static string ToSubject(this Gender gender)
        {
            switch (gender)
            {
                case Gender.Female:
                    return "she";
                case Gender.Male:
                    return "he";
                default:
                    return "they";
            }
        }
        public static string ToObject(this Gender gender)
        {
            switch (gender)
            {
                case Gender.Female:
                    return "her";
                case Gender.Male:
                    return "him";
                default:
                    return "them";
            }
        }
        public static string ToPossessivePronoun(this Gender gender)
        {
            switch (gender)
            {
                case Gender.Female:
                    return "hers";
                case Gender.Male:
                    return "his";
                default:
                    return "theirs";
            }
        }

        public static string ToPossessiveAdjective(this Gender gender)
        {
            switch (gender)
            {
                case Gender.Female:
                    return "her";
                case Gender.Male:
                    return "his";
                default:
                    return "their";
            }
        }
    }
}
