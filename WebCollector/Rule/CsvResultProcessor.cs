namespace WebCollector.Rule
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Actions.Collect;
    using SoftwareControllerApi.Action;
    using SoftwareControllerApi.Rule;

    /// <summary>
    /// CSV processor class for writing a list of results to a CSV file.
    /// </summary>
    public class CsvResultProcessor : IResultProcessor
    {
        private readonly string m_CsvFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvResultProcessor"/> class.
        /// </summary>
        /// <param name="csvFile">The CSV file path.</param>
        public CsvResultProcessor(string csvFile)
        {
            m_CsvFile = csvFile;
        }

        /// <summary>
        /// Write the list of results to the CSV file.
        /// </summary>
        /// <param name="results">The list of results.</param>
        public void Process(IList<IResult> results)
        {
            using (StreamWriter writer = new StreamWriter(m_CsvFile, true)) {
                StringBuilder lineBuilder = new StringBuilder();

                foreach (IResult result in results) {
                    ISingleResult<ItemBase> singleResult = result as ISingleResult<ItemBase>;
                    if (singleResult == null) continue;

                    TextItem textItem = singleResult.Content as TextItem;
                    if (textItem == null) continue;

                    lineBuilder.Append(textItem.Content);
                    lineBuilder.Append(',');
                }

                string line = lineBuilder.ToString().Trim(',');
                if (!string.IsNullOrWhiteSpace(line)) writer.WriteLine(line);
            }
        }
    }
}
