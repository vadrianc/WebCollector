namespace WebCollectorTest.Control
{
    using System;
    using NUnit.Framework;
    using WebCollector;

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
    }
}
