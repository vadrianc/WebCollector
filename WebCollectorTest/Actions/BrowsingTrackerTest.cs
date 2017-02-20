namespace WebCollectorTest.Actions
{
    using NUnit.Framework;
    using WebCollector.Actions.Navigate;

    [TestFixture(Category = "BrowsingTracker")]
    public class BrowsingTrackerTest
    {
        [Test]
        public void PushAndPop()
        {
            BrowsingTracker tracker = new BrowsingTracker();
            tracker.Push("www.hotnews.com");
            string address = tracker.Pop();
            Assert.That(address, Is.EqualTo("www.hotnews.com"));
        }

        [Test]
        public void GetPreviousAddress()
        {
            BrowsingTracker tracker = new BrowsingTracker();
            tracker.Push("www.hotnews1.com");
            tracker.Push("www.hotnews2.com");
            string address = tracker.GetPreviousAddress();
            Assert.That(address, Is.EqualTo("www.hotnews1.com"));
        }

        [Test]
        public void GetPreviousAddressOneAddress()
        {
            BrowsingTracker tracker = new BrowsingTracker();
            tracker.Push("www.hotnews1.com");
            string address = tracker.GetPreviousAddress();
            Assert.That(address, Is.EqualTo(null));
        }

        [Test]
        public void GetPreviousAddressNoAddress()
        {
            BrowsingTracker tracker = new BrowsingTracker();
            string address = tracker.GetPreviousAddress();
            Assert.That(address, Is.EqualTo(null));
        }
    }
}
