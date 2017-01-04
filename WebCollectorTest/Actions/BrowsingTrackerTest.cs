namespace WebCollectorTest.Actions
{
    using NUnit.Framework;
    using WebCollector.Actions.Navigate;

    [TestFixture]
    public class BrowsingTrackerTest
    {
        [Test]
        [Category("BrowsingTracker")]
        public void PushAndPop()
        {
            BrowsingTracker tracker = new BrowsingTracker();
            tracker.Push("www.hotnews.com");
            string address = tracker.Pop();
            Assert.That(address, Is.EqualTo("www.hotnews.com"));
        }

        [Test]
        [Category("BrowsingTracker")]
        public void GetPreviousAddress()
        {
            BrowsingTracker tracker = new BrowsingTracker();
            tracker.Push("www.hotnews1.com");
            tracker.Push("www.hotnews2.com");
            string address = tracker.GetPreviousAddress();
            Assert.That(address, Is.EqualTo("www.hotnews1.com"));
        }

        [Test]
        [Category("BrowsingTracker")]
        public void GetPreviousAddressOneAddress()
        {
            BrowsingTracker tracker = new BrowsingTracker();
            tracker.Push("www.hotnews1.com");
            string address = tracker.GetPreviousAddress();
            Assert.That(address, Is.EqualTo(null));
        }

        [Test]
        [Category("BrowsingTracker")]
        public void GetPreviousAddressNoAddress()
        {
            BrowsingTracker tracker = new BrowsingTracker();
            string address = tracker.GetPreviousAddress();
            Assert.That(address, Is.EqualTo(null));
        }
    }
}
