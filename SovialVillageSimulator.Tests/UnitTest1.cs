using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jochum.SocialVillageSimulator.Criteria;
using Jochum.SocialVillageSimulator;
using Jochum.SocialVillageSimulator.SocialAspects;

namespace SovialVillageSimulator.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
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
    }
}

