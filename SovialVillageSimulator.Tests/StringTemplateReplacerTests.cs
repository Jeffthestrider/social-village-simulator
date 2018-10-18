using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jochum.SocialVillageSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SovialVillageSimulator.Tests
{
    [TestClass]
    public class StringTemplateReplacerTests
    {

        [TestMethod]
        public void ReplacesWithAParticle()
        {
            var replacer = new StringTemplateReplacer();

            var template = "a(n) puppy";

            var result = replacer.FillInTemplate(null, null, template, null);

            Assert.AreEqual("a puppy", result);
        }

        [TestMethod]
        public void ReplacesWithAnParticle()
        {
            var replacer = new StringTemplateReplacer();

            var template = "a(n) essay";

            var result = replacer.FillInTemplate(null, null, template, null);

            Assert.AreEqual("an essay", result);
        }

        [TestMethod]
        public void ReplacesWithAParticleInMiddleOfText()
        {
            var replacer = new StringTemplateReplacer();

            var template = "The man was a(n) contractor";

            var result = replacer.FillInTemplate(null, null, template, null);

            Assert.AreEqual("The man was a contractor", result);
        }


        [TestMethod]
        public void ReplacesWithAnParticleInMiddleOfText()
        {
            var replacer = new StringTemplateReplacer();

            var template = "The goblin stole a(n) axe from your bag!";

            var result = replacer.FillInTemplate(null, null, template, null);

            Assert.AreEqual("The goblin stole an axe from your bag!", result);
        }


        [TestMethod]
        public void ReplacesWithAParticleAtEndOfText()
        {
            var replacer = new StringTemplateReplacer();

            var template = "Some text a(n)";

            var result = replacer.FillInTemplate(null, null, template, null);

            Assert.AreEqual("Some text a", result);
        }
        [TestMethod]
        public void ReplacesWithMultipleAInText()
        {
            var replacer = new StringTemplateReplacer();

            var template = "I ate a(n).apple for lunch";

            var result = replacer.FillInTemplate(null, null, template, null);

            Assert.AreEqual("I ate an.apple for lunch", result);
        }

        [TestMethod]
        public void ReplacesWithMultipleAvsAnInText()
        {
            var replacer = new StringTemplateReplacer();

            var template = "I have a(n) shovel. I also have a(n) pen. I don't like it when a(n) alligator bites me, though.";

            var result = replacer.FillInTemplate(null, null, template, null);

            Assert.AreEqual("I have a shovel. I also have a pen. I don't like it when an alligator bites me, though.", result);
        }

    }
}
