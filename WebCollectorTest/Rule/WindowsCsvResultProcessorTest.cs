namespace WebCollectorTest.Rule
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using NUnit.Framework;
    using SoftwareControllerApi.Action;
    using SoftwareControllerLib.Action;
    using WebCollector.Actions.Collect;
    using WebCollector.Rule;

    [TestFixture(Category = "WindowsCsvResultProcessor")]
    public class WindowsCsvResultProcessorTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullCsvFile()
        {
            new WindowsCsvResultProcessor(null);
        }

        [Test]
        [TestCase("")]
        [TestCase("   ")]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyCsvFile(string csvFile)
        {
            new WindowsCsvResultProcessor(csvFile);
        }

        [Test]
        [TestCase("abc", "cba", "abc,cba")]
        [TestCase("abc\n", "\tcba", "abc,cba")]
        [TestCase("abc\n\t\t\t", "\tcba", "abc,cba")]
        [TestCase("\n", "\tcba\n", ",cba")]
        [TestCase("abc", "cb,a", "abc,\"cb,a\"")]
        [TestCase("abc", "cba,", "abc,\"cba,\"")]
        [TestCase(null, "cba", ",cba")]
        [TestCase("", "cba", ",cba")]
        public void CsvTwoFields(string field1, string field2, string expectedLine)
        {
            try {
                WindowsCsvResultProcessor windowsCsv = new WindowsCsvResultProcessor("test.csv");
                SingleResult<TextItem> result1 = new SingleResult<TextItem>(new TextItem(field1), ActionState.SUCCESS);
                SingleResult<TextItem> result2 = new SingleResult<TextItem>(new TextItem(field2), ActionState.SUCCESS);
                IList<IResult> results = new List<IResult>();
                results.Add(result1);
                results.Add(result2);
                windowsCsv.Process(results);

                Assert.That(File.Exists("test.csv"), Is.True);
                using (StreamReader sr = new StreamReader("test.csv")) {
                    string line;
                    int count = 0;
                    while ((line = sr.ReadLine()) != null) {
                        Assert.That(line, Is.EqualTo(expectedLine));
                        count++;
                    }

                    Assert.That(count, Is.EqualTo(1));
                }
            } finally {
                File.Delete("test.csv");
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullResults()
        {
            WindowsCsvResultProcessor windowsCsv = new WindowsCsvResultProcessor("test.csv");
            windowsCsv.Process(null);
        }

        [Test]
        public void EmptyResults()
        {
            try {
                WindowsCsvResultProcessor windowsCsv = new WindowsCsvResultProcessor("test.csv");
                IList<IResult> results = new List<IResult>();
                windowsCsv.Process(results);
            } finally {
                File.Delete("test.csv");
            }
        }
    }
}
