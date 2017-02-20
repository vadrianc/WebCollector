namespace WebCollectorTest.Actions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.NetworkInformation;
    using NUnit.Framework;
    using SoftwareControllerApi.Action;
    using WebCollector;
    using WebCollector.Actions;
    using WebCollector.Config;
    using WebCollector.Utils;

    [TestFixture(Category = "NavigateAction")]
    public class NavigateActionTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullSession()
        {
            new NavigateAction(null, "back");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullSession2()
        {
            new NavigateAction("abc", null, "back");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullActionName()
        {
            WebConfigReader reader = new WebConfigReader(Path.Combine("Resources", "default.xml"));
            WebCollectorSession session = reader.Read();

            new NavigateAction(null, session, "back");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyActionName()
        {
            WebConfigReader reader = new WebConfigReader(Path.Combine("Resources", "default.xml"));
            WebCollectorSession session = reader.Read();

            new NavigateAction(string.Empty, session, "back");
        }

        [Test]
        [Timeout(5000)]
        public void NavigateExecute()
        {
            WebConfigReader reader = new WebConfigReader(Path.Combine("Resources", "default.xml"));
            WebCollectorSession session = reader.Read();
            session.Html = HtmlUtils.GetHtmlString(session.StartAddress);
            session.AddressTracker.Push(session.StartAddress);

            Ping ping = new Ping();
            Uri uri = new Uri(session.Address);
            PingReply reply = ping.Send(uri.Host);
            if (reply.Status != IPStatus.Success) Assert.Inconclusive("Failed to ping " + session.Address);

            NavigateAction action = new NavigateAction(session, null);
            action.Tag = "a";
            action.Attributes = new List<TagAttribute>();
            action.Attributes.Add(new TagAttribute("class", "next ajax-page"));

            Assert.That(action.Link, Is.Null);
            Assert.That(action.Tag, Is.EqualTo("a"));
            Assert.That(action.Attributes.Count, Is.EqualTo(1));
            Assert.That(action.Attributes[0].Name, Is.EqualTo("class"));
            Assert.That(action.Attributes[0].Value, Is.EqualTo("next ajax-page"));
            Assert.That(action.Attributes[0].IsSingleQuote, Is.False);
            Assert.That(action.Where, Is.Null);
            Assert.That(action.Session, Is.EqualTo(session));
            Assert.That(action.Name, Is.Null);

            IResult result = action.Execute();

            Assert.That(result.State, Is.EqualTo(ActionState.SUCCESS));
            Assert.That(action.Link, Is.EqualTo("http://www.yellowpages.com/search?search_terms=supermarket&geo_location_terms=Washington%2C%20DC&page=2"));
            Assert.That(action.Tag, Is.EqualTo("a"));
            Assert.That(action.Attributes.Count, Is.EqualTo(1));
            Assert.That(action.Attributes[0].Name, Is.EqualTo("class"));
            Assert.That(action.Attributes[0].Value, Is.EqualTo("next ajax-page"));
            Assert.That(action.Attributes[0].IsSingleQuote, Is.False);
            Assert.That(action.Where, Is.Null);
            Assert.That(action.Session, Is.EqualTo(session));
            Assert.That(action.Name, Is.Null);
        }
    }
}
