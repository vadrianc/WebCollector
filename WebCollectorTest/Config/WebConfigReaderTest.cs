namespace WebCollectorTest.Config
{
    using System;
    using System.IO;
    using System.Xml;
    using NUnit.Framework;
    using WebCollector;
    using WebCollector.Config;

    [TestFixture]
    public class WebConfigReaderTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        [Category("WebConfigReader")]
        public void NullConfigFile()
        {
            new WebConfigReader(null);
        }

        [Test]
        [TestCase("")]
        [TestCase("   ")]
        [ExpectedException(typeof(ArgumentException))]
        [Category("WebConfigReader")]
        public void EmptyConfigFile(string file)
        {
            new WebConfigReader(file);
        }

        [Test]
        [Category("WebConfigReader")]
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
            Assert.That(session.Html, Is.Null);
        }

        [Test]
        [ExpectedException(typeof(XmlException))]
        [Category("WebConfigReader")]
        public void InexistentActionInConfig()
        {
            WebConfigReader reader = new WebConfigReader(Path.Combine("Resources", "inexistent_action.xml"));
            reader.Read();
        }
    }
}
