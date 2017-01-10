namespace WebCollectorTest
{
    using System;
    using System.IO;
    using NUnit.Framework;
    using SoftwareControllerApi.Rule;
    using WebCollector.Rule;

    [TestFixture]
    public class ProcessorFactoryTest
    {
        [Test]
        [TestCase("  ")]
        [TestCase("")]
        [TestCase("abc")]
        [ExpectedException(typeof(ArgumentException))]
        [Category("ProcessorFactory")]
        public void InvalidPath(string file)
        {
            ProcessorFactory.Create(file);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [Category("ProcessorFactory")]
        public void DirectoryInsteadOfFile()
        {
            ProcessorFactory.Create(Directory.GetCurrentDirectory());
        }

        [Test]
        [Category("ProcessorFactory")]
        public void CsvFile()
        {
            IResultProcessor processor = ProcessorFactory.Create("c:\\out.csv");
            Assert.IsInstanceOf<WindowsCsvResultProcessor>(processor);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [Category("ProcessorFactory")]
        public void WeirdExtension()
        {
            ProcessorFactory.Create("c:\\out.abc");
        }
    }
}
