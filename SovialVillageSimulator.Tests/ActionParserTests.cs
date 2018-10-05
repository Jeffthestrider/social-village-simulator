using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jochum.SocialVillageSimulator;
using Jochum.SocialVillageSimulator.Interactions;
using Jochum.SocialVillageSimulator.Parsers;
using Jochum.SocialVillageSimulator.SocialAspects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SovialVillageSimulator.Tests
{
    [TestClass]
    public class ActionParserTests
    {

        [TestMethod]
        public void GetAction()
        {
            var name = "Tom";
            var parser = new ActionParser();
            Character speaker = new Character(null, null)
            {
                Gender = Gender.Male,
                IsPc = false,
                Mood = Mood.Happy,
                Name = "Frank"
            };
            Character spokenTo = new Character(null, null)
            {
                Gender = Gender.Male,
                IsPc = false,
                Mood = Mood.Angry,
                Name = name
            };

            var result = parser.GetAction<string>(speaker, spokenTo, "SpokenTo.Neutrally.Greet");

            Assert.AreEqual(ActionAdverb.Neutrally, result.Adverb);
            Assert.AreEqual(ActionVerb.Greet, result.Verb);
            Assert.AreEqual(name, result.SpokenTo.Name);
        }

    }
}
