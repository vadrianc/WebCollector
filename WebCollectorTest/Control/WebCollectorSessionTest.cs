namespace WebCollectorTest.Control
{
    using System;
    using System.IO;
    using NUnit.Framework;
    using SoftwareControllerApi.Action;
    using SoftwareControllerLib;
    using SoftwareControllerLib.Action;
    using WebCollector;
    using WebCollector.Actions;
    using WebCollector.Config;

    [TestFixture]
    public class WebCollectorSessionTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        [Category("WebCollectorSession")]
        public void NullAddress()
        {
            new WebCollectorSession("abc", null, "http://www.yellowpages.com/search?search_terms=supermarket&amp;geo_location_terms=Washington%2C%20DC");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        [Category("WebCollectorSession")]
        public void NullStartAddress()
        {
            new WebCollectorSession("abc", "http://www.yellowpages.com", null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [Category("WebCollectorSession")]
        public void EmptyAddress()
        {
            new WebCollectorSession("abc", "   ", "http://www.yellowpages.com/search?search_terms=supermarket&amp;geo_location_terms=Washington%2C%20DC");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [Category("WebCollectorSession")]
        public void EmptyStartAddress()
        {
            new WebCollectorSession("abc", "http://www.yellowpages.com", "   ");
        }

        [Test]
        [Category("WebCollectorSession")]
        public void CheckAddressAndStartAddress()
        {
            WebCollectorSession session = new WebCollectorSession("abc", "http://www.yellowpages.com", "http://www.yellowpages.com/search?search_terms=supermarket&amp;geo_location_terms=Washington%2C%20DC");

            Assert.That(session.Address, Is.EqualTo("http://www.yellowpages.com"));
            Assert.That(session.StartAddress, Is.EqualTo("http://www.yellowpages.com/search?search_terms=supermarket&amp;geo_location_terms=Washington%2C%20DC"));
        }

        [Test]
        [Category("WebCollectorSession")]
        public void RunYellowPagesSessionAndFail()
        {
            WebConfigReader reader = new WebConfigReader(Path.Combine("Resources", "yellowpages_config.xml"));
            WebCollectorSession session = reader.Read();

            using (StreamReader sr = new StreamReader(Path.Combine("Resources", "yellowpages_sample.html")))
            { 
                session.Html = sr.ReadToEnd();

                IResult result = session.RunUntilFailure();
                Assert.That(result.State, Is.EqualTo(ActionState.FAIL));
                Assert.That(result, Is.InstanceOf<MultiResult>());
                MultiResult multiResult = result as MultiResult;
                // Check for the 30 collected items + the fail action result => thus the 31
                Assert.That(multiResult.Results.Count, Is.EqualTo(31));

                Assert.That(session.Address, Is.EqualTo("http://www.yellowpages.com"));
                Assert.That(session.StartAddress, Is.EqualTo("http://www.yellowpages.com/search?search_terms=supermarket&geo_location_terms=Washington%2C%20DC"));
                Assert.That(session.Rules.Count, Is.EqualTo(2));
                Assert.That(session.Rules[0], Is.InstanceOf<RepeatableRule>());
                Assert.That(session.Rules[0].Actions.Count, Is.EqualTo(2));
                Assert.That(session.Rules[0].Actions[0], Is.InstanceOf<TagCollectAction>());
                Assert.That(session.Rules[0].Actions[1], Is.InstanceOf<WaitAction>());
                Assert.That(session.Rules[1], Is.InstanceOf<Rule>());
                Assert.That(session.Rules[1].Actions.Count, Is.EqualTo(1));
                Assert.That(session.Rules[1].Actions[0], Is.InstanceOf<TagCollectAction>());
            }
        }
    }
}
