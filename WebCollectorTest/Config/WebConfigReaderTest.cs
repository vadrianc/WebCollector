namespace WebCollectorTest.Config
{
    using System;
    using System.IO;
    using System.Xml;
    using NUnit.Framework;
    using SoftwareControllerLib;
    using WebCollector;
    using WebCollector.Actions;
    using WebCollector.Config;

    [TestFixture(Category = "WebConfigReader")]
    public class WebConfigReaderTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullConfigFile()
        {
            new WebConfigReader(null);
        }

        [Test]
        [TestCase("")]
        [TestCase("   ")]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyConfigFile(string file)
        {
            new WebConfigReader(file);
        }

        [Test]
        public void ReadConfig()
        {
            WebConfigReader reader = new WebConfigReader(Path.Combine("Resources", "default.xml"));
            WebCollectorSession session = reader.Read();

            Assert.That(session, Is.Not.Null);
            Assert.That(session.Address, Is.EqualTo("http://www.yellowpages.com"));
            Assert.That(session.StartAddress, Is.EqualTo("http://www.yellowpages.com/search?search_terms=supermarket&geo_location_terms=Washington%2C%20DC"));
            Assert.That(session.Name, Is.EqualTo("YellowPages scraper"));
            Assert.That(session.Rules, Is.Not.Null);
            Assert.That(session.Rules.Count, Is.EqualTo(2));
            Assert.That(session.Rules[0], Is.InstanceOf<Rule>());
            Assert.That(session.Rules[0].Actions.Count, Is.EqualTo(2));
            Assert.That(session.Rules[0].Actions[0], Is.InstanceOf<TagCollectAction>());
            Assert.That(session.Rules[0].Actions[1], Is.InstanceOf<WaitAction>());
            Assert.That(session.Rules[1], Is.InstanceOf<Rule>());
            Assert.That(session.Rules[1].Actions.Count, Is.EqualTo(1));
            Assert.That(session.Rules[1].Actions[0], Is.InstanceOf<NavigateAction>());
            Assert.That(session.Html, Is.Null);
        }

        [Test]
        [ExpectedException(typeof(XmlException))]
        public void InexistentActionInConfig()
        {
            WebConfigReader reader = new WebConfigReader(Path.Combine("Resources", "inexistent_action.xml"));
            reader.Read();
        }

        [Test]
        public void ConfigWithRepeatableRule()
        {
            WebConfigReader reader = new WebConfigReader(Path.Combine("Resources", "repeatable_rule.xml"));
            WebCollectorSession session = reader.Read();

            Assert.That(session, Is.Not.Null);
            Assert.That(session.Address, Is.EqualTo("http://www.yellowpages.com"));
            Assert.That(session.StartAddress, Is.EqualTo("http://www.yellowpages.com/search?search_terms=supermarket&geo_location_terms=Washington%2C%20DC"));
            Assert.That(session.Name, Is.EqualTo("YellowPages scraper"));
            Assert.That(session.Rules, Is.Not.Null);
            Assert.That(session.Rules.Count, Is.EqualTo(1));
            Assert.That(session.Rules[0], Is.InstanceOf<RepeatableRule>());
            Assert.That(session.Rules[0].Actions.Count, Is.EqualTo(1));
            Assert.That(session.Rules[0].Actions[0], Is.InstanceOf<TagCollectAction>());
            Assert.That(session.Html, Is.Null);
        }
    }
}
