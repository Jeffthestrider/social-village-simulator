using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jochum.SocialVillageSimulator.Criteria;
using Jochum.SocialVillageSimulator;
using Jochum.SocialVillageSimulator.SocialAspects;

namespace SovialVillageSimulator.Tests
{
    [TestClass]
    public class CriteriaParserTests
    {
        [TestMethod]
        public void SpeakerIsHappyIsTrue()
        {
            var parser = new CriteriaParser();

            var s = new Character(null)
            {
                Mood = Mood.Happy
            };
            var st = new Character(null)
            {
                Mood = Mood.Sad
            };

            var result = parser.IsValid(s, st, "Speaker.Mood.Happy");

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void SpokenToIsMelancholyIsTrue()
        {
            var parser = new CriteriaParser();

            var s = new Character(null)
            {
                Mood = Mood.Happy
            };
            var st = new Character(null)
            {
                Mood = Mood.Melancholy
            };

            var result = parser.IsValid(s, st, "SpokenTo.Mood.Melancholy");

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void SpokenToIsAngryIsFalse()
        {
            var parser = new CriteriaParser();

            var s = new Character(null)
            {
                Mood = Mood.Happy
            };
            var st = new Character(null)
            {
                Mood = Mood.Melancholy
            };

            var result = parser.IsValid(s, st, "SpokenTo.Mood.Angry");

            Assert.IsFalse(result);

        }
    }
}

