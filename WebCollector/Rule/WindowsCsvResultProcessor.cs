namespace WebCollector.Rule
{
    using System.Text;

    /// <summary>
    /// Windows specific CSV processor class for writing a list of results to a CSV file.
    /// </summary>
    public class WindowsCsvResultProcessor : CsvResultProcessorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsCsvResultProcessor"/> class.
        /// </summary>
        /// <param name="csvFile">The CSV file path.</param>
        public WindowsCsvResultProcessor(string csvFile) : base(csvFile) { }

        private const string c_Quote = "\"";
        private const string c_DoubleQuote = "\"\"";
        private const string c_Comma = ",";

        /// <summary>
        /// Escape special characters within the given field.
        /// </summary>
        /// <param name="field">The field to be escaped.</param>
        /// <returns>The given field with escaped special characters.</returns>
        protected override string Escape(string field)
        {
            if (field == null) return null;

            if (field.Contains(c_Quote)) field = field.Replace(c_Quote, c_DoubleQuote);

            if (field.Contains(c_Comma)) {
                StringBuilder fieldBuilder = new StringBuilder();
                fieldBuilder.Append(c_Quote);
                fieldBuilder.Append(field);
                fieldBuilder.Append(c_Quote);

                return fieldBuilder.ToString();
            }

            return field;
        }
    }
}