namespace WebCollectorTest.Utils
{
    using System;
    using System.IO;
    using NUnit.Framework;
    using WebCollector.Utils;

    [TestFixture]
    public class HtmlUtilsTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        [Category("HtmlUtils")]
        public void NullUri()
        {
            HtmlUtils.GetHtmlString(null);
        }

        [Test]
        [TestCase("")]
        [TestCase("   ")]
        [ExpectedException(typeof(ArgumentException))]
        [Category("HtmlUtils")]
        public void EmptyUri(string uri)
        {
            HtmlUtils.GetHtmlString(uri);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [Category("HtmlUtils")]
        public void InvalidUri()
        {
            HtmlUtils.GetHtmlString(Path.Combine("abc", "sample.html"));
        }

        [Test]
        [Category("HtmlUtils")]
        public void GetHtmlString()
        {
            string localPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "yellowpages_sample.html");
            Uri uri = new Uri(localPath, UriKind.Absolute);
            string html = HtmlUtils.GetHtmlString(uri.AbsoluteUri);
            Assert.That(html, Is.Not.Null);
        }
    }
}
