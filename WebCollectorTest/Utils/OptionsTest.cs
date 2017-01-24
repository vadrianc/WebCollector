namespace WebCollectorTest.Utils
{
    using System;
    using NUnit.Framework;
    using WebCollector.Utils;

    [TestFixture(Category = "Options")]
    public class OptionsTest
    {
        [Test]
        [TestCase("--config", "default.xml")]
        [TestCase("-c", "default.xml")]
        public void Config(params string[] helpOption)
        {
            Options options = new Options(helpOption);
            Assert.That(options.Help, Is.False);
            Assert.That(options.Config, Is.EqualTo("default.xml"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NoOptions()
        {
            new Options();
        }

        [Test]
        [TestCase("--config")]
        [TestCase("-c")]
        [ExpectedException(typeof(ArgumentException))]
        public void NoConfig(string config)
        {
            new Options(config);
        }
    }
}
