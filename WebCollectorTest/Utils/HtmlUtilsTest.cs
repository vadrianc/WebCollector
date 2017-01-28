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

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        [Category("HtmlUtils")]
        public void StripNull()
        {
            HtmlUtils.StripHtmlTags(null);
        }

        [Test]
        [TestCase("<br>")]
        [TestCase("</br>")]
        [Category("HtmlUtils")]
        public void SingleTagElement(string element)
        {
            string result = HtmlUtils.StripHtmlTags(element);
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        [TestCase("<a>abc</a>", "abc")]
        [TestCase("<a></a>", "")]
        [TestCase("<h1 attr=\"1\"><a>abc</a></h1>", "abc")]
        [TestCase("<a><p>abc <br>new line</p></a>", "abc new line")]
        [Category("HtmlUtils")]
        public void StartAndEndTag(string element, string text)
        {
            string result = HtmlUtils.StripHtmlTags(element);
            Assert.That(result, Is.EqualTo(text));
        }

        [Test]
        [TestCase("<a")]
        [TestCase("<a>>/a<")]
        [TestCase(">a<")]
        [ExpectedException(typeof(ArgumentException))]
        [Category("HtmlUtils")]
        public void BadlyFormatterHTMLElement(string element)
        {
            HtmlUtils.StripHtmlTags(element);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        [Category("HtmlUtils")]
        public void ReplaceSymbolsInNullString()
        {
            HtmlUtils.ReplaceSymbols(null);
        }

        [Test]
        [TestCase("", "")]
        [TestCase("<div>&nbsp;</div>", "<div> </div>")]
        [TestCase("<div>&nbsp;&nbsp;</div>", "<div>  </div>")]
        [TestCase("<div>z&nbsp;abc&nbsp;def</div>", "<div>z abc def</div>")]
        [TestCase("&pound;&copy;", "£©")]
        [Category("HtmlUtils")]
        public void ReplaceSymbols(string input, string expectedOutput)
        {
            string output = HtmlUtils.ReplaceSymbols(input);
            Assert.That(output, Is.EqualTo(expectedOutput));
        }
    }
}
