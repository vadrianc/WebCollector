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
    public abstract class CsvResultProcessorBase : IResultProcessor
    {
        private readonly string m_CsvFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvResultProcessorBase"/> class.
        /// </summary>
        /// <param name="csvFile">The CSV file path.</param>
        public CsvResultProcessorBase(string csvFile)
        {
            if (csvFile == null) throw new ArgumentNullException("csvFile");
            if (string.IsNullOrWhiteSpace(csvFile)) throw new ArgumentException("Cannot be empty or whitespace only", "csvFile");

            m_CsvFile = csvFile;
        }

        /// <summary>
        /// Write the list of results to the CSV file.
        /// </summary>
        /// <param name="results">The list of results.</param>
        public void Process(IList<IResult> results)
        {
            if (results == null) throw new ArgumentNullException("results");

            using (StreamWriter writer = new StreamWriter(m_CsvFile, true)) {
                StringBuilder lineBuilder = new StringBuilder();

                foreach (IResult result in results) {
                    ISingleResult<ItemBase> singleResult = result as ISingleResult<ItemBase>;
                    if (singleResult == null) continue;

                    TextItem textItem = singleResult.Content as TextItem;
                    if (textItem == null) continue;

                    lineBuilder.Append(Escape(textItem.Content));
                    lineBuilder.Append(',');
                }

                string line = lineBuilder.ToString().TrimEnd(',');
                if (!string.IsNullOrWhiteSpace(line)) writer.WriteLine(line);
            }
        }

        /// <summary>
        /// Escape special characters within the given field.
        /// </summary>
        /// <param name="field">The field to be escaped.</param>
        /// <returns>The given field with escaped special characters.</returns>
        protected abstract string Escape(string field);
    }
}
