namespace WebCollectorTest.Actions
{
    using System;
    using System.IO;
    using System.Net.NetworkInformation;
    using NUnit.Framework;
    using SoftwareControllerApi.Action;
    using WebCollector;
    using WebCollector.Actions;
    using WebCollector.Config;
    using WebCollector.Utils;

    [TestFixture]
    public class NavigateActionTest
    {
        [Test]
        [Timeout(3100)]
        [Category("NavigateAction")]
        public void NavigateExecute()
        {
            WebConfigReader reader = new WebConfigReader(Path.Combine("Resources", "default.xml"));
            WebCollectorSession session = reader.Read();
            session.Html = HtmlUtils.GetHtmlString(session.StartAddress);
            session.AddressTracker.Push(session.StartAddress);

            Ping ping = new Ping();
            Uri uri = new Uri(session.Address);
            PingReply reply = ping.Send(uri.Host);
            if (reply.Status != IPStatus.Success) Assert.Inconclusive("Could not access " + session.Address);

            NavigateAction action = new NavigateAction(session, null);
            action.Tag = "a";
            action.Class = "next ajax-page";

            Assert.That(action.Link, Is.Null);
            Assert.That(action.Tag, Is.EqualTo("a"));
            Assert.That(action.Class, Is.EqualTo("next ajax-page"));
            Assert.That(action.Where, Is.Null);
            Assert.That(action.Session, Is.EqualTo(session));
            Assert.That(action.Name, Is.Null);

            IResult result = action.Execute();

            Assert.That(result.State, Is.EqualTo(ActionState.SUCCESS));
            Assert.That(action.Link, Is.EqualTo("http://www.yellowpages.com/search?search_terms=supermarket&geo_location_terms=Washington%2C%20DC&page=2"));
            Assert.That(action.Tag, Is.EqualTo("a"));
            Assert.That(action.Class, Is.EqualTo("next ajax-page"));
            Assert.That(action.Where, Is.Null);
            Assert.That(action.Session, Is.EqualTo(session));
            Assert.That(action.Name, Is.Null);

        }
    }
}
